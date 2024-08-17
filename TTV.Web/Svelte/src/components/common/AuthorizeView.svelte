<script>
  import { authStore, authorizationPolicies } from '$lib/auth.js';

  /** @type {'admin'|'canIssueVouchers'|?string} */
  export let authorizationPolicy = null;

  $: isAuthenticated = $authStore.isAuthenticated;
  $: isAuthorized = isAuthenticated
    ? authorizationPolicy
      ? authorizationPolicies[authorizationPolicy]($authStore.user)
      : true
    : false;
</script>

{#if isAuthenticated}
  {#if isAuthorized}
    <slot name="authorized" user={$authStore.user}></slot>
  {:else}
    <slot name="unauthorized" user={$authStore.user}></slot>
  {/if}
{:else}
  <slot name="anonymous"></slot>
{/if}
