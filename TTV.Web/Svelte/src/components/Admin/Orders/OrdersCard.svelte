<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import OrdersFilter from '$components/Admin/Orders/OrdersFilter.svelte';
  import OrdersList from '$components/Admin/Orders/OrdersList.svelte';
  import { adminOrdersStore } from '$components/Admin/Orders/orders.js';

  $: error = $adminOrdersStore.ordersError || $adminOrdersStore.usersError || $adminOrdersStore.statusesError;
</script>

<div class="card text-white bg-secondary mx-auto">
  <div class="card-body">
    <AuthorizeView authorizationPolicy="admin">
      <div slot="authorized">
        <OrdersFilter />
        <OrdersList />
      </div>
      <div slot="unauthorized">Insufficient Permissions</div>
    </AuthorizeView>
  </div>
</div>

<ErrorCard {error} />
