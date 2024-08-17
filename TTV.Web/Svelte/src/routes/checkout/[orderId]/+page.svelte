<script>
  import Checkout from '$components/Checkout/Checkout.svelte';
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';

  export let data;

  $: orderId = data.orderId;
</script>

<svelte:head>
  <title>Tesla Tuition Videos - Checkout</title>
</svelte:head>

<AuthorizeRoute>
  <Checkout {orderId} />
</AuthorizeRoute>
