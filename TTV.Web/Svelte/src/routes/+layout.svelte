<script>
  import NavMenu from '$components/NavMenu/NavMenu.svelte';
  import AppFooter from '$components/AppFooter.svelte';
  import CookieConsent from '$components/CookieConsent.svelte';
  import { afterNavigate } from '$app/navigation';
  import { authActions } from '$lib/auth.js';

  afterNavigate(async (navigation) => {
    // We manually redirect after successful login, so we want to update the auth store after the
    // redirect has happened, otherwise subscribers to the auth store get interrupted by the redirect
    const isLoginCallback = navigation?.from?.route.id === '/authentication/login-callback';
    if (isLoginCallback) {
      await authActions.updateStoreWithCurrentUser();
    }
  });
</script>

<NavMenu />

<div class="container mb-4">
  <slot />
</div>

<AppFooter />
<CookieConsent />
