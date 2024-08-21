<script>
  import { onMount } from 'svelte';

  /** @type {import('$lib/types').Lesson} */
  export let lesson;

  /** @type {HTMLElement} */
  let popoverElem;

  onMount(() => {
    new bootstrap.Popover(popoverElem);
  });
</script>

<div class="d-flex my-2">
  <div class="line-clamp-2 card-text small">{lesson.description}</div>
  <button
    bind:this={popoverElem}
    type="button"
    class="btn btn-sm btn-outline-primary"
    data-bs-container="body"
    data-bs-toggle="popover"
    data-bs-trigger="focus"
    data-bs-placement="top"
    data-bs-html="true"
    data-bs-content="{lesson.description}<br/><br/>Duration: {lesson.duration}"
    data-bs-original-title={lesson.title}>
    <i class="bi-info-circle"></i>
  </button>
</div>

<style>
  div.line-clamp-2 {
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }

  @supports (-webkit-line-clamp: 2) {
    div.line-clamp-2 {
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: initial;
      display: -webkit-box;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
    }
  }
</style>
