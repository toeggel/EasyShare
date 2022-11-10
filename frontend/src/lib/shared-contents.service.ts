import { Configuration, SharedContentsApi } from './_generated-api';

export class SharedContentsService {
    private sharedContentsApi: SharedContentsApi;

    constructor() {
        const configuration = new Configuration({
            basePath: 'https://localhost:7200',
        });

        this.sharedContentsApi = new SharedContentsApi(configuration);
    }

    async getSharedContent(id: string): Promise<string> {
        return await this.sharedContentsApi.getSharedContent({ id });
    };

    async createSharedContent(content: string): Promise<void> {
        return await this.sharedContentsApi.createSharedContent({ body: content });
    };
}
