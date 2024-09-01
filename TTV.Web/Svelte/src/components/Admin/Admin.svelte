<script>
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import { adminDiscountVouchersStore } from '$components/Admin/DiscountVouchers/discount-vouchers.js';
  import { adminOrdersStore } from '$components/Admin/Orders/orders.js';
  import DiscountVouchersCard from '$components/Admin/DiscountVouchers/DiscountVouchersCard.svelte';
  import OrdersCard from '$components/Admin/Orders/OrdersCard.svelte';

  $: isLoading =
    $adminDiscountVouchersStore.isLoading ||
    $adminOrdersStore.isLoadingOrders ||
    $adminOrdersStore.isLoadingUsers ||
    $adminOrdersStore.isLoadingStatuses;
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Admin Console</h1>
  </div>
</div>

<ProgressLoader {isLoading} />
<div class="row justify-content-center my-3">
  <div class="col-auto">
    <ul
      class="nav nav-pills mb-3 gap-3"
      id="admin-tabs"
      role="tablist">
      <li
        class="nav-item"
        role="presentation">
        <button
          class="nav-link active"
          id="order-tab"
          data-bs-toggle="tab"
          data-bs-target="#order-content"
          type="button"
          role="tab"
          aria-controls="orders"
          aria-selected="false">Orders</button>
      </li>
      <li
        class="nav-item"
        role="presentation">
        <button
          class="nav-link"
          id="discount-voucher-tab"
          data-bs-toggle="pill"
          data-bs-target="#discount-voucher-content"
          type="button"
          role="tab"
          aria-controls="discount-voucher"
          aria-selected="true">Discount Vouchers</button>
      </li>
    </ul>
    <div
      class="tab-content"
      id="admin-tab-content">
      <div
        class="tab-pane fade show active"
        id="order-content"
        role="tabpanel"
        aria-labelledby="order-tab">
        <OrdersCard />
      </div>
      <div
        class="tab-pane fade"
        id="discount-voucher-content"
        role="tabpanel"
        aria-labelledby="discount-voucher-tab">
        <DiscountVouchersCard />
      </div>
    </div>
  </div>
</div>
