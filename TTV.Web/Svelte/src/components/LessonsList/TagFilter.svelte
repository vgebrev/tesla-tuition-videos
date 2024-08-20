<script>
  import { lessonListActions, lessonListStore } from './lesson-list.js';
  import { onMount } from 'svelte';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import { groupBy } from '$lib/util.js';

  onMount(async () => {
    await lessonListActions.getTags();
  });

  async function clearFilter() {
    lessonListActions.setTagFilterDrawer(false);
    await lessonListActions.search(null, null);
  }

  /**
   * @param {import('$lib/types').Tag} tag
   * @returns {boolean}
   */
  function isChecked(tag) {
    return state.searchTags?.some((t) => t.id === tag.id);
  }

  /**
   * @param {Event} e
   * @param {import('$lib/types').Tag} tag
   */
  async function tagToggled(e, tag) {
    const isChecked = e.target.checked;
    let searchTags = state.searchTags || [];

    if (isChecked && !searchTags.some((t) => t.id === tag.id)) {
      searchTags = [...searchTags, tag];
    }

    if (!isChecked) {
      searchTags = searchTags.filter((t) => t.id !== tag.id);
    }

    await lessonListActions.setSearchTags(searchTags);
  }

  $: state = $lessonListStore;
</script>

<div class="card text-white bg-secondary">
  <div class="card-body">
    <ProgressLoader isLoading={state.isLoadingTags} />
    <ErrorCard error={state.tagsError} />
    {#if state.tags}
      <div class="d-flex justify-content-end">
        <button
          type="button"
          class="btn btn-sm btn-outline-primary m-1"
          on:click={clearFilter}><i class="bi bi-x me-1"></i> Clear</button>
      </div>
      {#each Object.entries(groupBy(state.tags, (tag) => tag.category.name)) as [category, tags]}
        <h5 class="card-title">{category}</h5>
        <ul class="list-group list-group-flush">
          {#each tags as tag (tag.id)}
            <li class="list-group-item d-flex justify-content-between align-items-center">
              <div class="form-check form-switch">
                <input
                  class="form-check-input"
                  type="checkbox"
                  checked={isChecked(tag)}
                  on:change={(e) => tagToggled(e, tag)}
                  id="filter-item-{tag.id}" />
                <label
                  class="form-check-label"
                  for="filter-item-{tag.id}">{tag.name}</label>
              </div>
              <label
                class="form-check-label"
                for="filter-item-{tag.id}"><span class="badge bg-primary rounded-pill">{tag.lessonCount}</span></label>
            </li>
          {/each}
        </ul>
      {/each}
    {/if}
  </div>
</div>
