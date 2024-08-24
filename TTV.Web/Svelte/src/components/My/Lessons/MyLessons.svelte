<script>
  import { myLessonsActions, myLessonsStore } from '$components/My/Lessons/my-lessons.js';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import LessonCard from '$components/common/LessonCard.svelte';
  import { onMount } from 'svelte';

  onMount(async () => {
    await myLessonsActions.getOwnedLessons();
  });
  $: state = $myLessonsStore;
</script>

<div class="container">
  <div class="row justify-content-center">
    <div class="col text-center">
      <h1 class="header-white">Lesson Library</h1>
    </div>
  </div>

  <ProgressLoader
    isLoading={state.isLoading}
    class="mb-3" />

  {#if state.lessons}
    {#if state.lessons.length > 0}
      <div class="row row-cols-sm-1 row-cols-md-2 row-cols-lg-3 g-3">
        {#each state.lessons as lesson (lesson.id)}
          <div class="col-sm-12 col-md-6 col-lg-4">
            <LessonCard {lesson}></LessonCard>
          </div>
        {/each}
      </div>
    {:else}
      <div class="row">
        <div class="col text-center">
          Your lesson library is empty.
          <a
            href="/lessons"
            class="card-link">Find some lessons.</a>
        </div>
      </div>
    {/if}
  {/if}

  <ErrorCard error={state.error} />
</div>
