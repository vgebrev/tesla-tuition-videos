<script>
  import LessonCard from '$components/common/LessonCard.svelte';
  import { learningPathsActions, learningPathsState } from './learning-paths';

  /**
   * @typedef {Object} LearningPathItem
   * @property {number} id
   * @property {string} name
   * @property {string} description
   * @property {number} sequence
   * @property {number|null} lessonId
   * @property {Lookup[]} curricula
   * @property {LearningPathItem[]} [items]
   */

  /** @type {LearningPathItem} */
  export let item;

  /** @type {number} */
  export let depth = 0;

  /** @type {string} */
  export let parentPath = '';

  /** @type {Set<string>} */
  export let expandedSections;

  /** @type {string} */
  export let selectedCurriculum;

  $: itemPath = `${parentPath}-${item.id}`;
  $: isExpanded = expandedSections.has(itemPath);
  $: hasChildren = hasSubItems(item);
  $: isLessonItem = isLesson(item);
  $: indentLevel = depth * 12; // 12px per level
  $: canExpand = hasChildren || isLessonItem;

  // Check if this item should be visible based on curriculum
  $: shouldShowItem = item.curricula?.some((curriculum) => curriculum.id.toString() === selectedCurriculum) || false;

  $: state = $learningPathsState;
  $: lessonData = isLessonItem ? state.loadedLessons?.find((lesson) => lesson.id === item.lessonId) : null;

  /**
   * @param {string} id
   */
  function toggleSection(id) {
    if (expandedSections.has(id)) {
      expandedSections.delete(id);
    } else {
      expandedSections.add(id);
      // If this is a lesson item and we don't have lesson data, fetch it
      if (isLessonItem && !lessonData) {
        learningPathsActions.getLessonDetails(item.lessonId);
      }
    }
    expandedSections = expandedSections;
  }

  /**
   * @param {LearningPathItem} item
   * @returns {boolean}
   */
  function hasSubItems(item) {
    return item.items && item.items.length > 0;
  }

  /**
   * @param {LearningPathItem} item
   * @returns {boolean}
   */
  function isLesson(item) {
    return item.lessonId !== null && item.lessonId !== undefined;
  }

  function handleKeydown(event) {
    if (event.key === 'Enter' || event.key === ' ') {
      event.preventDefault();
      if (canExpand) {
        toggleSection(itemPath);
      }
    }
  }
</script>

{#if shouldShowItem}
  <div class="learning-path-item">
    <!-- Item Row -->
    <div
      class="d-flex align-items-center rounded user-select-none item-row"
      style="padding-left: {indentLevel}px;"
      role="treeitem"
      aria-selected={isExpanded}
      tabindex="0"
      aria-expanded={canExpand ? isExpanded : undefined}
      on:click={() => canExpand && toggleSection(itemPath)}
      on:keydown={handleKeydown}>
      <!-- Caret -->
      <div class="d-flex align-items-center justify-content-center flex-shrink-0 p-1">
        {#if canExpand}
          {#if isLessonItem}
            <i class="bi bi-chevron-{isExpanded ? 'down' : 'right'} text-white-50"></i>
          {:else}
            <i class="bi bi-caret-{isExpanded ? 'down' : 'right'}-fill text-white-50"></i>
          {/if}
        {/if}
      </div>

      <!-- Item Content -->
      <div class="d-flex align-items-center w-100 overflow-hidden">
        <div class="d-flex align-items-center justify-content-center flex-shrink-0 me-2">
          {#if isLessonItem}
            <i class="bi bi bi-play-btn text-primary ms-2"></i>
          {:else}
            <i class="bi bi-folder text-white-50"></i>
          {/if}
        </div>
        <span class="text-white text-truncate me-2">{item.name}</span>
        <!--{#if isLessonItem}-->
        <!--  <button class="btn btn-outline-primary btn-sm ms-auto flex-shrink-0">-->
        <!--    <i class="bi bi-cart-plus me-1"></i>-->
        <!--    Buy-->
        <!--  </button>-->
        <!--{/if}-->
      </div>
    </div>

    <!-- Children -->
    {#if hasChildren && isExpanded}
      <div class="children-container">
        {#each item.items.sort((a, b) => a.sequence - b.sequence) as childItem (childItem.id)}
          <svelte:self
            item={childItem}
            depth={depth + 1}
            parentPath={itemPath}
            {selectedCurriculum}
            bind:expandedSections />
        {/each}
      </div>
    {/if}

    <!-- Lesson Card -->
    {#if isLessonItem && isExpanded}
      <div>
        {#if lessonData}
          <LessonCard lesson={lessonData} />
        {:else if state.isLoading}
          <div class="card border-primary">
            <div class="card-body text-center p-4">
              <div
                class="spinner-border text-primary"
                role="status">
                <span class="visually-hidden">Loading...</span>
              </div>
              <p class="text-white mt-2 mb-0">Loading lesson details...</p>
            </div>
          </div>
        {/if}
      </div>
    {/if}
  </div>
{/if}
