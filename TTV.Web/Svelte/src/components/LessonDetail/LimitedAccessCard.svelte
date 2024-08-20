<script>
  import LessonOwnView from '$components/common/LessonOwnView.svelte';
  import BuyNowButton from '$components/common/BuyNowButton.svelte';
  import AuthorizeView from '$components/common/AuthorizeView.svelte';
  import LoginLink from '$components/common/LoginLink.svelte';

  /** @type {import('$lib/types').Lesson} */
  export let lesson;
</script>

<LessonOwnView {lesson}>
  <div
    slot="not-owned"
    class="card border-warning mb-3">
    <div class="card-body d-flex align-items-center justify-content-center flex-wrap">
      <span class="card-text">Limited access. Please purchase the lesson for full access. </span>
      <BuyNowButton
        {lesson}
        class="mx-2 my-2 my-md-0 btn btn-outline-warning"></BuyNowButton>
      <AuthorizeView>
        <span
          slot="anonymous"
          class="card-text"
          >If you have already purchased the lesson, please <LoginLink class="text-warning">Log in</LoginLink>.</span>
      </AuthorizeView>
    </div>
  </div>
</LessonOwnView>
