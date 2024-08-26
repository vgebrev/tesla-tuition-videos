<script>
  import { page } from '$app/stores';
  import ErrorView from '$components/common/ErrorView.svelte';
</script>

<ErrorView
  status={$page.status}
  errorMessage={$page.error?.message} />
