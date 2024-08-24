<script>
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import { adminStore } from '$components/Admin/admin.js';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import DiscountVouchersCard from '$components/Admin/DiscountVouchersCard.svelte';

  $: state = $adminStore;
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Admin Console</h1>
  </div>
</div>

<ProgressLoader isLoading={state.isLoading} />
<div class="row justify-content-center my-3">
  <div class="col-auto">
    <DiscountVouchersCard />
  </div>
</div>
<ErrorCard error={state.error} />
