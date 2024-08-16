<script>
  /** @type {import('$lib/types').Price} */
  export let price;

  function formatted(amount) {
    return `R${amount.toFixed(0)}`;
  }
</script>

{#if price.promoAmount !== null}
  {formatted(price.promoAmount)} <del class="text-faint">{formatted(price.amount)}</del>
{:else}
  {formatted(price.amount)}
{/if}
