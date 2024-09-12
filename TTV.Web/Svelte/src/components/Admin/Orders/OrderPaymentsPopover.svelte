<script>
  import { formatPaymentMethod } from '$components/Checkout/checkout.js';
  import Popover from '$components/common/Popover.svelte';
  import { formatDateTime } from '$lib/util.js';

  /** @type {import('$lib/types').Order} */
  export let order;

  /** @type {HTMLElement} */
  let popoverElem;

  const paymentStatuses = {
    pending: 1,
    paid: 2,
    failed: 3,
    cancelled: 4
  };
</script>

<button
  class="btn btn-link p-0 me-1"
  bind:this={popoverElem}>
  <i class="bi bi-info-circle"></i>
</button>
<Popover
  triggerElem={popoverElem}
  title="Payments & Vouchers">
  <ul class="list-group list-group-flush">
    {#if order.payments.length > 0}
      {#each order.payments as payment (payment.id)}
        <li
          class="list-group-item list-group-item-action"
          class:text-danger={payment.status.id === paymentStatuses.failed}
          class:text-muted={payment.status.id === paymentStatuses.cancelled}
          class:text-success={payment.status.id === paymentStatuses.paid}>
          {formatDateTime(payment.createdOn)} -
          {payment.status.name}
          {formatPaymentMethod(payment.paymentMethod)} Payment - R{payment.amount.toFixed(0)}
        </li>
      {/each}
    {/if}
    {#if order.appliedDiscounts.length > 0}
      {#each order.appliedDiscounts as appliedDiscount (appliedDiscount.voucherCode)}
        <li class="list-group-item list-group-item-action text-success">
          {formatDateTime(appliedDiscount.usedAt)} - Applied Voucher ({appliedDiscount.voucherCode}) - R{appliedDiscount.amount.toFixed(
            0
          )}
        </li>
      {/each}
    {/if}
  </ul>
  {#if order.appliedDiscounts.length === 0 && order.payments.length === 0}
    <span class="text-muted">No payments or discount vouchers</span>
  {/if}
</Popover>
