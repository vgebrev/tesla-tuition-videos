<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import DiscountVoucherIssue from '$components/Admin/DiscountVouchers/DiscountVoucherIssue.svelte';
  import DiscountVoucherList from '$components/Admin/DiscountVouchers/DiscountVoucherList.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import { adminDiscountVouchersStore } from '$components/Admin/DiscountVouchers/discount-vouchers.js';

  $: state = $adminDiscountVouchersStore;
</script>

<div class="card text-white bg-secondary mx-auto">
  <div class="card-body">
    <AuthorizeView authorizationPolicy="admin">
      <div slot="authorized">
        <DiscountVoucherIssue></DiscountVoucherIssue>
        <DiscountVoucherList></DiscountVoucherList>
      </div>
      <div slot="unauthorized">Insufficient Permissions</div>
    </AuthorizeView>
  </div>
</div>
<ErrorCard error={state.error} />
