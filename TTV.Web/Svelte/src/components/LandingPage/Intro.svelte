<script>
  import { onMount, onDestroy } from 'svelte';
  import { observer } from '$lib/intersection-observer';
  import { resolve } from '$app/paths';

  /** @type {HTMLElement} */
  let headerElem;
  /** @type {HTMLVideoElement} */
  let videoElem;
  /** @type {number} */
  let interval;

  function initLandingVideo() {
    if (interval) {
      clearInterval(interval);
    }
    interval = setInterval(() => {
      if (!videoElem) return;
      videoElem.pause();
      videoElem.currentTime = 0;
      const play = videoElem.play();
      if (play !== undefined) {
        play.then(() => {}).catch(() => {});
      }
    }, 18 * 1000);
  }

  onMount(() => {
    observer.observe(headerElem);
    initLandingVideo();
  });

  onDestroy(() => {
    if (interval) {
      clearInterval(interval);
    }
  });
</script>

<div class="position-relative intro w-100 p-0">
  <div class="position-absolute top-0 d-flex flex-column justify-content-between h-100 w-100 p-3 p-md-5">
    <div>
      <h1
        bind:this={headerElem}
        class="fade-in header-white">
        Master Physical Science
      </h1>
      <h3>High-quality, comprehensive videos for South African high school students</h3>
      <h5 class="opacity-75">Compliant with the IEB and CAPS curriculums</h5>
    </div>
    <div class="d-flex flex-column align-items-center w-100 gap-1 gap-md-3 gap-lg-5">
      <h3>Where science comes alive!</h3>
      <div class="container row text-center">
        <div class="col-12 col-md-6 my-1">
          <a
            href={resolve('/lessons')}
            class="btn btn-lg btn-outline-primary p-3 flex-fill w-75">Browse Lessons</a>
        </div>
        <div class="col-12 col-md-6 my-1">
          <a
            href={resolve('/lesson/[lessonId]', { lessonId: '37' })}
            class="btn btn-lg btn-outline-primary p-3 flex-fill w-75">Try a Free Lesson</a>
        </div>
      </div>
    </div>
  </div>
  <div class="intro-video-container">
    <video
      autoplay
      muted
      bind:this={videoElem}
      id="intro-video"
      poster="../img/video-poster.jpg">
      <source
        src="../video/intro.mp4"
        type="video/mp4" />
    </video>
  </div>
</div>
