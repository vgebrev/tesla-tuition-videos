<script>
  import { formatPaymentMethod } from '$components/Checkout/checkout.js';
  import Popover from '$components/common/Popover.svelte';

  /** @type {import('$lib/types').Order} */
  export let order;

  /** @type {HTMLElement} */
  let popoverElem;
</script>

<button
  class="btn btn-link pt-0"
  bind:this={popoverElem}>
  <i class="bi bi-info-circle"></i>
</button>
<Popover triggerElem={popoverElem}>
  {#if order.payments.length > 0}
    <h6 class="px-2">Payments</h6>
    <ul class="list-group list-group-flush">
      {#each order.payments as payment, i (payment.id)}
        <li
          class="list-group-item"
          class:bg-secondary={i % 2 !== 0}>
          {formatPaymentMethod(payment.paymentMethod)} - R{payment.amount.toFixed(0)} - {payment.status.name}
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
