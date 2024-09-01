<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import DiscountVouchersIssue from '$components/Admin/DiscountVouchers/DiscountVouchersIssue.svelte';
  import DiscountVouchersList from '$components/Admin/DiscountVouchers/DiscountVouchersList.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import { adminDiscountVouchersStore } from '$components/Admin/DiscountVouchers/discount-vouchers.js';

  $: state = $adminDiscountVouchersStore;
</script>

<div class="card text-white bg-secondary mx-auto">
  <div class="card-body">
    <AuthorizeView authorizationPolicy="admin">
      <div slot="authorized">
        <DiscountVouchersIssue></DiscountVouchersIssue>
        <DiscountVouchersList></DiscountVouchersList>
      </div>
      <div slot="unauthorized">Insufficient Permissions</div>
    </AuthorizeView>
  </div>
</div>
<ErrorCard error={state.error} />
