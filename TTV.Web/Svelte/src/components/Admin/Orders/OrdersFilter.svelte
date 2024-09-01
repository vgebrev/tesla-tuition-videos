<script>
  import { adminOrdersStore, adminOrdersActions } from '$components/Admin/Orders/orders.js';
  import { onMount } from 'svelte';

  onMount(async () => {
    await adminOrdersActions.getUsers();
    await adminOrdersActions.getStatuses();
  });

  async function filterOrders() {
    await adminOrdersActions.getOrders();
  }

  $: state = $adminOrdersStore;
</script>

<h4 class="card-title">Filter Orders</h4>
<div class="card-text border-bottom border-1 border-dark mb-3">
  <form
    on:submit|preventDefault={filterOrders}
    novalidate>
    <div class="row">
      <div class="form-group mb-2 col-12 col-md-6 col-lg-3">
        <label for="user-select">User <span class="text-muted">(Optional)</span></label>
        {#if state.users}
          <select
            class="form-select"
            id="user-select"
            bind:value={$adminOrdersStore.filter.userId}>
            <option value=""></option>
            {#each state.users as user (user.id)}
              <option value={user.id}>{user.name} | {user.provider}</option>
            {/each}
          </select>
        {/if}
      </div>

      <div class="form-group mb-2 col-12 col-md-6 col-lg-2">
        <label for="status-select">Status <span class="text-muted">(Optional)</span></label>
        {#if state.statuses}
          <select
            class="form-select"
            id="status-select"
            bind:value={$adminOrdersStore.filter.status}>
            <option value=""></option>
            {#each state.statuses as status (status.id)}
              <option value={status.id}>{status.name}</option>
            {/each}
          </select>
        {/if}
      </div>

      <div class="form-group mb-2 col-12 col-md-6 col-lg-2">
        <label for="placed-on-from">From <span class="text-muted">(Optional)</span></label>
        <input
          type="date"
          class="form-control"
          bind:value={$adminOrdersStore.filter.from}
          id="placed-on-from" />
        <div
          class:is-invalid={!true}
          class="text-danger invalid-feedback">
          Invalid date
        </div>
      </div>

      <div class="form-group mb-2 col-12 col-md-6 col-lg-2">
        <label for="placed-on-to">To <span class="text-muted">(Optional)</span></label>
        <input
          type="date"
          class="form-control"
          bind:value={$adminOrdersStore.filter.to}
          id="placed-on-to" />
      </div>

      <div class="form-group mb-2 col-4 col-md-6 col-lg-1 d-flex align-items-center">
        <div class="form-check form-switch mt-3">
          <input
            class="form-check-input"
            type="checkbox"
            bind:checked={$adminOrdersStore.filter.isFinalised}
            id="order-finalised" />
          <label
            class="form-check-label"
            for="order-finalised">Finalised</label>
        </div>
      </div>

      <div class="mb-3 pt-4 col-8 col-md-6 col-lg-2">
        <button
          type="submit"
          class="btn btn-primary mx-4 text-nowrap"><i class="bi bi-receipt me-2"></i> Filter Orders</button>
      </div>
    </div>
  </form>
</div>
