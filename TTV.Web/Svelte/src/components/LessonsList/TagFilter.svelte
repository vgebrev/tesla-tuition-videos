<script>
  import { getTags, tagsStore } from '$lib/api/tags.js';
  import { onMount } from 'svelte';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import { defaultErrorMessage } from '$lib/config.js';
  import { groupBy } from '$lib/utils.js';

  let isLoading = false;
  let tagsError = { isError: false, message: null };

  onMount(async () => {
    console.log('here');
    if (tags.length === 0) {
      try {
        isLoading = true;
        await getTags();
      } catch {
        tagsError = { isError: true, message: defaultErrorMessage };
      } finally {
        isLoading = false;
      }
    }
  });

  function clearFilter() {
    //TODO: Implement clearFilter
  }

  function isChecked(tag) {}

  function tagToggled(e, tag) {}

  $: tags = $tagsStore;
</script>

<div class="card text-white bg-secondary">
  <div class="card-body">
    <ProgressLoader {isLoading} />
    <ErrorCard error={tagsError} />
    {#if tags.length > 0}
      <div class="d-flex justify-content-end">
        <button type="button" class="btn btn-sm btn-outline-primary m-1" on:click={clearFilter}
          ><i class="bi bi-x me-1"></i> Clear</button
        >
      </div>
      {#each Object.entries(groupBy(tags, (tag) => tag.category.name)) as [category, tags]}
        <h5 class="card-title">{category}</h5>
        <ul class="list-group list-group-flush">
          {#each tags as tag}
            <li class="list-group-item d-flex justify-content-between align-items-center">
              <div class="form-check form-switch">
                <input
                  class="form-check-input"
                  type="checkbox"
                  checked={isChecked(tag)}
                  on:change={(e) => tagToggled(e, tag)}
                  id="filter-item-{tag.id}"
                />
                <label class="form-check-label" for="filter-item-@tag.Id">{tag.name}</label>
              </div>
              <label class="form-check-label" for="filter-item-{tag.id}"
                ><span class="badge bg-primary rounded-pill">{tag.lessonCount}</span></label
              >
            </li>
          {/each}
        </ul>
      {/each}
    {/if}
  </div>
</div>
