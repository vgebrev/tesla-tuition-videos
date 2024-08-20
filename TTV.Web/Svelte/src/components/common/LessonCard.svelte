<script>
  import LessonDescription from '$components/common/LessonDescription.svelte';
  import TagBadges from '$components/common/TagBadges.svelte';
  import LessonOwnView from '$components/common/LessonOwnView.svelte';
  import BuyNowButton from '$components/common/BuyNowButton.svelte';
  import { config } from '$lib/config';
  /** @type {import('$lib/types').Lesson} */
  export let lesson;

  $: thumbnailUri = `${config.api.baseUrl}/videos/lesson/${lesson.id}/thumbnail`;
</script>

<div class="card border-primary h-100">
  <div class="card-header bg-secondary">
    <h5 class="card-title text-center mb-0">{lesson.title}</h5>
  </div>
  <div class="card-body">
    <div class="row">
      <div class="col-12">
        <a href="/lesson/{lesson.id}"
          ><img
            class="rounded-4 w-100"
            src={thumbnailUri}
            alt={lesson.title}
            title={lesson.title} /></a>
      </div>
    </div>
    <LessonDescription {lesson}></LessonDescription>
    <TagBadges tags={lesson.tags}></TagBadges>
  </div>
  <div class="card-footer d-flex justify-content-between align-items-baseline">
    <a href="/lesson/{lesson.id}" class="card-link">View Lesson</a>
    <LessonOwnView {lesson}>
      <div slot="owned">
        <span class="text-primary text-sm">You own this lesson</span>
      </div>
      <div slot="not-owned">
        <BuyNowButton {lesson}></BuyNowButton>
      </div>
      <div slot="free"><span class="text-primary text-sm">Free lesson</span></div>
    </LessonOwnView>
  </div>
</div>
