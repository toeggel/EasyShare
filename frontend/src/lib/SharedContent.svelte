<script lang="ts">
    import { goto } from '$app/navigation';
    import type { SharedContent } from '$lib/_generated-api';

    export let content = '';

    const publish = async (): Promise<void> => {
        const response = await fetch(`https://localhost:7200/shared-contents`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(content),
        });

        let sharedContent = await response.json() as SharedContent;
        goto(`${ sharedContent.id }`);
    };

    const copy = (): void => {
        const content = document.getElementsByClassName('input')[0] as HTMLInputElement;
        navigator.clipboard.writeText(content.value);
    };

</script>

<header class="row">
    <h1>EasyShare</h1>
    <button class="btn" on:click={publish}>Save</button>
</header>

<main class="input-wrapper">
    <textarea class="input" placeholder="Insert text here" bind:value={content}></textarea>
</main>

<footer class="row">
    <button class="btn" on:click={copy}>Copy</button>
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
