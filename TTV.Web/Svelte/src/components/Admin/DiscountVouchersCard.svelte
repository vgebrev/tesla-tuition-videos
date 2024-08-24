<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import DiscountVouchersIssue from '$components/Admin/DiscountVouchersIssue.svelte';
  import DiscountVouchersList from '$components/Admin/DiscountVouchersList.svelte';
</script>

<div class="card text-white bg-secondary mx-auto">
  <div class="card-header">Discount Vouchers</div>
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
