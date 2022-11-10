import { Configuration, SharedContentsApi, type SharedContent } from './_generated-api';

export class SharedContentsService {
    private sharedContentsApi: SharedContentsApi;

    constructor() {
        const configuration = new Configuration({
            basePath: 'https://127.0.0.1:7200',
        });

        this.sharedContentsApi = new SharedContentsApi(configuration);
    }

    async getSharedContent(id: number): Promise<SharedContent> {
        return await this.sharedContentsApi.getSharedContent({ id });
    };

    async createSharedContent(content: string): Promise<void> {
        return await this.sharedContentsApi.createSharedContent({ body: content });
    };
}
