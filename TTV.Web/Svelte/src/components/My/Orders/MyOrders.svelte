<script>
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import { myOrdersStore } from '$components/My/Orders/my-orders.js';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import OrderList from '$components/My/Orders/OrderList.svelte';
  $: state = $myOrdersStore;
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Order History</h1>
  </div>
</div>

<ProgressLoader isLoading={state.isLoading} />
<OrderList />
<ErrorCard error={state.error} />
