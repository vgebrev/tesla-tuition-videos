<script>
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';
  import Admin from '$components/Admin/Admin.svelte';
</script>

<AuthorizeRoute authorizationPolicy="admin">
  <div slot="authorized">
    <Admin />
  </div>
</AuthorizeRoute>
