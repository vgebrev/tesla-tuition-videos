<script>
  import { config } from '$lib/config.js';

  const whatsAppLink = `https://wa.me/${config.contactInfo.phoneNumber.replace(/\s+/g, '').replace('+', '')}`;
  const telLink = `tel:${config.contactInfo.phoneNumber.replace(/\s+/g, '')}`;
  const mailtoLink = `mailto:${config.contactInfo.supportEmail}`;
</script>

<div class="row justify-content-center">
  <div class="col text-center">
    <h1 class="header-white">Contact Us</h1>
  </div>
</div>
<div class="row">
  <div class="col m-2 m-md-5 text-center">
    <p class="fs-5 text-white">We are here to help you. Please feel free to contact us using the following methods:</p>
  </div>
</div>
<div class="row">
  <div class="col-12 col-sm-12 col-md-6 col-lg-4">
    <div class="d-flex flex-column m-2">
      <span class="fs-5"><i class="bi bi-whatsapp"></i> WhatsApp</span>
      <a
        href={whatsAppLink}
        target="_blank"
        class="btn btn-large btn-outline-primary">{config.contactInfo.phoneNumber}</a>
    </div>
  </div>
  <div class="col-12 col-sm-12 col-md-6 col-lg-4">
    <div class="d-flex flex-column m-2">
      <span class="fs-5"><i class="bi bi-telephone"></i> Call</span>
      <a
        href={telLink}
        class="btn btn-large btn-outline-primary">{config.contactInfo.phoneNumber}</a>
    </div>
  </div>
  <div class="col-12 col-sm-12 col-md-12 col-lg-4">
    <div class="d-flex flex-column m-2">
      <span class="fs-5"><i class="bi bi-envelope-at"></i> Email</span>
      <a
        href={mailtoLink}
        class="btn btn-large btn-outline-primary">{config.contactInfo.supportEmail}</a>
    </div>
  </div>
</div>
<div class="row">
  <div class="col mt-5 text-center">
    <p class="fs-6 text-white">Get in touch on social media:</p>
  </div>
</div>
<div class="row">
  <div class="col text-center">
    <div class="d-flex gap-5 justify-content-center">
      <a
        href="https://www.instagram.com/teslatuitionvideos/"
        target="_blank"
        title="Instagram"><i class="fs-1 bi bi-instagram"></i></a>
      <a
        href="https://www.facebook.com/teslatuitionvideos/"
        target="_blank"
        title="Facebook"><i class="fs-1 bi bi-facebook"></i></a>
      <a
        href="https://www.youtube.com/@TeslaTuitionVideos"
        target="_blank"
        title="YouTube"><i class="fs-1 bi bi-youtube"></i></a>
    </div>
  </div>
</div>
