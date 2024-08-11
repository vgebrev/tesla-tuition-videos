<script>
  import { onMount } from 'svelte';
  import { observer } from '$lib/intersection-observer.js';
  import Testimonial from '$components/LandingPage/Testimonial.svelte';

  const testimonials = [
    {
      seq: 1,
      testimonialBy: 'Tristan van Rensburg',
      text: "These science videos have been incredibly helpful in expanding my understanding of complex concepts, making learning engaging and accessible. They've played a vital role in broadening my knowledge and sparking my curiosity in various scientific fields."
    },
    {
      seq: 2,
      testimonialBy: 'Akhela Kalpage',
      text: 'I love Tesla Tuition Videos because it teaches me any sections in Physics or Chemistry that I don’t understand. It is also helpful when I forget a section and I can just watch the video to refresh my memory.'
    },
    {
      seq: 3,
      testimonialBy: 'Jade Celgow',
      text: 'I was shown a brand new approach towards Physical Sciences. Physical Sciences became fun, enjoyable and interesting through these incredibly engaging and informative lessons. The results truly showed too. I would highly recommend to all students!'
    },
    {
      seq: 4,
      testimonialBy: 'Nabeel Petkaar',
      text: "All thanks to Lesley's patience, positive energy and the ability to simplify complex topics, I have seen a noticeable improvement in my grades and understanding of the topics. I highly recommend Lesley as your Science Tutor."
    },
    {
      seq: 5,
      testimonialBy: 'Khelan Desai',
      text: 'Thanks to Lesley’s incredible tutoring, I was able to excel in Physical Sciences and achieve in the top 1% nationally. I cannot thank her enough for her amazing guidance and insight in my journey!'
    },
    {
      seq: 6,
      testimonialBy: 'Mrittika Mahendra',
      text: 'Lesley has positively impacted my learning and I have made an improvement in my science marks and understanding of the subject. The lessons are well planned as she always provides past exam questions to assist you in forthcoming exams or tests and specific questions to assist you in a topic that you struggle with.'
    },
    {
      seq: 7,
      testimonialBy: 'Kayleigh Butler',
      text: "Lesley's guidance has been instrumental in elevating my understanding of science. Her clear explanations and patient approach made complex concepts seem manageable. With Lesley's support, my confidence in tackling scientific challenges grew significantly. Her bubbly and kind personality translated through not only her teaching but also to me as a person. She gave me a passion for science that I never knew I had."
    },
    {
      seq: 8,
      testimonialBy: 'Abdullah Mansuri',
      text: 'Ms Robinson is a wonderful science tutor whose dynamic teaching style and care for her students is a game-changer that makes learning both enjoyable and impactful. Ms Robinson’s ability to make complex concepts understandable and exciting has enriched my understanding of science and for me, has resulted in improved grades and a newfound confidence in science.'
    },
    {
      seq: 9,
      testimonialBy: 'Hannah Melling',
      text: 'Lesley was a wonderful Science tutor. I sought her halfway through my matric year (2020). Within only a couple months, she boosted not only my understanding of the subject but also my confidence in the subject. She helped me so so much and I am so grateful and attribute my final A in science to her.'
    },
    {
      seq: 10,
      testimonialBy: 'Kyle Van Eyssen',
      text: "I enrolled at Tesla Tuition with the aim of improving my high school marks in maths and science for admission to a more advanced university program. Lesley, my exceptional teacher, was not only patient and gentle but also deeply passionate about science. Under Lesley's guidance, I transformed from a struggling high school student to earning two degrees in accounting science, setting me on the path to becoming a chartered accountant."
    },
    {
      seq: 11,
      testimonialBy: 'Devany Markovic',
      text: "Lesley’s exceptional tutoring skills are defined by her unwavering patience, kindness and ability to match you at your level. Whether you're grappling with difficult concepts or aiming for high grades, Lesley’s adaptable approach caters to every student's unique learning needs."
    },
    {
      seq: 12,
      testimonialBy: 'Lauren Lachman',
      text: "Lesley's dedication and patience had a successful impact on my results. I am grateful for her support and guidance throughout our lessons. She is a wonderful tutor!"
    },
    {
      seq: 13,
      testimonialBy: 'Emma Steyn',
      text: 'Lesley was my physical science tutor in high school and was the best tutor I could have asked for - she helped me to understand and enjoy the work that we covered in chemistry and physics. She is single-handedly the reason I was able to get into a BSc degree in university. I would recommend Lesley to anyone and everyone who takes physical science.'
    },
    {
      seq: 14,
      testimonialBy: 'Tayla Booth',
      text: "Lesley Robinson came to my rescue during a period of utter despair with physics. Seeking her guidance, she led me through interactive tutoring sessions, unravelling the complexities of the subject. Thanks to her invaluable assistance, I achieved a distinction in Science in my Matric year. Lesley's support, patience, and willingness to explain concepts repeatedly was fundamental in my success. I am extremely grateful to her for not only helping me academically but also for sparking a newfound passion for science within me. Thank you, Lesley, for everything."
    }
  ];
  let headerElem;
  let carouselElem;

  onMount(() => {
    observer.observe(headerElem);

    new bootstrap.Carousel(carouselElem, {
      interval: 10 * 1000,
      ride: 'carousel'
    });
  });
</script>

<div class="bg-classroom p-3 mt-1 mt-md-3 p-md-3 min-vh-75">
  <div class="m-2 mt-5 m-md-5 p-1 p-md-3 bg-body opacity-90 h-100-6rem">
    <h1 bind:this={headerElem} class="fade-in header-white text-opacity-100 p-2">Testimonials</h1>
    <div
      bind:this={carouselElem}
      id="testimonialsCarousel"
      class="carousel slide h-100-6rem"
      data-bs-ride="carousel"
    >
      <div class="carousel-indicators">
        {#each testimonials as testimonial, i}
          <button
            type="button"
            data-bs-target="#testimonialsCarousel"
            data-bs-slide-to={i}
            class:active={i === 0}
            aria-label="Slide {testimonial.seq}"
          ></button>
        {/each}
      </div>
      <div class="carousel-inner h-100 d-flex align-items-center">
        {#each testimonials as testimonial, i}
          <div class="carousel-item" class:active={i === 0}>
            <Testimonial {testimonial} />
          </div>
        {/each}
      </div>
      <button
        class="carousel-control-prev"
        type="button"
        data-bs-target="#testimonialsCarousel"
        data-bs-slide="prev"
      >
        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Previous</span>
      </button>
      <button
        class="carousel-control-next"
        type="button"
        data-bs-target="#testimonialsCarousel"
        data-bs-slide="next"
      >
        <span class="carousel-control-next-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Next</span>
      </button>
    </div>
  </div>
</div>
