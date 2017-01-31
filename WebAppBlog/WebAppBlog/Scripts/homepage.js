var slideIndex = 1;

showSlides(1);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}


/* Responsive Images */
Response.create({
    prop: "width"  // "width" "device-width" "height" "device-height" or "device-pixel-ratio"
  , prefix: "min-width- r src"  // the prefix(es) for your data attributes (aliases are optional)
  , breakpoints: [1000, 760, 320, 0] // min breakpoints (defaults for width/device-width)
  , lazy: true // optional param - data attr contents lazyload rather than whole page at once
});
