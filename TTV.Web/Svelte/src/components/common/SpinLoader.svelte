<script>
  /** @type {boolean} */
  export let isLoading = false;
</script>

{#if isLoading}
  <div
    class="spinner-border spinner-border-sm text-primary"
    role="status">
    <span class="visually-hidden">Loading...</span>
  </div>
{/if}
