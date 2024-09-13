<script>
  /** @type {import('$lib/types').PageInfo} */
  export let pageInfo;

  /** @type {(skip: number, take: number) => void} */
  export let onPageChange;

  $: currentPage = Math.ceil(pageInfo.skip / pageInfo.take) + 1;
  $: totalPages = Math.ceil(pageInfo.count / pageInfo.take);

  /**
   * Set the current page.
   * @param {number} page
   */
  function setPage(page) {
    if (page < 1 || page > totalPages) return;
    currentPage = page;
    const skip = (currentPage - 1) * pageInfo.take;
    onPageChange && onPageChange(skip, pageInfo.take);
  }
</script>

<nav aria-label="Page navigation">
  <ul class="pagination">
    <li class="page-item text-primary small align-self-center">Page</li>
    <li
      class="page-item"
      class:disabled={currentPage === 1}>
      <button
        class="page-link"
        on:click={() => setPage(currentPage - 1)}
        aria-label="Previous">
        <i class="bi bi-chevron-double-left" />
      </button>
    </li>

    {#each Array(totalPages) as _, i}
      <li
        class="page-item"
        class:active={currentPage === i + 1}>
        <button
          class="page-link"
          on:click={() => setPage(i + 1)}>
          {i + 1}
        </button>
      </li>
    {/each}

    <li
      class="page-item"
      class:disabled={currentPage === totalPages}>
      <button
        class="page-link"
        on:click={() => setPage(currentPage + 1)}
        aria-label="Next">
        <i class="bi bi-chevron-double-right" />
      </button>
    </li>
    <li class="page-item align-self-center text-primary small">
      {pageInfo.skip + 1} - {Math.min(pageInfo.count, pageInfo.skip + pageInfo.take)} of {pageInfo.count}
    </li>
  </ul>
</nav>
