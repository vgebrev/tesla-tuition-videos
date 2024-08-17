<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
</script>

<AuthorizeView authorizationPolicy="admin">
  <a slot="authorized" class="nav-link" href="/admin"><i class="bi bi-gear"></i> Admin</a>
</AuthorizeView>
