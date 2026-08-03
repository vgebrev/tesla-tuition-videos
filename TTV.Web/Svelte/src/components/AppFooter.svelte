<script>
  import { resolve } from '$app/paths';

  const currentYear = new Date().getFullYear();
</script>

<footer
  class="bg-dark text-center text-lg-start py-1 small"
  data-bs-theme="dark">
  <div class="d-flex justify-content-center">
    <div class="text-center p-3">
      Copyright &copy; 2024-{currentYear} Tesla Tuition Videos | All Rights Reserved |
      <a href={resolve('/terms-and-conditions')}>Terms and Conditions</a>
      | <a href={resolve('/privacy-policy')}>Privacy Policy</a> |
      <a href={resolve('/cookies-policy')}>Cookies Policy</a>
      <br />
      Lesson icons and background images designed by
      <a
        target="_blank"
        href="https://www.freepik.com/">Freepik</a>
      | Logo designed by Kiki's Creations | Website theme designed by
      <a
        target="_blank"
        href="https://bootswatch.com/">Bootswatch</a>
    </div>
    <img
      src="img/made-in-south-africa-icon.svg"
      title="Made in South Africa"
      alt="Made in South Africa"
      class="icon-lg" />
  </div>
</footer>
