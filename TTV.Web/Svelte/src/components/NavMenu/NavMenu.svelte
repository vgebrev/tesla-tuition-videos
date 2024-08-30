<script>
  import LoginNavLink from '$components/NavMenu/LoginNavLink.svelte';
  import ShoppingCartNavLink from '$components/NavMenu/ShoppingCartNavLink.svelte';
  import Announcement from '$components/NavMenu/Announcement.svelte';
  import AdminNavLink from '$components/NavMenu/AdminNavLink.svelte';
  import NavLink from '$components/NavMenu/NavLink.svelte';
  import TestEnvironmentBanner from '$components/TestEnvironmentBanner.svelte';
</script>

<nav
  class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top"
  data-bs-theme="dark">
  <div class="container">
    <div class="d-flex">
      <NavLink
        title="Home page"
        cssClass=""
        target=".navbar-collapse.show"
        ><img
          src="img/ttv-logo.svg"
          title="Logo"
          alt="Logo"
          class="icon" /></NavLink>
      <NavLink
        title="Home page"
        cssClass="navbar-brand d-none d-md-block pt-0"
        target=".navbar-collapse.show">
        <span class="brand-text h3">Tesla Tuition Videos</span>
      </NavLink>
      <NavLink
        title="Home page"
        class="navbar-brand d-sm-block d-md-none pt-0"
        target=".navbar-collapse.show">
        <span class="brand-text h7">Tesla Tuition Videos</span>
      </NavLink>
    </div>

    <Announcement cssClass="d-lg-none ms-auto"></Announcement>
    <button
      class="navbar-toggler px-0"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbar"
      aria-controls="navbar"
      aria-expanded="false"
      aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>

    <div
      class="collapse navbar-collapse"
      id="navbar">
      <div class="navbar-nav ms-md-auto">
        <NavLink href="/lessons"><i class="bi bi-book"></i> Lessons</NavLink>
        <ShoppingCartNavLink />
        <AdminNavLink />
        <NavLink href="/contact-us"><i class="bi bi-envelope-open"></i> Contact us</NavLink>
        <LoginNavLink />
        <Announcement cssClass="d-none d-lg-block"></Announcement>
      </div>
    </div>
  </div>
</nav>

<TestEnvironmentBanner />

<style>
  .fixed-top {
    z-index: 1;
  }
</style>
