function initSlideChange(event) {
    var element = event.currentTarget;
    var parent = element.parentElement;
    var images = document.getElementsByClassName(parent.children[0].className);

    var i = 0;
    //get currently displayed img as index
    for(var img of images) {
        i++;
        if (img.style.display === "block") {
            break;
        }
    }

    //pass index+/-1 to show new img
    if (element.innerHTML === "&#10094;") {
        changeSlides(i - 1, parent.children[0].className);
    } else {
        changeSlides(i + 1, parent.children[0].className);
    }
}

function changeSlides(n, classname) {
    var i;
    var imgInGallery = document.getElementsByClassName(classname);
    if (n > imgInGallery.length) { n = 1 }
    if (n < 1) { n = imgInGallery.length }

    //hide all imgs
    for (i = 0; i < imgInGallery.length; i++) {
        imgInGallery[i].style.display = "none";
    }

    //show img based on calculated index
    imgInGallery[n - 1].style = "display:block;"
    setTimeout(function () { changeSlides(n + 1, classname); }, 5000); //switch slides every 5 sec automatically
}

function createNewBlog() {

    $.ajax({
        type: "GET",
        url: "/api/ExternBlogApi/CreateBlog",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function (data) {
            if (data.readyState === 4 & data.status === 200) {
                console.log(data);
                window.location.href = window.location.href = "http://localhost:62781/ExternBlog/ShowBlog/" + data.responseText;
            }
        }
    });


}