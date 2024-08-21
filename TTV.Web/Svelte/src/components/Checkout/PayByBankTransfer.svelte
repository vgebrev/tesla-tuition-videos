<script>
  import { config } from '$lib/config.js';

  /** @type {number} */
  export let orderId;
</script>

<p>To complete your order, please make a deposit to the following account:</p>

<div class="bg-dark p-2 mb-2">
  <div class="row">
    <div class="col-12 col-sm-4 col-lg-5">Bank:</div>
    <div class="col-12 col-sm-8 col-lg-7"><strong>{config.bankAccount.bankName}</strong></div>
  </div>
  <div class="row">
    <div class="col-12 col-sm-4 col-lg-5">Account Number:</div>
    <div class="col-12 col-sm-8 col-lg-7"><strong>{config.bankAccount.accountNumber}</strong></div>
  </div>
  <div class="row">
    <div class="col-12 col-sm-4 col-lg-5">Branch Code:</div>
    <div class="col-12 col-sm-8 col-lg-7"><strong>{config.bankAccount.branchCode}</strong></div>
  </div>
</div>

<p>
  <small>
    Please include your order number <strong>{orderId}</strong> in the reference.
  </small>
</p>
<p>
  <small>
    <strong>NOTE:</strong> It might take up to 2 business days for the deposit to reflect and your order to be completed.
  </small>
</p>
