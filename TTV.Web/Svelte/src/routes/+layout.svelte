<script>
  import AppFooter from '$components/AppFooter.svelte';
  import CookieConsent from '$components/CookieConsent.svelte';
  import NavMenu from '$components/NavMenu/NavMenu.svelte';
  import NewVersionNotificationBanner from '$components/NewVersionNotificationBanner.svelte';
  import { afterNavigate, beforeNavigate } from '$app/navigation';
  import { updated } from '$app/stores';
  import { authStore, authActions } from '$lib/auth.js';
  import { onMount } from 'svelte';
  import FullHeightLoading from '$components/common/FullHeightLoading.svelte';

  onMount(async () => {
    if (!$authStore.isAuthenticated) {
      await authActions.silentSignin();
    }
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
  {#if $authStore.isLoading}
    <FullHeightLoading />
  {:else}
    <slot />
  {/if}
</div>

<AppFooter />
<CookieConsent />
