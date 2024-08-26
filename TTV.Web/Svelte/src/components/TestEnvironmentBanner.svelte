<script>
  import { onMount } from 'svelte';
  import { config } from '$lib/config.js';

  /** @type {HTMLSpanElement} */
  let popoverElem;
  onMount(() => {
    if (!config.isTestEnvironment) return;
    new bootstrap.Popover(popoverElem, { html: true });
  });
</script>

{#if config.isTestEnvironment}
  <div
    class="position-fixed top-0 w-100 d-flex justify-content-center p-2"
    style="z-index: 1050;pointer-events:none;">
    <span
      bind:this={popoverElem}
      class="badge rounded-pill bg-danger p-2"
      style="cursor: pointer;pointer-events:auto;"
      data-bs-toggle="popover"
      data-bs-content="This is a test copy of Tesla Tuition Videos. No paid content is shown and no real transactions are processed. The live site is at <a href='https://teslatuitionvideos.co.za' class='text-secondary'>https://teslatuitionvideos.co.za</a>."
      data-bs-trigger="click">
      Test Environment
    </span>
  </div>
{/if}
