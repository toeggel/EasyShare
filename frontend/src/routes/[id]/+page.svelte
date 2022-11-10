<script lang="ts">
    import type { PageData } from './$types';
    import SharedContent from '$lib/SharedContent.svelte';

    export let data: PageData;
    console.log(data);
</script>

<SharedContent content="{data.content}"></SharedContent>
