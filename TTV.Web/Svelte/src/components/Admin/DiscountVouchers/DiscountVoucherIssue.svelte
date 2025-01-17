<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import {
    adminDiscountVouchersStore,
    adminDiscountVouchersActions
  } from '$components/Admin/DiscountVouchers/discount-vouchers.js';

  async function issueVoucher() {
    if (!model.amount.isValid() || !model.expirationDate.isValid() || !model.note.isValid()) {
      return;
    }
    await adminDiscountVouchersActions.issueDiscountVoucher(
      model.amount.value || 0,
      model.expirationDate.value,
      model.note.value
    );
    model.amount.value = null;
    model.expirationDate.value = null;
    model.note.value = null;
  }

  /** @type {{ amount: { value: number | null, isValid: () => boolean }, note: { value: string | null, isValid: () => boolean }, expirationDate: { value: Date | string | null, isValid: () => boolean } }} */
  const model = {
    amount: { value: null, isValid: () => model.amount.value !== null && model.amount.value > 0 },
    note: { value: null, isValid: () => true },
    expirationDate: {
      value: null,
      isValid: () =>
        model.expirationDate.value === null ||
        model.expirationDate.value === '' ||
        !isNaN(new Date(model.expirationDate.value).getMilliseconds())
    }
  };

  $: state = $adminDiscountVouchersStore;
</script>

<AuthorizeView authorizationPolicy="canIssueVouchers">
  <div slot="authorized">
    <h4 class="card-title">Issue a voucher</h4>
    <div class="card-text border-bottom border-1 border-dark mb-3">
      <form
        on:submit|preventDefault={issueVoucher}
        novalidate>
        <div class="row">
          <div class="form-group mb-2 col-12 col-md-6 col-lg-3">
            <label for="voucher-amount">Amount</label>
            <input
              type="number"
              bind:value={model.amount.value}
              class="form-control"
              class:is-invalid={!model.amount.isValid()}
              class:is-valid={model.amount.isValid()}
              min="0"
              id="voucher-amount"
              required />
            <div
              class:is-invalid={model.amount.isValid()}
              class="text-danger invalid-feedback">
              A positive amount is required
            </div>
          </div>
          <div class="form-group mb-2 col-12 col-md-6 col-lg-3">
            <label for="voucher-expiration-date">Expiration Date <span class="text-muted">(Optional)</span></label>
            <input
              type="date"
              bind:value={model.expirationDate.value}
              class="form-control"
              class:is-invalid={!model.expirationDate.isValid()}
              class:is-valid={model.expirationDate.isValid()}
              id="voucher-expiration-date" />
            <div
              class:is-invalid={model.expirationDate.isValid()}
              class="text-danger invalid-feedback">
              Invalid date
            </div>
          </div>
          <div class="form-group mb-2 col-12 col-md-6 col-lg-3">
            <label for="voucher-note">Note <span class="text-muted">(Optional)</span></label>
            <input
              type="text"
              bind:value={model.note.value}
              class="form-control"
              class:is-invalid={!model.note.isValid()}
              class:is-valid={model.note.isValid()}
              id="voucher-note" />
            <div
              class:is-invalid={model.note.isValid()}
              class="text-danger invalid-feedback">
              Note is invalid
            </div>
          </div>
          <div class="mb-3 pt-4 col-12 col-md-6 col-lg-3">
            <button
              type="submit"
              class="btn btn-primary"><i class="bi bi-ticket-perforated me-2"></i> Issue Voucher</button>
          </div>
        </div>
      </form>
    </div>

    {#if state.issuedVoucher}
      <div class="d-flex justify-content-center">
        <div
          class="alert alert-primary alert-dismissible col-7"
          role="alert">
          <button
            type="button"
            class="btn-close"
            aria-label="Close alert"
            data-bs-dismiss="alert"></button>
          <strong>Voucher Issued!</strong>
          <p>
            A voucher for <strong>R{state.issuedVoucher.amount.toFixed(0)}</strong> was issued. The voucher code is
            <strong class="border border-1 bg-secondary p-1">{state.issuedVoucher.code}</strong>.
          </p>
        </div>
      </div>
    {/if}
  </div>
</AuthorizeView>
