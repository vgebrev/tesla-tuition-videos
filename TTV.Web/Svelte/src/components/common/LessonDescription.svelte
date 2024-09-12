<script>
  import Popover from '$components/common/Popover.svelte';

  /** @type {import('$lib/types').Lesson} */
  export let lesson;

  /** @type {HTMLElement} */
  let popoverElem;
</script>

<div class="d-flex my-2">
  <div class="line-clamp-2 card-text small">{lesson.description}</div>
  <button
    bind:this={popoverElem}
    type="button"
    class="btn btn-link">
    <i class="bi-info-circle"></i>
  </button>
  <Popover
    title={lesson.title}
    triggerElem={popoverElem}>
    {lesson.description}<br /><br />Duration: {lesson.duration}
  </Popover>
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
