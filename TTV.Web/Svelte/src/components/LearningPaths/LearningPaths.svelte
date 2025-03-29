<script>
  import { learningPathsState } from './learning-paths';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';

  $: state = $learningPathsState;
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Learning Paths</h1>
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
