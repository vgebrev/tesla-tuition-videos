<script>
  import { authStore } from '$lib/auth.js';
</script>

{#if $authStore.isAuthenticated}
  <slot name="authorized" user={$authStore.user}></slot>
{:else}
  <slot name="unauthorized"></slot>
{/if}
