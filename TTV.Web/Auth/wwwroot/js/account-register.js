$(function () {
  $('[data-bs-toggle="popover"]').popover()
  const successMessage = document.getElementById("success-message")
  if (successMessage) {
    successMessage.scrollIntoView({ behavior: "smooth" });
  }
  const password = document.getElementById('password');
  password.addEventListener('input', checkPasswordStrength);
})

function checkPasswordStrength() {
  const password = document.getElementById('password').value;
  const strengthBar = document.getElementById('password-strength-bar');
  let strength = 0;

  // Length criteria
  if (password.length >= 20) {
    strength += 6;
  } else if (password.length >= 16) {
    strength += 5;
  } else if (password.length >= 12) {
    strength += 3;
  } else if (password.length >= 8) {
    strength++;
  }

  // Character type criteria
  if (/[A-Z]/.test(password)) strength++;
  if (/[a-z]/.test(password)) strength++;
  if (/[0-9]/.test(password)) strength++;
  if (/[^A-Za-z0-9]/.test(password)) strength++;

  switch (true) {
    case (strength <= 2):
      strengthBar.className = 'progress-bar bg-danger w-25';
      strengthBar.setAttribute('aria-valuenow', 25);
      strengthBar.innerText = "Weak";
      break;
    case (strength <= 4):
      strengthBar.className = 'progress-bar bg-warning w-50';
      strengthBar.setAttribute('aria-valuenow', 50);
      strengthBar.innerText = "Medium";
      break;
    case (strength <= 6):
      strengthBar.className = 'progress-bar bg-success w-75';
      strengthBar.setAttribute('aria-valuenow', 75);
      strengthBar.innerText = "Strong";
      break;
    case (strength >= 7):
      strengthBar.className = 'progress-bar bg-success w-100';
      strengthBar.setAttribute('aria-valuenow', 100);
      strengthBar.innerText = "Very Strong";
      break;
  }
}
