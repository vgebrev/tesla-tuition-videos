<script>
  import Checkout from '$components/Checkout/Checkout.svelte';

  export let data;

  $: orderId = data.orderId;
</script>

<svelte:head>
  <title>Tesla Tuition Videos - Checkout</title>
</svelte:head>

<Checkout {orderId} />
