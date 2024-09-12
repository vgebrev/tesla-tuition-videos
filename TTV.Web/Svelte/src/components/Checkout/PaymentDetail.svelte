<script>
  import { checkoutActions, checkoutStore, paymentMethods } from '$components/Checkout/checkout.js';
  import Modal from '$components/common/Modal.svelte';
  import PayByBankTransfer from '$components/Checkout/PayByBankTransfer.svelte';
  import { goto } from '$app/navigation';
  import PayfastPaymentMethodsPopover from '$components/Checkout/PayfastPaymentMethodsPopover.svelte';

  let paymentMethod = paymentMethods.payfast;

  /** @type {import('$lib/types').Order} */
  export let order;
  let isEftModalOpen = isBankTransferPaymentInitiated();

  function isBankTransferPaymentInitiated() {
    const initiatePaymentResult = $checkoutStore.initiatePaymentResult;
    const latestPayment = initiatePaymentResult?.value;
    return (
      !!latestPayment &&
      latestPayment?.paymentMethod === paymentMethods.bankTransfer &&
      latestPayment?.paymentMethod === paymentMethod
    );
  }

  function completeOrder() {
    goto(`/order-complete/${order.id}`);
  }

  async function orderPayment() {
    await checkoutActions.initiatePayment(order.id, paymentMethod);
    isEftModalOpen = isBankTransferPaymentInitiated();
  }
</script>

<div class="card bg-secondary">
  <div class="card-header">Payment Method</div>
  <div class="card-body">
    <form>
      <div class="row">
        <div class="col-12 col-md-6 mb-2 text-center">
          <div class="form-check disabled">
            <label
              class="form-check-label"
              for="payfast">
              <input
                type="radio"
                bind:group={paymentMethod}
                class="form-check-input"
                id="payfast"
                value={paymentMethods.payfast} />
              <i class="bi bi-credit-card"></i> Payfast
            </label>
          </div>
          <small class="text-sm text-primary">(Credit Card, Debit Card, or Zapper)</small>
          <PayfastPaymentMethodsPopover />
        </div>
        <div class="col-12 col-md-6 mb-2 text-center">
          <div class="form-check">
            <label
              class="form-check-label"
              for="eft">
              <input
                type="radio"
                bind:group={paymentMethod}
                class="form-check-input"
                id="eft"
                value={paymentMethods.bankTransfer} />
              <i class="bi bi-bank"></i> Bank Transfer
            </label>
          </div>
        </div>
      </div>
    </form>
  </div>
  <div class="card-footer">
    <div class="row justify-content-center">
      {#if order.canComplete}
        <div class="col-12 col-sm-6 col-md-4 text-center mb-2">
          <button
            type="button"
            class="btn btn-primary btn-lg"
            on:click={completeOrder}><i class="bi bi-hand-thumbs-up me-2"></i> Complete</button>
        </div>
      {/if}

      {#if order.isPayable}
        <div class="col-12 col-sm-6 col-md-4 text-center mb-2">
          <button
            type="button"
            class="btn btn-primary btn-lg"
            on:click={orderPayment}><i class="bi bi-credit-card me-2"></i> Pay Now</button>
        </div>
      {/if}
    </div>

    <div class="row">
      {#if !order.canComplete && !order.isPayable}
        <div class="col-12">
          <div class="mb-2">Payment is not possible:</div>
          {#if order.hasOwnedLessons}
            <div class="card border-danger mb-2">
              <div class="card-body">You already own one or more of the lessons in this order.</div>
            </div>
          {/if}
          {#if order.isFinalised}
            <div class="card border-danger">
              <div class="card-body">
                The order has a status of "{order.status.name}".
              </div>
            </div>
          {/if}
        </div>
      {/if}
    </div>
  </div>
</div>

<Modal
  bind:isOpen={isEftModalOpen}
  title="Bank Transfer">
  <PayByBankTransfer orderId={order.id} />
</Modal>
