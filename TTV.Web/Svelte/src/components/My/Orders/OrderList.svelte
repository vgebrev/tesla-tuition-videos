<script>
  import { onMount } from 'svelte';
  import { myOrdersActions, myOrdersStore } from '$components/My/Orders/my-orders.js';
  import OrderDetail from '$components/My/Orders/OrderDetail.svelte';
  import Pagination from '$components/common/Pagination.svelte';

  onMount(async () => {
    await myOrdersActions.getOrders();
  });

  /**
   * Handle page change.
   * @param {number} skip
   * @param {number} take
   */
  async function onPageChange(skip, take) {
    state.pageInfo.skip = skip;
    state.pageInfo.take = take;
    await myOrdersActions.getOrders();
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

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
      <div class="row row-cols-1">
        <div class="col g-3 mx-2 d-flex justify-content-center justify-content-md-start">
          <Pagination
            pageInfo={state.pageInfo}
            {onPageChange} />
        </div>
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
