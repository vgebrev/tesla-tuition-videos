<script>
  import { userManager } from '$lib/auth.js';
  import { onMount } from 'svelte';
  import { goto } from '$app/navigation';

  onMount(() => {
    userManager.signinRedirectCallback().then(() => {
      const route = sessionStorage.getItem('redirect') || '/';
      sessionStorage.removeItem('redirect');
      goto(route);
    });
  });
</script>
