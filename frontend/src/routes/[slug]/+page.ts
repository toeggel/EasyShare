import type { PageLoad } from './$types';

export const load: PageLoad = ({ params: { slug } }: { params: { slug: string } }) => {
	return {
		content: fetch('https://api.fake-rest.refine.dev/posts')
			.then((response) => response.json())
			.then((json) => slug + ' ==> ' + json[0].title)
	};
};
