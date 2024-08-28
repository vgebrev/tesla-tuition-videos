<script>
  import ErrorView from '$components/common/ErrorView.svelte';
</script>

<ErrorView
  status={500}
  errorMessage="Unexpected login error" />
