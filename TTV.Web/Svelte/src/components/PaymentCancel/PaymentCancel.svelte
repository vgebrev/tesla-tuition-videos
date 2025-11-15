<script>
  import { resolve } from '$app/paths';

  /** @type {number} */
  export let orderId;
</script>

<div class="container">
  <div class="row justify-content-center">
    <div class="col text-center">
      <h1 class="header-white">Payment Cancelled</h1>
    </div>
  </div>
  <div class="row">
    <div class="col text-center d-flex flex-column gap-3 mt-3">
      The payment for Order #{orderId} was cancelled.
      <a
        href={resolve('/checkout/[orderId]', { orderId: String(orderId) })}
        title="Contact us"
        class="card-link">Try again.</a>
    </div>
  </div>
</div>
