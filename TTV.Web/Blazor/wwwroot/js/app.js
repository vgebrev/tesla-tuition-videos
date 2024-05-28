const seconds = 1000;

window.initializeCarousel = (carouselId) => {
  const carouselElem = document.getElementById(carouselId);
  new bootstrap.Carousel(carouselElem, {
    interval: 10 * seconds,
    ride: 'carousel'
  });
};

window.initIntersectionObserver = (element) => {
  const observer = new IntersectionObserver((entries) => {
    entries.forEach(entry => {
      if (entry.isIntersecting) {
        entry.target.classList.add('visible');
        observer.unobserve(entry.target);
      }
    });
  }, { threshold: 1.0 });

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
  }, 21 * seconds);
}