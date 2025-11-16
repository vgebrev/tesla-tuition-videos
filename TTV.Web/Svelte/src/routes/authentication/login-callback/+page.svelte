<script>
  import { authStore, userManager } from '$lib/auth.js';
  import { onMount } from 'svelte';
  import { goto } from '$app/navigation';
  import { resolve } from '$app/paths';

  onMount(() => {
    if ($authStore.isAuthenticated) {
      // The root layout seems to do a redirect here, after the STS has already hit it, and we're already authenticated.
      // This short-circuit avoids a session state error in signingRedirectCallback getting called twice.
      return;
    }
    userManager.signinRedirectCallback().then(async () => {
      const route = sessionStorage.getItem('redirect') || '/';
      sessionStorage.removeItem('redirect');
      await goto(resolve(route));
    });
  });
</script>
