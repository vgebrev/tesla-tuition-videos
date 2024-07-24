$(function () {
  $('[data-bs-toggle="popover"]').popover()
  const successMessage = document.getElementById("success-message")
  if (successMessage) {
    successMessage.scrollIntoView({ behavior: "smooth" });
  }
})
