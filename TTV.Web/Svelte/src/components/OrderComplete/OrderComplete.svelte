<script>
  import { onMount } from 'svelte';
  import { orderCompleteActions, orderCompleteStore } from '$components/OrderComplete/order-complete.js';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import LessonCard from '$components/common/LessonCard.svelte';
  import { resolve } from '$app/paths';

  /** @type {number} */
  export let orderId;

  onMount(async () => {
    await orderCompleteActions.completeOrder(orderId);
  });

  $: state = $orderCompleteStore;
</script>

<div class="container">
  <div class="row justify-content-center">
    <div class="col text-center">
      <h1 class="header-white">Order Complete</h1>
    </div>
  </div>

  <ProgressLoader
    isLoading={state.isLoading}
    class="mb-3"></ProgressLoader>

  {#if state.order && state.completeOrderResult?.isSuccess}
    <div class="row justify-content-center">
      <div class="col-lg-6 col-md-8 col-sm-12 text-center">
        <div class="card border-success my-3">
          <div class="card-body">
            <p class="card-text">
              Thank you! You've successfully added the lessons below to <a
                href={resolve('/my/library')}
                title="your library">your library</a>
            </p>
          </div>
        </div>
      </div>
    </div>

    <div class="row row-cols-lg-3 row-cols-md-2 row-cols-sm-1 g-3 justify-content-evenly">
      {#each state.order.lessons as lesson (lesson.id)}
        <div class="col-lg-4 col-md-6 col-sm-12">
          <LessonCard {lesson}></LessonCard>
        </div>
      {/each}
    </div>
  {/if}
  <ErrorCard error={state.error} />
</div>
