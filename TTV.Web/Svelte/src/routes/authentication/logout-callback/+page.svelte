<script>
  import { userManager } from '$lib/user-manager.js';
  import { onMount } from 'svelte';

  onMount(() => {
    userManager.signoutRedirectCallback().then(() => {
      window.location = sessionStorage.getItem('redirect') || '/';
    });
  });
</script>
