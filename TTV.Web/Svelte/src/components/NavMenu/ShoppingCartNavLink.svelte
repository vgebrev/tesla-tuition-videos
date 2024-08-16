<script>
  import {
    shoppingCartActions,
    shoppingCartStore
  } from '$components/ShoppingCart/shopping-cart.js';
  import { onMount } from 'svelte';
  import RemoveAlreadyOwnedLessons from '$components/ShoppingCart/RemoveAlreadyOwnedLessons.svelte';

  onMount(() => {
    shoppingCartActions.loadFromLocalStorage();
  });

  $: state = $shoppingCartStore;
</script>

<RemoveAlreadyOwnedLessons />
<a class="nav-link" href="/shopping-cart">
  <i class="bi bi-cart4"></i> Cart
  <span class="badge bg-primary rounded-pill">{state.lessons.length}</span>
</a>
