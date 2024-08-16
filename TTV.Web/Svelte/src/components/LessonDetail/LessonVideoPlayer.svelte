<script>
  import { config } from '$lib/config.js';
  import { authStore } from '$lib/auth.js';
  import { onMount } from 'svelte';

  /** @type {number} */
  export let lessonId;

  onMount(() => {
    const video = document.getElementById('lesson-video');
    const source = document.getElementById('lesson-video-source');
    source.src = videoUri;
    video.load();
  });
  $: videoUri = `${config.api.baseUrl}/videos/lesson/${lessonId}?t=${new Date().getTime()}${$authStore.isAuthenticated ? `&access_token=${$authStore.user.access_token}` : ''}`;
</script>

<!-- svelte-ignore a11y-media-has-caption -->
<video
  id="lesson-video"
  oncontextmenu="return false;"
  controls
  controlslist="nodownload"
  preload="none"
  poster="img/video-poster.jpg"
  class="w-100"
>
  <source id="lesson-video-source" src={videoUri} type="video/mp4" />
  Your browser does not support video.
</video>
