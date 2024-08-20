<script>
  /** @type {Result} */
  export let result = null;

  $: borderCssClass = result?.isSuccess || false ? 'border-success' : 'border-danger';
  $: resultMessage = result?.message || (result?.isSuccess || false ? 'Successful' : 'Unsuccessful');
</script>

{#if result}
  <div class="card {borderCssClass}">
    <div class="card-body">
      <p class="card-text">
        {resultMessage}
        <slot />
      </p>
    </div>
  </div>
{/if}
