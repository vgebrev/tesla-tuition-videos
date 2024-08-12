<script>
  import LoginDisplay from '$components/LoginDisplay.svelte';
</script>

<nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top" data-bs-theme="dark">
  <div class="container">
    <div class="d-flex">
      <a href="/" title="Home page">
        <img src="img/ttv-logo.svg" title="Logo" alt="Logo" class="icon" />
      </a>
      <a href="/" title="Home page" class="navbar-brand d-none d-md-block pt-0">
        <span class="brand-text h3">Tesla Tuition Videos</span>
      </a>
      <a href="/" title="Home page" class="navbar-brand d-sm-block d-md-none pt-0">
        <span class="brand-text h7">Tesla Tuition Videos</span>
      </a>
    </div>

    <!--		<Announcement CssClass="d-lg-none ms-auto"></Announcement>-->
    <button
      class="navbar-toggler px-0"
      type="button"
      data-bs-toggle="collapse"
      data-bs-target="#navbar"
      aria-controls="navbar"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbar">
      <div class="navbar-nav ms-md-auto">
        <a class="nav-link" href="/lessons"><i class="bi bi-book"></i> Lessons</a>
        <!--				<ShoppingCartNavLink />-->
        <!--				<AdminNavLink />-->
        <a class="nav-link" href="/contact-us"><i class="bi bi-envelope-open"></i> Contact us</a>
        <LoginDisplay />
        <!--				<Announcement CssClass="d-none d-lg-block"></Announcement>-->
      </div>
    </div>
  </div>
</nav>

<!--<TestEnvironmentBanner />-->
