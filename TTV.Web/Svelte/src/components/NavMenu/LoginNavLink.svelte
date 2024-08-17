<script>
  import { userManager } from '$lib/auth.js';
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import LoginLink from '$components/common/LoginLink.svelte';

  async function logout() {
    sessionStorage.setItem('redirect', window.location.pathname);
    await userManager.signoutRedirect();
  }
</script>

<AuthorizeView>
  <div slot="authorized" let:user>
    <div class="nav-item dropdown">
      <a
        href="/"
        class="nav-link dropdown-toggle"
        data-bs-toggle="dropdown"
        role="button"
        aria-haspopup="true"
        aria-expanded="false"
        on:click|preventDefault={() => {}}
        ><i class="bi bi-person-circle"></i> {user.profile.name} <b class="caret"></b></a
      >

      <div class="dropdown-menu">
        <a href="/my/library" class="dropdown-item">Lesson Library</a>
        <a href="/my/orders" class="dropdown-item">Orders</a>
        <a href="/my/discount-vouchers" class="dropdown-item">Discount Vouchers</a>
        <hr class="dropdown-divider" />
        <button class="dropdown-item" type="button" on:click={logout}>Log out</button>
      </div>
    </div>
  </div>
  <div slot="anonymous">
    <div class="nav-item">
      <LoginLink class="nav-link" />
    </div>
  </div>
</AuthorizeView>
