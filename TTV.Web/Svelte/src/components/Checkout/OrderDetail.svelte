<script>
  import ResultCard from '$components/common/ResultCard.svelte';
  import { checkoutStore, checkoutActions } from '$components/Checkout/checkout.js';
  import { sum } from '$lib/util.js';

  function userOwnsLesson(lesson) {
    if (state.order === null || lesson.owner == null) {
      return false;
    }
    return lesson.owner.id === state.order.placedBy.id;
  }

  function formatPaymentMethod(paymentMethod) {
    switch (paymentMethod) {
      case 1:
        return 'Manual Bank Transfer';
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
    ? sum(state.order.lessons, (l) => l.currentPrice.effectiveAmount) -
      sum(state.order.appliedDiscounts, (d) => d.amount) -
      sum(
        state.order.payments.filter((p) => p.status.name === 'Paid'),
        (p) => p.amount
      )
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
          {#each state.order.lessons as lesson (lesson.id)}
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
            <th>R{sum(state.order.lessons, (l) => l.currentPrice.effectiveAmount).toFixed(0)}</th>
          </tr>
        </tbody>
        {#if state.order.payments.some((p) => p.status.name === 'Paid')}
          <tbody>
            {#each state.order.payments.filter((p) => p.status.name === 'Paid') as payment (payment.id)}
              <tr>
                <td>{formatPaymentMethod(payment.paymentMethod)} Payment</td>
                <td>-R{payment.amount.toFixed(0)}</td>
              </tr>
            {/each}
            <tr class="table-secondary border-primary border-bottom border-1">
              <th class="text-end">Sub Total</th>
              <th
                >-R{sum(
                  state.order.payments.filter((p) => p.status.name === 'Paid'),
                  (p) => p.amount
                ).toFixed(0)}</th
              >
            </tr>
          </tbody>
        {/if}
        {#if state.order.appliedDiscounts.length > 0}
          <tbody>
            {#each state.order.appliedDiscounts as discount (discount.voucherCode)}
              <tr>
                <td>Discount Voucher {discount.voucherCode}</td>
                <td>-R{discount.amount.toFixed(0)}</td>
              </tr>
            {/each}
            <tr class="table-secondary border-primary border-bottom border-1">
              <th class="text-end">Sub Total</th>
              <th>-R{sum(state.order.appliedDiscounts, (d) => d.amount).toFixed(0)}</th>
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
