<script>
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';
  import MyDiscountVouchers from '$components/My/DiscountVouchers/MyDiscountVouchers.svelte';
</script>

<svelte:head>
  <title>Tesla Tuition Videos - My Discount Vouchers</title>
</svelte:head>

<AuthorizeRoute>
  <div slot="authorized">
    <MyDiscountVouchers />
  </div>
</AuthorizeRoute>
