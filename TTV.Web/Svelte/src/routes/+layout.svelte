<script>
	import NavMenu from '$components/NavMenu.svelte';
	import AppFooter from '$components/AppFooter.svelte';
	import CookieConsent from '$components/CookieConsent.svelte';
</script>

<NavMenu />

<div class="container mb-4">
	<slot />
</div>

<AppFooter />
<CookieConsent />