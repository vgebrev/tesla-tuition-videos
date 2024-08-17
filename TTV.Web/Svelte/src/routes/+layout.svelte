<script>
  import NavMenu from '$components/NavMenu/NavMenu.svelte';
  import AppFooter from '$components/AppFooter.svelte';
  import CookieConsent from '$components/CookieConsent.svelte';
  import { afterNavigate } from '$app/navigation';
  import { authActions } from '$lib/auth.js';
  import { onMount } from 'svelte';

  onMount(async () => {
    await authActions.silentSignin();
  });

  afterNavigate(async (navigation) => {
    const isLoginCallback = navigation?.from?.route.id === '/authentication/login-callback';
    if (isLoginCallback) {
      await authActions.updateStoreWithCurrentUser('login-callback');
    }
  });
</script>

<NavMenu />

<div class="container mb-4">
  <slot />
</div>

<AppFooter />
<CookieConsent />
