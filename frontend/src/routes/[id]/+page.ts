import type { PageLoad } from './$types';
import { SharedContentsService } from '$lib/shared-contents.service';

export const load: PageLoad = async ({ fetch, params }) => {
    if (typeof window!=='undefined') {
        const { id }: { id: string } = params;
        const sharedContentsService = new SharedContentsService();
        return {
            content: await sharedContentsService.getSharedContent(id),
        };
    }
};
