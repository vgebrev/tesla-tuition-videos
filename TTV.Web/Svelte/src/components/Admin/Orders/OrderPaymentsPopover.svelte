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
<Popover
  triggerElem={popoverElem}
  title="Payments & Vouchers">
  <ul class="list-group list-group-flush">
    {#if order.payments.length > 0}
      {#each order.payments as payment (payment.id)}
        <li class="list-group-item list-group-item-action">
          {payment.status.name}
          {formatPaymentMethod(payment.paymentMethod)} Payment - R{payment.amount.toFixed(0)}
        </li>
      {/each}
    {/if}
    {#if order.appliedDiscounts.length > 0}
      {#each order.appliedDiscounts as appliedDiscount (appliedDiscount.voucherCode)}
        <li class="list-group-item list-group-item-action">
          Applied Voucher ({appliedDiscount.voucherCode}) - R{appliedDiscount.amount.toFixed(0)}
        </li>
      {/each}
    {/if}
  </ul>
  {#if order.appliedDiscounts.length === 0 && order.payments.length === 0}
    <span class="text-muted">No payments or discount vouchers</span>
  {/if}
</Popover>
