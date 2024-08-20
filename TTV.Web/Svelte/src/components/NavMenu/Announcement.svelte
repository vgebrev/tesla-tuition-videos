<script>
  import { config } from '$lib/config.js';
  import Modal from '$components/common/Modal.svelte';
  import { onMount } from 'svelte';
  import { getCookie, setCookie } from '$lib/cookies.js';
  import { daysBetween } from '$lib/util.js';

  /** @type {string} CSS class attribute to apply to the show announcement button */
  export let cssClass = '';

  let isOpen = false;
  let isShown = false;
  let announcement = config.announcement;
  let endDate = new Date(announcement.endDate);
  let isShowable = announcement.active && endDate >= new Date();

  function show() {
    if (isShowable) {
      setCookie('announcementShown', announcement.id, daysBetween(new Date(), endDate) + 1);
      isShown = true;
      isOpen = true;
    }
  }

  onMount(() => {
    isShown = isShowable ? getCookie('announcementShown', announcement.id) : true;
  });
</script>

{#if isShowable}
  <button
    class="{cssClass} btn btn-link"
    class:jingling-bell={!isShown}
    class:text-warning={!isShown}
    on:click={show}
    title="Announcement">
    <i
      class:bi-bell={isShown}
      class:bi-bell-fill={!isShown}
      class="bi fs-5"></i>
  </button>

  <Modal
    title={announcement.title}
    bind:isOpen>
    <p>{announcement.message}</p>
  </Modal>
{/if}

<style>
  .jingling-bell {
    animation: jingle 10s ease-in-out infinite;
    transform-origin: top center;
  }

  @keyframes jingle {
    0% {
      transform: rotate(0);
    }
    1% {
      transform: rotate(-8deg);
    }
    2% {
      transform: rotate(0);
    }
    3% {
      transform: rotate(8deg);
    }
    4% {
      transform: rotate(0);
    }
    5% {
      transform: rotate(-8deg);
    }
    6% {
      transform: rotate(0);
    }
    7% {
      transform: rotate(8deg);
    }
    8% {
      transform: rotate(0);
    }
    9% {
      transform: rotate(-8deg);
    }
    10% {
      transform: rotate(0);
    }
    11% {
      transform: rotate(8deg);
    }
    12% {
      transform: rotate(0);
    }
  }
</style>
