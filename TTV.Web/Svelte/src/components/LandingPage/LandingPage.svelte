<script>
	import { setContext } from 'svelte';
	import Intro from '$components/LandingPage/Intro.svelte';
	import MeetTheTeacher from '$components/LandingPage/MeetTheTeacher.svelte';
	import Features from '$components/LandingPage/Features.svelte';
	import Testimonials from '$components/LandingPage/Testimonials.svelte';

	const observer = new IntersectionObserver((entries) => {
	  entries.forEach(entry => {
	    if (entry.isIntersecting) {
	      entry.target.classList.add('visible');
	      observer.unobserve(entry.target);
	    }
	  });
	}, { rootMargin: "0px 0px 0px 0px" });

	setContext('observer', observer);
</script>

<div class="row">
	<Intro />
</div>
<div class="row">
	<MeetTheTeacher />
</div>
<div class="row">
	<Features />
</div>
<div class="row">
	<Testimonials />
</div>