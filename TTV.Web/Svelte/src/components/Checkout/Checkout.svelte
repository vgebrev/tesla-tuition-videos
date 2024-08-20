<script>
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import OrderDetail from '$components/Checkout/OrderDetail.svelte';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import { onMount } from 'svelte';
  import { checkoutStore, checkoutActions } from '$components/Checkout/checkout.js';
  import ApplyDiscountVoucher from '$components/Checkout/ApplyDiscountVoucher.svelte';

  /** @type {int} */
  export let orderId;

  onMount(async () => {
    await checkoutActions.getOrder(orderId, state.order);
  });

  $: state = $checkoutStore;
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Checkout</h1>
  </div>
</div>

<ProgressLoader
  isLoading={state.isLoading}
  class="mb-3" />

{#if state.order}
  <div class="row">
    <div class="col-lg-6 col-sm-12 mb-3">
      <div class="row">
        <div class="col-12">
          <OrderDetail />
        </div>
      </div>
    </div>
    <div class="col-lg-6 col-sm-12">
      <div class="row">
        <div class="col-12 mb-3">
          <ApplyDiscountVoucher />
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <!--          <PaymentDetail Order="Order"></PaymentDetail>-->
        </div>
      </div>
    </div>
  </div>
{:else if !state.isLoading && !state.error.isError}
  <div class="row justify-content-center">
    <div class="card border-warning">
      <div class="card-body">
        Order #{orderId} not found.
      </div>
    </div>
  </div>
{/if}

{#if state.error.isError}
  <ErrorCard error={state.error} />
{/if}
