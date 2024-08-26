<script>
  import AppFooter from '$components/AppFooter.svelte';
  import CookieConsent from '$components/CookieConsent.svelte';
  import NavMenu from '$components/NavMenu/NavMenu.svelte';
  import NewVersionNotificationBanner from '$components/NewVersionNotificationBanner.svelte';
  import { afterNavigate, beforeNavigate } from '$app/navigation';
  import { updated } from '$app/stores';
  import { authActions } from '$lib/auth.js';
  import { onMount } from 'svelte';

  onMount(async () => {
    await authActions.silentSignin();
  });

  beforeNavigate(({ willUnload, to }) => {
    if ($updated && !willUnload && to?.url) {
      location.href = to.url.href;
    }
  });

  afterNavigate(async (navigation) => {
    const isLoginCallback = navigation?.from?.route.id === '/authentication/login-callback';
    if (isLoginCallback) {
      await authActions.updateStoreWithCurrentUser('login-callback');
    }
  });
</script>

<NavMenu />
<NewVersionNotificationBanner />

<div class="container mb-4">
  <slot />
</div>

<AppFooter />
<CookieConsent />
