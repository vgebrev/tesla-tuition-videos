<script>
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';
  import OrderComplete from '$components/OrderComplete/OrderComplete.svelte';

  export let data;

  $: orderId = data.orderId;
</script>

<svelte:head>
  <title>Tesla Tuition Videos - Order Complete</title>
</svelte:head>

<AuthorizeRoute>
  <div slot="authorized">
    <OrderComplete {orderId} />
  </div>
</AuthorizeRoute>
