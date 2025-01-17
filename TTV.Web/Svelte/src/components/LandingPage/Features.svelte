<script>
  import { onMount } from 'svelte';
  import { observer } from '$lib/intersection-observer.js';
  import FlipCard from '$components/LandingPage/FlipCard.svelte';

  /** @type {HTMLElement} */
  let headerElem;
  onMount(() => {
    observer.observe(headerElem);
  });
</script>

<div class="mt-1 mt-md-3 bg-dark p-3 p-md-5">
  <h1
    bind:this={headerElem}
    class="fade-in header-white">
    Why choose Tesla Tuition Videos?
  </h1>
  <p class="fs-5">Our mission is to make quality Physical Science education accessible and affordable for everyone.</p>

  <div class="row">
    <FlipCard>
      <div slot="front">
        <div class="fs-1"><i class="bi bi-film"></i></div>
        <h4 class="card-title">Video Lessons</h4>
      </div>
      <div slot="back">
        <ul class="h-100">
          <li>Expertly crafted by our experienced teacher.</li>
          <li>Clear, step-by-step explanations of complex concepts.</li>
          <li>Accessible anytime, anywhere for flexible learning.</li>
        </ul>
      </div>
    </FlipCard>
    <FlipCard>
      <div slot="front">
        <div class="fs-1"><i class="bi bi-journal-text"></i></div>
        <h4 class="card-title">Practice Exercises</h4>
      </div>
      <div slot="back">
        <ul class="h-100">
          <li>Exercises tailored to each video lesson and topic.</li>
          <li>Varied question types to build exam readiness.</li>
          <li>Answers provided for self-assessment.</li>
          <li>Based on real past exam papers.</li>
        </ul>
      </div>
    </FlipCard>
    <FlipCard>
      <div slot="front">
        <div class="fs-1"><i class="bi bi-piggy-bank"></i></div>
        <h4 class="card-title">Affordable Pricing</h4>
      </div>
      <div slot="back">
        <ul class="h-100">
          <li>Lesson length-based pricing structure.</li>
          <li>High-quality education at a fraction of the cost of private tutoring.</li>
          <li>No hidden fees.</li>
          <li>Buy once, use forever.</li>
        </ul>
      </div>
    </FlipCard>
    <FlipCard>
      <div slot="front">
        <div class="fs-1"><i class="bi bi-mortarboard"></i></div>
        <h4 class="card-title">Broad Range of Topics</h4>
      </div>
      <div slot="back">
        <ul class="h-100">
          <li>Designed for the South African education system.</li>
          <li>Multiple curricula - IEB and CAPS.</li>
          <li>Physics and Chemistry.</li>
          <li>Complete coverage of Grade 11 (Examinable in Grade 12.)</li>
          <li class="text-muted">Grade 12 content coming soon.</li>
        </ul>
      </div>
    </FlipCard>
    <FlipCard>
      <div slot="front">
        <div class="fs-1"><i class="bi bi-file-earmark-plus"></i></div>
        <h4 class="card-title">Living Content</h4>
      </div>
      <div slot="back">
        <ul class="h-100">
          <li>Continually adding new videos and exercises.</li>
          <li>Updating existing videos and exercises as needed.</li>
          <li>Content that evolves with educational standards and student needs.</li>
        </ul>
      </div>
    </FlipCard>
    <FlipCard>
      <div slot="front">
        <div class="fs-1"><i class="bi bi-question-circle"></i></div>
        <h4 class="card-title">Customer Support</h4>
      </div>
      <div slot="back">
        <ul class="h-100">
          <li>Assistance for technical issues via WhatsApp, email or phone.</li>
          <li>Content-related queries.</li>
          <li>Quick response times to ensure minimal disruption to learning.</li>
        </ul>
      </div>
    </FlipCard>
  </div>
</div>
