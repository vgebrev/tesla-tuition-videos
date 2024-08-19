<script>
  import { checkoutStore, checkoutActions } from '$components/Checkout/checkout.js';
  import ResultCard from '$components/common/ResultCard.svelte';

  function userOwnsLesson(lesson) {
    if (state.order == null || lesson.owner == null) {
      return false;
    }
    return lesson.owner.id === state.order.placedBy.id;
  }

  function formatPaymentMethod(paymentMethod) {
    switch (paymentMethod) {
      case 1:
        return 'ManualBankTransfer';
      case 2:
        return 'Payfast';
      case 3:
        return 'PayPal';
    }
  }

  function cancelOrder() {
    if (!state.order) return;
    checkoutActions.cancelOrder(state.order.id);
  }

  /** @type {CheckoutStoreState} */
  $: state = $checkoutStore;
  $: orderTotal = state.order
    ? state.order.lessons.reduce((sum, lesson) => sum + lesson.currentPrice.effectiveAmount, 0) -
      state.order.appliedDiscounts.reduce((sum, d) => sum + d.amount, 0) -
      state.order.payments
        .filter((p) => p.status.name === 'Paid')
        .reduce((sum, p) => sum + p.amount, 0)
    : 0;
</script>

{#if state.order}
  <div class="card bg-secondary">
    <div class="card-header">Order #{state.order.id} Details</div>
    <div class="card-body">
      <table class="table">
        <thead>
          <tr>
            <th>Item</th>
            <th>Amount</th>
          </tr>
        </thead>
        <tbody>
          {#each state.order.lessons as lesson, i (lesson.id)}
            <tr>
              <td>
                {lesson.title}
                {#if userOwnsLesson(lesson)}
                  <small class="text-danger"> (You already own this lesson)</small>
                {/if}
              </td>
              <td>R{lesson.currentPrice.effectiveAmount.toFixed(0)}</td>
            </tr>
          {/each}
          <tr class="table-secondary border-primary border-bottom border-1">
            <th class="text-end">Sub Total</th>
            <th
              >R{state.order.lessons
                .reduce((sum, lesson) => sum + lesson.currentPrice.effectiveAmount, 0)
                .toFixed(0)}</th
            >
          </tr>
        </tbody>
        {#if state.order.payments.some((p) => p.status.name === 'Paid')}
          <tbody>
            {#each state.order.payments.filter((p) => p.status.name === 'Paid') as payment, i (payment.id)}
              <tr>
                <td>{formatPaymentMethod(payment.paymentMethod)} Payment</td>
                <td>-R{payment.amount.toFixed(0)}</td>
              </tr>
            {/each}
            <tr class="table-secondary border-primary border-bottom border-1">
              <th class="text-end">Sub Total</th>
              <th
                >-R{state.order.payments
                  .filter((p) => p.status.name === 'Paid')
                  .reduce((sum, p) => sum + p.amount, 0)
                  .toFixed(0)}</th
              >
            </tr>
          </tbody>
        {/if}
        {#if state.order.appliedDiscounts.length > 0}
          <tbody>
            {#each state.order.appliedDiscounts as discount, i (discount.voucherCode)}
              <tr>
                <td>Discount Voucher {discount.voucherCode}</td>
                <td>-R{discount.amount.toFixed(0)}</td>
              </tr>
            {/each}
            <tr class="table-secondary border-primary border-bottom border-1">
              <th class="text-end">Sub Total</th>
              <th
                >-R{state.order.appliedDiscounts
                  .reduce((sum, d) => sum + d.Amount, 0)
                  .toFixed(0)}</th
              >
            </tr>
          </tbody>
        {/if}
        <tfoot>
          <tr class="table-secondary border-primary border-bottom border-2">
            <th class="text-end">Total</th>
            <th>R{orderTotal.toFixed(0)}</th>
          </tr>
        </tfoot>
      </table>
    </div>
    <div class="card-footer row">
      {#if !state.order.isFinalised}
        <div class="col-12 mb-2">
          <button type="button" class="btn btn-outline-primary" on:click={cancelOrder}
            ><i class="bi bi-trash me-2"></i> Cancel</button
          >
          <small class="text-muted ms-2">Applied discount vouchers will be refunded.</small>
        </div>
      {/if}
      {#if state.cancelOrderResult}
        <div class="col-12">
          <ResultCard result={state.cancelOrderResult}></ResultCard>
        </div>
      {/if}
    </div>
  </div>
{/if}
