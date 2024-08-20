<script>
  import { checkoutStore, checkoutActions } from '$components/Checkout/checkout.js';
  import ResultCard from '$components/common/ResultCard.svelte';

  /** @type {string | null} */
  let code = null;
  let validated = false;

  function applyVoucher() {
    validated = true;
    if (!isValid || !order) return;
    checkoutActions.applyVoucher(order.id, code);
  }

  $: order = $checkoutStore.order;
  $: applyVoucherResult = $checkoutStore.applyVoucherResult;
  $: isValid = !!code;
</script>

<div class="card bg-secondary">
  <div class="card-header">Discount Voucher</div>
  <div class="card-body">
    {#if order}
      <form
        on:submit|preventDefault={applyVoucher}
        novalidate>
        <fieldset disabled={order.totalAmount === 0}>
          <div class="row form-group">
            <div class="col-2 col-md-2 mb-2">
              <label
                for="apply-voucher"
                class="col-form-label">Code</label>
            </div>
            <div class="col-10 col-md-6 mb-2">
              <input
                bind:value={code}
                id="apply-voucher"
                class="form-control"
                class:is-invalid={validated && !isValid}
                class:is-valid={validated && isValid}
                placeholder="Code"
                required />
              <div
                class:is-invalid={validated && !isValid}
                class="text-danger invalid-feedback">
                Code is required
              </div>
            </div>
            <div class="col-12 col-md-4 text-center mb-3">
              <button
                class="btn btn-outline-primary"
                type="submit"><i class="bi bi-check me-1"></i> Apply</button>
            </div>
          </div>
        </fieldset>
      </form>
      <ResultCard result={applyVoucherResult}></ResultCard>
    {/if}
  </div>
</div>
