<script>
  import { config } from '$lib/config.js';
  import AuthorizeRoute from '$components/common/AuthorizeRoute.svelte';
</script>

<AuthorizeRoute authorizationPolicy="admin">
  <div slot="authorized">
    <pre>{JSON.stringify(config, null, 2)}</pre>
  </div>
</AuthorizeRoute>
