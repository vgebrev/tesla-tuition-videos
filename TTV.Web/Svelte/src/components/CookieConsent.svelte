<script>
  import { onMount } from 'svelte';
  import { getCookie, setCookie } from '$lib/cookies.js';

  let isConsentGiven = false;
  onMount(() => {
    isConsentGiven = getCookie('cookieConsent');
  });

  function giveConsent() {
    setCookie('cookieConsent', 'true', 180);
    isConsentGiven = true;
  }
</script>

{#if !isConsentGiven}
  <div class="position-fixed bottom-0 w-100">
    <div class="alert alert-primary d-flex gap-2 align-items-center justify-content-center m-auto">
      <span
        >As per our <a href="/cookies-policy">Cookies Policy</a>, we use authentication cookies to
        keep you logged in. We do not use tracking or marketing cookies.</span
      >
      <button on:click={giveConsent} class="btn btn-secondary">Accept</button>
    </div>
  </div>
{/if}
