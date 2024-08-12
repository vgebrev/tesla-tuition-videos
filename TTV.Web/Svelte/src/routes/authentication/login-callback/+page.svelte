<script>
  import { userManager } from '$lib/user-manager.js';
  import { onMount } from 'svelte';

  onMount(() => {
    userManager.signinRedirectCallback().then(() => {
      window.location = sessionStorage.getItem('redirect') || '/';
    });
  });
</script>
