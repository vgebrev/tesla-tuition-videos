<script>
  import LessonSearch from '$components/LessonsList/LessonSearch.svelte';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import { onMount } from 'svelte';
  import { lessonListActions, lessonListStore } from '$components/LessonsList/lesson-list.js';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import LessonCard from '$components/common/LessonCard.svelte';

  onMount(async () => {
    if (!state.lessons) await lessonListActions.search(state.searchText, state.searchTags);
  });

  $: state = $lessonListStore;
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Lessons</h1>
  </div>
</div>

<div class="row">
  <div class="col">
    <LessonSearch />
  </div>
</div>

<div class="row">
  <div class="col my-1 min-height-16">
    <ProgressLoader isLoading={state.isLoadingLessons}></ProgressLoader>
  </div>
</div>

{#if state.lessonsError.isError}
  <div class="row">
    <div class="col mb-3 mx-auto">
      <ErrorCard error={state.lessonsError}></ErrorCard>
    </div>
  </div>
{/if}

{#if state.lessons}
  {#if state.lessons.length > 0}
    <div class="row g-3">
      {#each state.lessons as lesson (lesson.id)}
        <div class="col-sm-12 col-md-6 col-lg-4">
          <LessonCard {lesson}></LessonCard>
        </div>
      {/each}
    </div>
  {:else}
    <div class="row">
      <div class="col text-center">
        No lessons match your search.
        <p class="text-muted">Please try again with different search terms or filters.</p>
      </div>
    </div>
  {/if}
{/if}
