<script>
  import TagBadges from '$components/common/TagBadges.svelte';
  import { lessonListActions, lessonListStore } from './lesson-list.js';

  async function clearFilter() {
    await lessonListActions.search(null, null);
  }

  $: searchTags = $lessonListStore.searchTags?.map(
    /** @param {import('$lib/types').Tag} tag */
    (tag) => {
      return { name: tag.name, priority: tag.category.priority };
    }
  );
</script>

{#if searchTags && searchTags.length > 0}
  <div class="d-flex align-items-baseline">
    <small>Filter by</small>
    <div>
      <TagBadges tags={searchTags}></TagBadges>
    </div>
    <button
      type="button"
      class="btn btn-sm btn-link m-1"
      on:click={clearFilter}>
      <i class="bi bi-x"></i> Clear Filters
    </button>
  </div>
{/if}
