const seconds = 1000;

window.initializeCarousel = (carouselId) => {
  const carouselElem = document.getElementById(carouselId);
  new bootstrap.Carousel(carouselElem, {
    interval: 10 * seconds,
    ride: 'carousel'
  });
};

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