<script>
  import AuthorizeView from '$components/common/AuthorizeView.svelte';

  /** @type {Lesson} */
  export let lesson;
</script>

<AuthorizeView>
  <div
    slot="authorized"
    let:user>
    {#if lesson.isFree}
      <slot name="free" />
    {:else if user.profile.sub === lesson.owner?.id}
      <slot name="owned" />
    {:else}
      <slot name="not-owned" />
    {/if}
  </div>
  <div slot="anonymous">
    {#if lesson.isFree}
      <slot name="free" />
    {:else}
      <slot name="not-owned" />
    {/if}
  </div>
</AuthorizeView>
