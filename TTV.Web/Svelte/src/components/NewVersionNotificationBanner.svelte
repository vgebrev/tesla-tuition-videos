<script>
  import { updated } from '$app/stores';
</script>

{#if $updated}
  <div
    class="position-fixed top-0 w-100 d-flex justify-content-center p-2 pe-none"
    style="z-index: 1050;">
    <div class="alert alert-dismissible alert-primary pe-auto">
      <button
        type="button"
        class="btn-close"
        data-bs-dismiss="alert"
        aria-label="Close alert"></button>
      We've made some improvements to our website. Please
      <a
        href="/"
        class="alert-link"
        on:click|preventDefault={() => window.location.reload()}>reload</a> to update.
    </div>
  </div>
{/if}
