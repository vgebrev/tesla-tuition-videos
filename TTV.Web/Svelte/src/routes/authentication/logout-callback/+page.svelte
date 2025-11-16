<script>
  import { onMount } from 'svelte';
  import { userManager } from '$lib/auth.js';
  import { goto } from '$app/navigation';
  import { resolve } from '$app/paths';

  onMount(async () => {
    const route = sessionStorage.getItem('redirect') || '/';
    sessionStorage.removeItem('redirect');

    await userManager.clearStaleState();
    await goto(resolve(route));
  });
</script>
