<script>
  import { defaultPageSize } from '$lib/config.js';
  import { myLessonsActions, myLessonsStore } from '$components/My/Lessons/my-lessons.js';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import LessonCard from '$components/common/LessonCard.svelte';
  import { onMount } from 'svelte';
  import Pagination from '$components/common/Pagination.svelte';

  onMount(async () => {
    await myLessonsActions.getOwnedLessonsPage(state.pageInfo?.skip || 0, state.pageInfo?.take || defaultPageSize);
  });

  /** Handle page change.
   * @param {number} skip
   * @param {number} take
   * */
  async function onPageChange(skip, take) {
    await myLessonsActions.getOwnedLessonsPage(skip, take);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

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

  {#if state.pageItems}
    {#if state.pageItems.length > 0}
      <div class="row row-cols-sm-1 row-cols-md-2 row-cols-lg-3 g-3">
        {#each state.pageItems as lesson (lesson.id)}
          <div class="col-sm-12 col-md-6 col-lg-4">
            <LessonCard {lesson}></LessonCard>
          </div>
        {/each}
      </div>
      {#if state.pageInfo}
        <div class="row">
          <div class="col g-3 mx-2 d-flex justify-content-center justify-content-md-start">
            <Pagination
              pageInfo={state.pageInfo}
              {onPageChange} />
          </div>
        </div>
      {/if}
    {:else}
      <div class="row">
        <div class="col text-center d-flex flex-column gap-3 mt-3">
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
