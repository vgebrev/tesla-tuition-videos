<script>
  import { onMount } from 'svelte';
  import { myOrdersActions, myOrdersStore } from '$components/My/Orders/my-orders.js';
  import OrderDetail from '$components/My/Orders/OrderDetail.svelte';

  onMount(async () => {
    await myOrdersActions.getOrders();
  });

  $: state = $myOrdersStore;
</script>

<div class="row">
  <div class="card-text">
    {#if state.orders && state.orders.length > 0}
      <div class="row row-cols-sm-1 row-cols-md-1 row-cols-md-2 g-3">
        {#each state.orders as order (order.id)}
          <div class="col-sm-12 col-md-12 col-lg-6">
            <OrderDetail {order} />
          </div>
        {/each}
      </div>
    {:else if !state.isLoading}
      <div class="row">
        <div class="col text-center d-flex flex-column gap-3 mt-3">
          Your order history is empty.
          <a
            href="/lessons"
            class="card-link">Order some lessons.</a>
        </div>
      </div>
    {/if}
  </div>
</div>
