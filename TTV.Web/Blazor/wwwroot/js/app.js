const seconds = 1000;

// Bootstrap components
window.initializeCarousel = (carouselId) => {
  const carouselElem = document.getElementById(carouselId);
  new bootstrap.Carousel(carouselElem, {
    interval: 10 * seconds,
    ride: 'carousel'
  });
};

window.initPopover = (element) => {
  new bootstrap.Popover(element);
}

// Intersection observers
const observer = new IntersectionObserver((entries) => {
  entries.forEach(entry => {
    if (entry.isIntersecting) {
      entry.target.classList.add('visible');
      observer.unobserve(entry.target);
    }
  });
}, { rootMargin: "0px 0px 0px 0px" });
window.initIntersectionObserver = (element) => {
  observer.observe(element);
};

// Videos
let interval;
window.initLandingVideo = (videoId) => {
  if (interval) {
    clearInterval(interval);
  }
  interval = setInterval(() => {
    const video = document.getElementById(videoId);
    if (!video) return;
    video.pause();
    video.currentTime = 0;
    video.play();
  }, 18 * seconds);
}

window.initLessonVideo = (videoId, sourceId, videoUri) => {
  const video = document.getElementById(videoId);
  const source = document.getElementById(sourceId);
  source.src = videoUri;
  video.load();
}

// Consent cookie
window.getCookie = function (name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop().split(';').shift() === 'true';
  return false;
};

window.setCookie = function (name, value, days) {
  const date = new Date();
  date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
  const expires = `expires=${date.toUTCString()}`;
  document.cookie = `${name}=${value}; ${expires}; path=/; Secure; SameSite=Lax`;
};
