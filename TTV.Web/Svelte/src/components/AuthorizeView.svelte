<script>
  import { onMount } from 'svelte';
  import { userManager } from '$lib/user-manager.js';
  import { writable } from 'svelte/store';

  export let authenticated = writable(false);
  export let user = writable(null);

  onMount(async () => {
    const currentUser = await userManager.getUser();
    if (currentUser && !currentUser.expired) {
      user.set(currentUser);
      authenticated.set(true);
    } else {
      authenticated.set(false);
    }
  });
</script>

{#if $authenticated}
  <slot name="authorized" user={$user}></slot>
{:else}
  <slot name="unauthorized"></slot>
{/if}
