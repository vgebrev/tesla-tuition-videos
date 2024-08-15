<script>
  /** @type {boolean} */
  export let isLoading = false;
  /** @type {number} */
  export let value = 100;
  /** @type {number} */
  export let min = 0;
  /** @type {number} */
  export let max = 100;

  $: width = `${(100 * value) / (max - min)}%`;
</script>

{#if isLoading}
  <div {...$$restProps}>
    <div class="progress">
      <div
        class="progress-bar progress-bar-striped progress-bar-animated"
        role="progressbar"
        style="width:{width}"
        aria-valuenow={value}
        aria-valuemin={min}
        aria-valuemax={max}
      ></div>
    </div>
  </div>
{/if}
