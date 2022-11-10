<script lang="ts">

    let content = '';

    import { SharedContentsService } from '$lib/shared-contents.service';

    async function publish(event: MouseEvent): Promise<void> {
        const sharedContentsService = new SharedContentsService();
        await sharedContentsService.createSharedContent(content);
    }

    const copy = (event: MouseEvent): void => {
        const content = document.getElementsByClassName('input')[0] as HTMLInputElement;
        navigator.clipboard.writeText(content.value);
    };

    function share(event: MouseEvent): void {
    }

</script>

<header class="row">
    <h1>EasyShare</h1>
    <button class="btn" on:click={publish}>Publish</button>
</header>

<main class="input-wrapper">
    <textarea class="input" placeholder="Insert text here" bind:value={content}></textarea>
</main>

<footer class="row">
    <button class="btn" on:click={copy}>Copy</button>
    <button class="btn" on:click={share}>Share</button>
</footer>

<style>
    header {
        margin-bottom: 16px;
    }

    footer {
        margin-top: 16px;
    }

    h1 {
        margin: 0;
    }

    .row {
        display: flex;
        flex-direction: row;
        justify-content: start;
        align-items: center;
        gap: 16px;
        height: 32px;
    }

    .input-wrapper {
        display: flex;
        flex-direction: column;
        flex: 1;
    }

    .input-wrapper .input {
        height: 100%;
    }

</style>
