<script>
  import { userManager } from '$lib/user-manager.js';
  import { onMount } from 'svelte';

  onMount(() => {
    userManager.signinRedirectCallback().then(() => {
      const route = sessionStorage.getItem('redirect') || '/';
      sessionStorage.removeItem('redirect');
      window.location = route;
    });
  });
</script>
