<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import {
    adminDiscountVouchersStore,
    adminDiscountVouchersActions
  } from '$components/Admin/DiscountVouchers/discount-vouchers.js';
  import { onMount } from 'svelte';
  import { formatDate, formatDateTime, isPast } from '$lib/util.js';
  import UserDisplay from '$components/Admin/UserDisplay.svelte';
  import Pagination from '$components/common/Pagination.svelte';
  import { defaultPageSize } from '$lib/config.js';

  onMount(async () => {
    await adminDiscountVouchersActions.getDiscountVouchers(
      state.pageInfo?.skip || 0,
      state.pageInfo?.take || defaultPageSize
    );
  });

  /**
   * Handle page change.
   * @param {number} skip
   * @param {number} take
   */
  async function onPageChange(skip, take) {
    await adminDiscountVouchersActions.getDiscountVouchers(skip, take);
  }

  $: state = $adminDiscountVouchersStore;
</script>

<AuthorizeView authorizationPolicy="admin">
  <div slot="authorized">
    {#if state.discountVouchers && state.discountVouchers.length > 0}
      <div class="table-responsive small">
        <table class="table table-striped">
          <thead>
            <tr>
              <th>Code</th>
              <th>Amount</th>
              <th>Balance</th>
              <th>Expires</th>
              <th>Issued</th>
              <th>Issued By</th>
              <th>Used By</th>
              <th>Note</th>
            </tr>
          </thead>
          <tbody>
            {#each state.discountVouchers as voucher, i (voucher.id)}
              <tr class:table-secondary={i % 2 === 0}>
                <td>{voucher.code}</td>
                <td>R{voucher.amount.toFixed(0)}</td>
                <td>R{voucher.balance.toFixed(0)}</td>
                <td
                  class="nowrap"
                  class:text-warning={isPast(voucher.expirationDate)}>
                  {#if voucher.expirationDate}
                    {formatDate(voucher.expirationDate)}
                  {:else}
                    <span class="text-muted">Never</span>
                  {/if}
                </td>
                <td class="nowrap">{formatDateTime(voucher.issuedAt)}</td>
                <td><UserDisplay user={voucher.issuedBy} /></td>
                <td>
                  {#if voucher.claimedBy}
                    <UserDisplay user={voucher.claimedBy} />
                  {:else}
                    <span class="text-muted">Not used</span>
                  {/if}
                </td>
                <td class="nowrap">{voucher.note || ''}</td>
              </tr>
            {/each}
          </tbody>
        </table>
      </div>
      {#if state.pageInfo}
        <div class="row row-cols-1">
          <div class="col d-flex justify-content-center justify-content-md-start">
            <Pagination
              pageInfo={state.pageInfo}
              {onPageChange} />
          </div>
        </div>
      {/if}
    {:else if !state.isLoading}
      <p>No discount vouchers issued.</p>
    {/if}
  </div>
</AuthorizeView>

<style>
  td.nowrap {
    white-space: nowrap;
  }
</style>
