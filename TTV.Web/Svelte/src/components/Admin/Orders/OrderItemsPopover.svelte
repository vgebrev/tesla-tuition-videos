<script>
  import Popover from '$components/common/Popover.svelte';
  /** @type {import('$lib/types').Order} */
  export let order;

  /** @type {HTMLElement} */
  let popoverElem;
</script>

<button
  class="btn btn-link pt-0"
  bind:this={popoverElem}>
  <i class="bi bi-info-circle"></i>
</button>
<Popover triggerElem={popoverElem}>
  {#if order.lessons.length > 0}
    <h6 class="px-2">Order Items</h6>
    <ul class="list-group list-group-flush">
      {#each order.lessons as lesson, i (lesson.id)}
        <li
          class="list-group-item"
          class:bg-secondary={i % 2 !== 0}>
          {lesson.title}
        </li>
      {/each}
    </ul>{:else}
    No items
  {/if}
</Popover>
