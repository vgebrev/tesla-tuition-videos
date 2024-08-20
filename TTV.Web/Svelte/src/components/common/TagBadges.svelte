<script>
  /** @type {SimpleTag[]} */
  export let tags = [];

  const backgrounds = ['bg-primary', 'bg-info', 'bg-secondary', 'bg-dark', 'bg-light'];

  function getTagBackground(priority) {
    return backgrounds[priority] || 'bg-light';
  }
</script>

{#each tags as tag}
  <span class="badge {getTagBackground(tag.priority)} mx-1">{tag.name}</span>
{/each}
