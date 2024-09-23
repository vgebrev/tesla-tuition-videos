<script>
  import { adminOrdersStore, adminOrdersActions } from '$components/Admin/Orders/orders.js';
  import { onMount } from 'svelte';
  import { formatDateTime } from '$lib/util.js';
  import UserDisplay from '$components/Admin/UserDisplay.svelte';
  import OrderItemsPopover from '$components/Admin/Orders/OrderItemsPopover.svelte';
  import OrderPaymentsPopover from '$components/Admin/Orders/OrderPaymentsPopover.svelte';
  import Pagination from '$components/common/Pagination.svelte';

  onMount(async () => {
    await adminOrdersActions.getOrders();
  });

  /**
   * Complete the given order.
   * @param {import('$lib/types').Order} order
   */
  async function completeOrder(order) {
    await adminOrdersActions.completeOrder(order);
  }

  /**
   * Handle page change.
   * @param {number} skip
   * @param {number} take
   */
  async function onPageChange(skip, take) {
    $adminOrdersStore.filter.skip = skip;
    $adminOrdersStore.filter.take = take;
    await adminOrdersActions.getOrders();
  }

  $: state = $adminOrdersStore;
</script>

{#if state.orders && state.orders.length > 0}
  <div class="table-responsive small">
    <table class="table table-striped">
      <thead>
        <tr>
          <th>Number</th>
          <th>Total</th>
          <th>Payments</th>
          <th>Due</th>
          <th>Placed By</th>
          <th>Placed On</th>
          <th>Status</th>
          <th>Reason</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {#each state.orders as order, i (order.id)}
          <tr class:table-secondary={i % 2 === 0}>
            <td>{order.id}</td>
            <td>
              <OrderItemsPopover {order} />
              R{order.orderTotal.toFixed(0)}
            </td>
            <td>
              <OrderPaymentsPopover {order} />
              R{order.paymentsTotal.toFixed(0)}
            </td>
            <td>R{order.totalAmount.toFixed(0)}</td>
            <td>
              <UserDisplay user={order.placedBy} />
            </td>
            <td>{formatDateTime(order.placedOn)}</td>
            <td>{order.status.name}</td>
            <td>{order.statusReason || ''}</td>
            <td>
              <div class="d-flex">
                {#if !order.isFinalised}
                  {#if !order.hasOwnedLessons}
                    <button
                      type="button"
                      class="btn btn-primary btn-sm ms-auto"
                      on:click={async () => await completeOrder(order)}
                      ><i class="bi bi-hand-thumbs-up me-2"></i> Complete
                    </button>
                  {:else}
                    <span class="text-danger ms-auto"
                      ><i class="bi bi-exclamation-triangle"></i> Contains owned lessons</span>
                  {/if}
                {/if}
              </div>
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
  {#if state.pageInfo}
    <div class="row row-cols-1">
      <div class="col d-flex justify-content-center justify-content-md-start">
        <Pagination
          pageInfo={state.pageInfo}
          {onPageChange} />
      </div>
    </div>
  {/if}
{:else if !state.isLoadingOrders}
  <p class="text-primary">No orders match the criteria.</p>
{/if}
