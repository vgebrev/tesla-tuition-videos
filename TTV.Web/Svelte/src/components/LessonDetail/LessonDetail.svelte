<script>
  import { onMount } from 'svelte';
  import {
    lessonDetailActions,
    lessonDetailStore
  } from '$components/LessonDetail/lesson-detail.js';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import TagBadges from '$components/common/TagBadges.svelte';
  import DocumentLink from '$components/LessonDetail/DocumentLink.svelte';
  import LimitedAccessCard from '$components/LessonDetail/LimitedAccessCard.svelte';
  import LessonVideoPlayer from '$components/LessonDetail/LessonVideoPlayer.svelte';

  /** @type {number} */
  export let lessonId;

  onMount(async () => {
    await lessonDetailActions.getLesson(lessonId);
    await lessonDetailActions.getDocuments(lessonId);
  });

  $: state = $lessonDetailStore;
</script>

<svelte:head>
  <title>Tesla Tuition Videos - {state.lesson?.title}</title>
</svelte:head>

<ProgressLoader isLoading={state.isLoadingLessons || state.isLoadingDocuments} />
{#if state.error.isError}
  <ErrorCard error={state.error} />
{/if}

{#if state.lesson}
  <div class="card border-primary">
    <div class="card-header bg-secondary">
      <h5 class="card-title text-center mb-0">{state.lesson.title}</h5>
    </div>
    <div class="card-body">
      <LimitedAccessCard lesson={state.lesson} />
      <LessonVideoPlayer {lessonId} />
      <p class="card-text">{state.lesson.description}</p>
      <TagBadges tags={state.lesson.tags} />
    </div>
    <div class="card-footer">
      {#if state.documents.length > 0}
        <h5 class="card-subtitle">Exercises and Additional Lesson Resources:</h5>
        <div class="d-flex gap-2 fs-5 my-2">
          {#each state.documents as document (document.id)}
            <DocumentLink {document} />
          {/each}
        </div>
      {/if}
    </div>
  </div>
{:else if !state.isLoadingLesson && !state.isLoadingDocuments && !state.error.isError}
  <div class="row justify-content-center">
    <div class="card border-warning col-12 col-md-8 col-xl-6">
      <div class="card-body">
        Lesson #{lessonId} not found.
      </div>
    </div>
  </div>
{/if}
