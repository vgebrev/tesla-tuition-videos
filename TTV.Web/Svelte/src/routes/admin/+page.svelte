<script>
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';
  import Admin from '$components/Admin/Admin.svelte';
</script>

<svelte:head>
  <title>Tesla Tuition Videos - Admin Console</title>
</svelte:head>

<AuthorizeRoute authorizationPolicy="admin">
  <div slot="authorized">
    <Admin />
  </div>
</AuthorizeRoute>
