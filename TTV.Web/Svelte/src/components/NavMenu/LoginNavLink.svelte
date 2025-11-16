<script>
  import { authStore, userManager } from '$lib/auth.js';
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import LoginLink from '$components/common/LoginLink.svelte';
  import NavLink from '$components/NavMenu/NavLink.svelte';
  import { resolve } from '$app/paths';

  async function logout() {
    authStore.update((state) => {
      state.isLoading = true;
      return state;
    });
    sessionStorage.setItem('redirect', window.location.pathname);
    await userManager.signoutRedirect();
  }
</script>

<AuthorizeView>
  <div
    slot="authorized"
    let:user>
    <div class="nav-item dropdown">
      <a
        href={resolve('/')}
        class="nav-link dropdown-toggle"
        data-bs-toggle="dropdown"
        role="button"
        aria-haspopup="true"
        aria-expanded="false"
        on:click|preventDefault={() => {}}
        ><i class="bi bi-person-circle"></i> {user?.profile?.name || 'Account'} <b class="caret"></b></a>

      <div class="dropdown-menu">
        <NavLink
          href="/my/library"
          cssClass="dropdown-item">Lesson Library</NavLink>
        <NavLink
          href="/my/orders"
          cssClass="dropdown-item">Orders</NavLink>
        <NavLink
          href="/my/discount-vouchers"
          cssClass="dropdown-item">Discount Vouchers</NavLink>
        <hr class="dropdown-divider" />
        <button
          class="dropdown-item"
          type="button"
          on:click={logout}
          data-bs-toggle="collapse"
          data-bs-target=".navbar-collapse.show">Log out</button>
      </div>
    </div>
  </div>
  <div slot="anonymous">
    <div class="nav-item">
      <LoginLink
        class="nav-link"
        data-bs-toggle="collapse"
        data-bs-target=".navbar-collapse.show" />
    </div>
  </div>
</AuthorizeView>
