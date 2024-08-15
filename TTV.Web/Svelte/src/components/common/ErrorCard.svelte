<script>
  /** @type {import('$lib/types').ErrorState} */
  export let error = { isError: false, message: null };
</script>

{#if error.isError}
  <div class="card border-danger">
    <div class="card-body">
      <p class="card-text">{error.message}</p>
    </div>
  </div>
{/if}
