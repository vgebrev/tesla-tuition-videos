<script>
  import { authStore, userManager } from '$lib/auth.js';
  import { goto } from '$app/navigation';

  async function login() {
    authStore.update((state) => {
      state.isLoading = true;
      return state;
    });
    sessionStorage.setItem('redirect', window.location.pathname);
    try {
      await userManager.signinSilent();
    } catch {
      try {
        await userManager.signinRedirect();
      } catch (e) {
        console.error(e);
        authStore.update((state) => {
          state.isLoading = false;
          return state;
        });
        goto('/authentication/error', { replaceState: true });
      }
    }
  }
</script>

<a
  href="/"
  {...$$restProps}
  on:click|preventDefault={login}><slot>Log in</slot></a>
