<script>
  import { onMount } from 'svelte';
  import { userManager } from '$lib/user-manager.js';

  onMount(async () => {
    const route = sessionStorage.getItem('redirect') || '/';
    sessionStorage.removeItem('redirect');

    await userManager.clearStaleState();
    window.location.href = route;
  });
</script>
