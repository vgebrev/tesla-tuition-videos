<script>
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import { myDiscountVouchersStore } from '$components/My/DiscountVouchers/my-discount-vouchers.js';
  import DiscountVoucherList from '$components/My/DiscountVouchers/DiscountVoucherList.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';

  $: state = $myDiscountVouchersStore;
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Discount Vouchers</h1>
  </div>
</div>

<ProgressLoader
  isLoading={state.isLoading}
  class="mb-3" />
<DiscountVoucherList />
<ErrorCard error={state.error} />
