import type { PageLoad } from './$types';
import type { SharedContent } from '$lib/_generated-api';

// Locally we struggle with self-signed certificate.
// We can disable it by running next line (once) (it can be enabled with "=1".
// process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0
// Or disable SSR all together
// export const ssr = true

export const load: PageLoad = async ({ fetch, params }) => {
    const res = await fetch(`https://localhost:7200/shared-contents/${ params.id }`);
    return await res.json() as SharedContent;
};
