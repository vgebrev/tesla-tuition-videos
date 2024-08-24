<script>
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';
  import MyOrders from '$components/My/Orders/MyOrders.svelte';
</script>

<svelte:head>
  <title>Tesla Tuition Videos - My Orders</title>
</svelte:head>

<AuthorizeRoute>
  <div slot="authorized">
    <MyOrders />
  </div>
</AuthorizeRoute>
