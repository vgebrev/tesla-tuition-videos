<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import ErrorCard from '$components/common/ErrorCard.svelte';
  import LessonPriceList from '$components/ShoppingCart/LessonPriceList.svelte';
  import LoginLink from '$components/common/LoginLink.svelte';
  import PriceDisplay from '$components/common/PriceDisplay.svelte';
  import ProgressLoader from '$components/common/ProgressLoader.svelte';
  import { shoppingCartActions, shoppingCartStore } from '$components/ShoppingCart/shopping-cart.js';
  import { sum } from '$lib/util.js';

  function confirmOrder() {
    shoppingCartActions.confirmOrder();
  }

  /**
   * @param {import('$lib/types').Lesson} lesson
   */
  function removeLesson(lesson) {
    shoppingCartActions.removeLesson(lesson);
  }

  $: state = $shoppingCartStore;
</script>

<div class="container">
  <div class="row justify-content-center">
    <div class="col text-center">
      <h1 class="header-white">Shopping Cart</h1>
    </div>
  </div>
  <ProgressLoader
    isLoading={state.isLoading}
    class="mb-3" />
  <div class="row justify-content-center">
    <div class="card border-primary col-sm-12 col-md-12 col-lg-10">
      <div class="card-body">
        {#if state.lessons.length > 0}
          <LessonPriceList lessons={state.lessons}>
            <tr
              class:table-secondary={i % 2 === 0}
              class="align-middle"
              slot="lesson-row"
              let:lesson
              let:i>
              <td>{lesson.title}</td>
              <td><PriceDisplay price={lesson.currentPrice}></PriceDisplay></td>
              <td class="text-end"
                ><button
                  type="button"
                  class="btn btn-outline-primary"
                  title="Remove Lesson"
                  on:click={() => removeLesson(lesson)}
                  ><i class="bi bi-trash"></i><span class="d-none d-md-block ms-1">Remove</span></button
                ></td>
            </tr>
            <tr slot="footer">
              <th class="text-end">Total</th>
              <th>R{sum(state.lessons, (l) => l.currentPrice.effectiveAmount).toFixed(0)}</th>
              <th></th>
            </tr>
          </LessonPriceList>
        {:else}
          <div class="row">
            <div class="col text-center">
              Your shopping cart is empty.
              <a
                href="/lessons"
                class="card-link">Find some lessons.</a>
            </div>
          </div>
        {/if}
      </div>
    </div>
  </div>
  {#if state.lessons.length > 0}
    <div class="row justify-content-center m-3">
      <div class="col-12 col-md-6 col-lg-6 text-center">
        <AuthorizeView>
          <div slot="unauthorized">
            <LoginLink class="btn btn-primary btn-lg"
              ><i class="bi bi-person-fill me-2"></i> Log in to continue</LoginLink>
          </div>
          <div slot="authorized">
            <button
              type="button"
              class="btn btn-primary btn-lg"
              disabled={state.lessons.length === 0}
              on:click={confirmOrder}
              ><i class="bi bi-check2-circle"></i><span class="ms-2">Confirm and checkout</span></button>
          </div>
        </AuthorizeView>
      </div>
    </div>
  {/if}
  <ErrorCard error={state.error} />
</div>
