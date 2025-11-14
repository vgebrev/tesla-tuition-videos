<script>
  import { learningPathsActions, learningPathsState } from './learning-path.js';
  import LearningPathItem from './LearningPathItem.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import { onMount } from 'svelte';
  import { SvelteSet } from 'svelte/reactivity';

  let expandedSections = new SvelteSet();
  let selectedLearningPath = '';
  let selectedCurriculum = '1'; // Default to CAPS

  // Curriculum options based on the enum
  const curriculumOptions = [
    { id: '1', name: 'CAPS' },
    { id: '2', name: 'IEB' }
  ];

  onMount(async () => {
    await learningPathsActions.getLearningPaths();
  });

  $: state = $learningPathsState;

  // Set the first learning path as selected when data loads
  $: if ((state.learningPaths?.length || 0) > 0 && !selectedLearningPath) {
    selectedLearningPath = state.learningPaths[0].id.toString();
  }

  // Expand all sections by default when learning paths are loaded
  $: if ((state.learningPaths?.length || 0) > 0 && expandedSections.size === 0) {
    expandAllSections();
  }

  function expandAllSections() {
    const allSections = new SvelteSet();

    function addItemsToExpanded(items, parentPath) {
      items.forEach((item) => {
        const itemPath = `${parentPath}-${item.id}`;
        if (item.items && item.items.length > 0) {
          allSections.add(itemPath);
          addItemsToExpanded(item.items, itemPath);
        }
      });
    }

    state.learningPaths.forEach((learningPath) => {
      addItemsToExpanded(learningPath.items, `learning-path-${learningPath.id}`);
    });

    expandedSections = allSections;
  }
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Learning Path</h1>
  </div>
</div>

<div class="row">
  <div class="col my-1 min-height-16">
    <ProgressLoader isLoading={state.isLoading}></ProgressLoader>
  </div>
</div>

{#if state.error.isError}
  <div class="row">
    <div class="col mb-3 mx-auto">
      <ErrorCard error={state.error}></ErrorCard>
    </div>
  </div>
{/if}

{#if state.learningPaths.length > 0}
  <div class="row justify-content-center">
    <div class="col-12 col-lg-10 col-xl-8">
      <div class="mb-4">
        Designed for both CAPS and IEB, our lessons can be followed in our suggested order or adapted to your school’s
        pace.
      </div>
      <!-- Curriculum Selection -->
      <div class="mb-4">
        <div class="pb-2 text-white">Which curriculum are you studying?</div>
        <div
          class="btn-group"
          role="group"
          aria-label="Curriculum selection">
          {#each curriculumOptions as curriculum (curriculum.id)}
            <input
              type="radio"
              class="btn-check"
              name="curriculum"
              id="curriculum-{curriculum.id}"
              bind:group={selectedCurriculum}
              value={curriculum.id}
              autocomplete="off" />
            <label
              class="btn btn-outline-primary"
              for="curriculum-{curriculum.id}">
              {curriculum.name}
            </label>
          {/each}
        </div>
      </div>

      <div class="mb-4">
        <div class="pb-2 text-white">What topic do you want to work on?</div>
        <div
          class="btn-group"
          role="group"
          aria-label="Learning path selection">
          {#each state.learningPaths as learningPath (learningPath.id)}
            <input
              type="radio"
              class="btn-check"
              name="learningPath"
              id="learning-path-{learningPath.id}"
              bind:group={selectedLearningPath}
              value={learningPath.id.toString()}
              autocomplete="off" />
            <label
              class="btn btn-outline-primary"
              for="learning-path-{learningPath.id}">
              {learningPath.name}
            </label>
          {/each}
        </div>
      </div>
      <!-- Learning Path Content -->
      {#each state.learningPaths as learningPath (learningPath.id)}
        {#if selectedLearningPath === learningPath.id.toString()}
          <div class="learning-path-content">
            <!-- Learning Path Header -->
            <div class="learning-path-header border-bottom border-white border-opacity-25">
              <p class="text-white-50 mb-0">{learningPath.description}</p>
            </div>

            <!-- Learning Path Tree -->
            <div class="bg-black bg-opacity-25 rounded py-3">
              {#each learningPath.items.sort((a, b) => a.sequence - b.sequence) as item (item.id)}
                <LearningPathItem
                  {item}
                  depth={0}
                  parentPath={`learning-path-${learningPath.id}`}
                  {selectedCurriculum}
                  bind:expandedSections />
              {/each}
            </div>
          </div>
        {/if}
      {/each}
    </div>
  </div>
{/if}
