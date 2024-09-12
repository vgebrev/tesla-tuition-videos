<script>
  import { config } from '$lib/config.js';
  import Popover from '$components/common/Popover.svelte';

  /** @type {HTMLSpanElement} */
  let popoverElem;
</script>

{#if config.isTestEnvironment}
  <div
    class="position-fixed top-0 w-100 d-flex justify-content-center p-2"
    style="z-index: 1050;pointer-events:none;">
    <span
      bind:this={popoverElem}
      class="badge rounded-pill bg-danger p-2"
      style="cursor: pointer;pointer-events:auto;">
      Test Environment
    </span>
    <Popover triggerElem={popoverElem}>
      <div class="p-2 fs-6">
        This is a test copy of Tesla Tuition Videos. No paid content is shown and no real transactions are processed.
        The live site is at <a
          href="https://teslatuitionvideos.co.za"
          class="text-white">https://teslatuitionvideos.co.za</a
        >.
      </div>
    </Popover>
  </div>
{/if}
