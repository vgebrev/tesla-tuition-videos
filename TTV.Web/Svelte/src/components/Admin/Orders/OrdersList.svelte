<script>
  import { adminOrdersStore, adminOrdersActions } from '$components/Admin/Orders/orders.js';
  import { onDestroy, onMount } from 'svelte';
  import { formatDateTime } from '$lib/util.js';
  import Popover from '$components/common/Popover.svelte';
  import { formatPaymentMethod } from '$components/Checkout/checkout.js';
  import UserDisplay from '$components/Admin/UserDisplay.svelte';
  import OrderItemsPopover from '$components/Admin/Orders/OrderItemsPopover.svelte';
  import OrderPaymentsPopover from '$components/Admin/Orders/OrderPaymentsPopover.svelte';

  /** @type HTMLButtonElement[] */
  let paymentPopoverButtons = [];

  const storeUnsub = adminOrdersStore.subscribe((data) => {
    paymentPopoverButtons = new Array(data.orders?.length || 0);
  });

  onMount(async () => {
    await adminOrdersActions.getOrders();
  });

  onDestroy(() => {
    storeUnsub();
  });
  /**
   * Complete the given order.
   * @param {import('$lib/types').Order} order
   */
  async function completeOrder(order) {
    await adminOrdersActions.completeOrder(order);
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
            <td
              ><div class="d-flex align-items-start">
                R{order.orderTotal.toFixed(0)}
                <div class="ms-auto"><OrderItemsPopover {order} /></div>
              </div></td>
            <td
              ><div class="d-flex align-items-start">
                R{order.paymentsTotal.toFixed(0)}
                <div class="ms-auto"><OrderPaymentsPopover {order} /></div>
              </div></td>
            <td>R{order.totalAmount.toFixed(0)}</td>
            <td>
              <UserDisplay user={order.placedBy} />
            </td>
            <td>{formatDateTime(order.placedOn)}</td>
            <td>{order.status.name}</td>
            <td>{order.statusReason || ''}</td>
            <td>
              {#if !order.isFinalised}
                <button
                  type="button"
                  class="btn btn-primary btn-sm"
                  on:click={async () => await completeOrder(order)}
                  ><i class="bi bi-hand-thumbs-up me-2"></i> Complete</button
                >{/if}</td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
{:else if !state.isLoadingOrders}
  <p>No orders match the criteria.</p>
{/if}
