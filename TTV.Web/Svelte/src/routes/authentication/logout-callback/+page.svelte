<script>
  import { onMount } from 'svelte';
  import { userManager } from '$lib/auth.js';
  import { goto } from '$app/navigation';

  onMount(async () => {
    const route = sessionStorage.getItem('redirect') || '/';
    sessionStorage.removeItem('redirect');

    await userManager.clearStaleState();
    await goto(route);
  });
</script>
