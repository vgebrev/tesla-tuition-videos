<script>
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';
  import PaymentCancel from '$components/PaymentCancel/PaymentCancel.svelte';

  export let data;

  $: orderId = data.orderId;
</script>

<svelte:head>
  <title>Tesla Tuition Videos - Payment Cancelled</title>
</svelte:head>

<AuthorizeRoute>
  <div slot="authorized">
    <PaymentCancel {orderId} />
  </div>
</AuthorizeRoute>
