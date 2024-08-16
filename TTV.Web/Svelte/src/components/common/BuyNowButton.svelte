<script>
  import {
    shoppingCartActions,
    shoppingCartStore
  } from '$components/ShoppingCart/shopping-cart.js';
  import PriceDisplay from '$components/common/PriceDisplay.svelte';

  /** @type {import('$lib/types').Lesson} */
  export let lesson;

  /** type {string} */
  export let cssClass = 'btn btn-primary';

  let props = {
    class: cssClass,
    ...$$restProps
  };

  function addToCart() {
    shoppingCartActions.addLesson(lesson);
  }

  $: state = $shoppingCartStore;
</script>

{#if state.lessons.some((l) => l.id === lesson.id)}
  <span class="text-primary">
    <i class="bi bi-cart-check"></i> Added to <a href="/shopping-cart" class="card-link">cart</a>.
  </span>
{:else}
  <button type="button" {...props} on:click={addToCart}
    ><i class="bi bi-cart4 me-1"></i> Buy Now for <PriceDisplay price={lesson.currentPrice}
    ></PriceDisplay></button
  >
{/if}
