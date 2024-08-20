<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import NavLink from '$components/NavMenu/NavLink.svelte';
</script>

<AuthorizeView authorizationPolicy="admin">
  <NavLink
    href="/admin"
    slot="authorized"><i class="bi bi-gear"></i> Admin</NavLink>
</AuthorizeView>
