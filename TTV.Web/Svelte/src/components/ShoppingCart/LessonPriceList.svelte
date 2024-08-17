<script>
  /** @type {import('$lib/types').Lesson[]} */
  export let lessons = [];
</script>

<table class="table">
  <thead>
    <tr>
      <th>Lesson</th>
      <th>Price</th>
      <th></th>
    </tr>
  </thead>
  <tbody>
    {#each lessons as lesson, i (lesson.id)}
      <slot name="lesson-row" {lesson} {i} />
    {/each}
  </tbody>
  <tfoot>
    <slot name="footer" />
  </tfoot>
</table>
