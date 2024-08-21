<script>
  import Drawer from '$components/common/Drawer.svelte';
  import TagFilter from '$components/LessonsList/TagFilter.svelte';
  import TagFilterIndicator from '$components/LessonsList/TagFilterIndicator.svelte';
  import { lessonListActions, lessonListStore } from '$components/LessonsList/lesson-list.js';

  /**
   * @param {KeyboardEvent} event
   */
  async function keyPressed(event) {
    if (event.key === 'Enter') {
      await search();
    }
  }

  async function search() {
    await lessonListActions.search(state.searchText, state.searchTags);
  }

  function openTagFilterDrawer() {
    lessonListActions.setTagFilterDrawer(true);
  }

  $: state = $lessonListStore;
</script>

<div class="form-group">
  <div class="row g-3">
    <div class="col-12 col-md-8 col-lg-9">
      <div class="form-floating">
        <input
          type="text"
          class="form-control"
          id="lesson-search-text"
          bind:value={$lessonListStore.searchText}
          on:keypress={keyPressed}
          placeholder="Search lessons..." />
        <label for="lesson-search-text">Search lessons</label>
      </div>
    </div>
    <div class="col-12 col-md-4 col-lg-3">
      <div class="d-flex gap-3 h-100">
        <button
          class="btn btn-outline-primary flex-fill h-100"
          type="button"
          on:click={search}>
          <i class="bi bi-search me-1"></i> <span>Search</span>
        </button>
        <button
          class:btn-secondary={$lessonListStore.searchTags?.length > 0}
          class="btn btn-outline-primary flex-fill h-100"
          on:click={openTagFilterDrawer}>
          <i class="bi bi-sliders"></i> <span>Filters</span>
        </button>
      </div>
    </div>
  </div>
  <div class="row">
    <div class="col"><TagFilterIndicator></TagFilterIndicator></div>
  </div>
</div>

<Drawer bind:isOpen={$lessonListStore.isTagFilterDrawerOpen}>
  <div slot="title">
    <i class="bi bi-sliders"></i> <span>Filters</span>
  </div>
  <div slot="body">
    <TagFilter></TagFilter>
  </div>
</Drawer>
