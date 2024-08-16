<script>
  import LessonDetail from '$components/LessonDetail/LessonDetail.svelte';

  export let data;

  $: lessonId = data.lessonId;
</script>

<svelte:head>
  <title>Tesla Tuition Videos</title>
</svelte:head>

<LessonDetail {lessonId} />
