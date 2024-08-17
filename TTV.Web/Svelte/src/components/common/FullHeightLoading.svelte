<script>
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
</script>

<div class="min-vh-100-6rem d-flex flex-column justify-content-center border-1 border-white">
  <h3 class="header-white">Loading...</h3>
  <ProgressLoader isLoading={true}></ProgressLoader>
</div>
