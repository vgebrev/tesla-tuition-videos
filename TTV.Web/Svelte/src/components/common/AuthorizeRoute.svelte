<script>
  import { authStore, authorizationPolicies, authActions } from '$lib/auth.js';
  import { onMount } from 'svelte';
  import FullHeightLoading from '$components/common/FullHeightLoading.svelte';
  import ErrorView from '$components/common/ErrorView.svelte';

  /** @type {'admin'|'canIssueVouchers'|null} */
  export let authorizationPolicy = null;

  onMount(async () => {
    if (!isAuthenticated) {
      await authActions.fullSignin();
    }
  });

  $: isAuthenticated = $authStore.isAuthenticated;
  $: isAuthorized = isAuthenticated
    ? authorizationPolicy
      ? authorizationPolicies[authorizationPolicy]($authStore.user)
      : true
    : false;
  $: isLoading = $authStore.isLoading;
</script>

{#if isAuthenticated}
  {#if isAuthorized}
    <slot name="authorized" />
  {:else}
    <slot name="unauthorized"
      ><ErrorView
        status={403}
        errorMessage="Forbidden" /></slot>
  {/if}
{:else}
  <slot name="anonymous">
    {#if isLoading}
      <FullHeightLoading />
    {:else}
      <ErrorView
        status={403}
        errorMessage="Forbidden" />
    {/if}
  </slot>
{/if}
