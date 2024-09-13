<script>
  import Popover from '$components/common/Popover.svelte';
  /** @type {import('$lib/types').Order} */
  export let order;

  /** @type {HTMLElement} */
  let popoverElem;
</script>

<button
  class="btn btn-link p-0 me-1"
  bind:this={popoverElem}>
  <i class="bi bi-info-circle"></i>
</button>
<Popover
  triggerElem={popoverElem}
  title="Order Items">
  {#if order.lessons.length > 0}
    <ul class="list-group list-group-flush">
      {#each order.lessons as lesson (lesson.id)}
        <li
          class="list-group-item list-group-item-action"
          class:text-danger={!order.isFinalised && lesson.owner?.id === order.placedBy.id}>
          {lesson.title} - R{lesson.currentPrice.effectiveAmount.toFixed(0)}
        </li>
      {/each}
    </ul>
  {:else}
    <span class="text-muted">No items</span>
  {/if}
</Popover>
