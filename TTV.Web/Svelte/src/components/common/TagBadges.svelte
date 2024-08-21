<script>
  /** @type {import('$lib/types').SimpleTag[]} */
  export let tags = [];

  const backgrounds = ['bg-primary', 'bg-info', 'bg-secondary', 'bg-dark', 'bg-light'];

  /**
   * @param {number} priority
   * @returns {string}
   */
  function getTagBackground(priority) {
    return backgrounds[priority] || 'bg-light';
  }
</script>

{#each tags as tag}
  <span class="badge {getTagBackground(tag.priority)} mx-1">{tag.name}</span>
{/each}
