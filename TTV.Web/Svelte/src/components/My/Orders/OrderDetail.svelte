<script>
  import { goto } from '$app/navigation';
  import { resolve } from '$app/paths';
  import { config } from '$lib/config';
  import { formatDate, sum } from '$lib/util.js';

  /** @type {import('$lib/types').Order} */
  export let order;

  function checkout() {
    goto(resolve('/checkout/[orderId]', { orderId: String(order.id) }));
  }

  /** Get the thumbnail URI for a lesson
   * @param {number} lessonId
   */
  function getThumbnailUri(lessonId) {
    return `${config.api.baseUrl}/videos/lesson/${lessonId}/thumbnail`;
  }
</script>

<div class="card border-primary h-100">
  <div class="card-header bg-secondary">
    <div class="d-flex justify-content-between">
      <h5 class="card-title text-center mb-0">Order #{order.id} ({order.status.name})</h5>
      <small class="text-muted">{formatDate(order.placedOn)}</small>
    </div>
  </div>
  <div class="card-body">
    {#each order.lessons as lesson (lesson.id)}
      <div class="row align-items-start mb-2">
        <div class="col-3 text-end">
          <a href={resolve('/lesson/[lessonId]', { lessonId: String(lesson.id) })}
            ><img
              class="rounded-2 icon-lg"
              src={getThumbnailUri(lesson.id)}
              alt={lesson.title}
              title={lesson.title} /></a>
        </div>
        <div class="col-6">
          <small>
            <a href={resolve('/lesson/[lessonId]', { lessonId: String(lesson.id) })}>{lesson.title}</a>
          </small>
        </div>
        <div class="col-3">R{lesson.currentPrice.effectiveAmount.toFixed(0)}</div>
      </div>
    {/each}
    <div class="row">
      <div class="col-9 text-end"><strong>Total</strong></div>
      <div class="col-3">
        <strong>R{sum(order.lessons, (lesson) => lesson.currentPrice.effectiveAmount).toFixed(0)}</strong>
      </div>
    </div>
    <div class="row">
      <div class="col-9 text-end"><strong>Outstanding</strong></div>
      <div class="col-3"><strong>R{order.totalAmount.toFixed(0)}</strong></div>
    </div>
  </div>
  <div class="card-footer d-flex justify-content-between align-items-baseline">
    {#if !order.isFinalised}
      <button
        type="button"
        class="btn btn-outline-primary"
        on:click={checkout}><i class="bi bi-arrow-right-circle me-2"></i> Continue</button>
    {/if}
  </div>
</div>
