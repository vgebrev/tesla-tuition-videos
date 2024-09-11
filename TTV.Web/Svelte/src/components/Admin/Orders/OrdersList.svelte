<script>
  import { adminOrdersStore, adminOrdersActions } from '$components/Admin/Orders/orders.js';
  import { onDestroy, onMount } from 'svelte';
  import { formatDateTime } from '$lib/util.js';
  import Popover from '$components/common/Popover.svelte';
  import { formatPaymentMethod } from '$components/Checkout/checkout.js';

  /** @type HTMLButtonElement[] */
  let lessonPopoverButtons = [];

  /** @type HTMLButtonElement[] */
  let paymentPopoverButtons = [];

  const storeUnsub = adminOrdersStore.subscribe((data) => {
    lessonPopoverButtons = new Array(data.orders?.length || 0);
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

  /**
   * Get the icon class for the given provider.
   * @param {string} provider
   * @returns {string}
   */
  function getProviderIconClass(provider) {
    const icons = {
      'TTV Account': 'bi bi-person',
      Google: 'bi bi-google'
    };
    return icons[provider] || '';
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
                <div class="ms-auto">
                  <button
                    class="btn btn-link pt-0"
                    bind:this={lessonPopoverButtons[i]}><i class="bi bi-info-circle"></i></button
                  ><Popover triggerElem={lessonPopoverButtons[i]}>
                    {#if order.lessons.length > 0}
                      <h6 class="px-2">Order Items</h6>
                      <ul class="list-group list-group-flush">
                        {#each order.lessons as lesson, i (lesson.id)}
                          <li
                            class="list-group-item"
                            class:bg-secondary={i % 2 !== 0}>
                            {lesson.title}
                          </li>
                        {/each}
                      </ul>{:else}
                      No items
                    {/if}</Popover>
                </div>
              </div></td>
            <td
              ><div class="d-flex align-items-start">
                R{order.paymentsTotal.toFixed(0)}
                <div class="ms-auto">
                  <button
                    class="btn btn-link pt-0"
                    bind:this={paymentPopoverButtons[i]}><i class="bi bi-info-circle"></i></button
                  ><Popover triggerElem={paymentPopoverButtons[i]}>
                    {#if order.payments.length > 0}
                      <h6 class="px-2">Payments</h6>
                      <ul class="list-group list-group-flush">
                        {#each order.payments as payment, i (payment.id)}
                          <li
                            class="list-group-item"
                            class:bg-secondary={i % 2 !== 0}>
                            {formatPaymentMethod(payment.paymentMethod)} - R{payment.amount.toFixed(0)} - {payment
                              .status.name}
                          </li>
                        {/each}
                      </ul>{/if}
                    {#if order.appliedDiscounts.length > 0}
                      <h6 class="px-2">Discount Vouchers</h6>
                      <ul class="list-group list-group-flush">
                        {#each order.appliedDiscounts as appliedDiscount, i (appliedDiscount.voucherCode)}
                          <li
                            class="list-group-item"
                            class:bg-secondary={i % 2 !== 0}>
                            {appliedDiscount.voucherCode} - R{appliedDiscount.amount.toFixed(0)}
                          </li>
                        {/each}
                      </ul>{/if}
                    {#if order.appliedDiscounts.length === 0 && order.payments.length === 0}
                      No payments or discount vouchers
                    {/if}
                  </Popover>
                </div>
              </div></td>
            <td>R{order.totalAmount.toFixed(0)}</td>
            <td>
              <i
                class={getProviderIconClass(order.placedBy.provider)}
                title={order.placedBy.provider}></i>
              {order.placedBy.name}
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
