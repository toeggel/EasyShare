import type { PageLoad } from './$types';
import { Configuration, SharesApi } from '$lib/_generated-api';

export const load: PageLoad = async ({ params }) => {
    const { id }: { id: string } = params;
    if (typeof window!=='undefined') {
        const configuration = new Configuration({
            basePath: 'https://localhost:7200',
        });
        const shareApi = new SharesApi(configuration);
        return {
            content: await shareApi.getShare({ id }),
        };
    }

};
