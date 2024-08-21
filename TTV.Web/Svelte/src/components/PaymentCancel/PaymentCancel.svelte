<script>
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
    <div class="col text-center">
      The payment for Order #{orderId} was cancelled.
      <a
        href="/checkout/{orderId}"
        title="Contact us"
        class="card-link">Try again.</a>
    </div>
  </div>
</div>
