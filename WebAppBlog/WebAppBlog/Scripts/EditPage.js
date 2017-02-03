
window.onload =function () {
    //var deleters = document.getElementsByClassName("deleter");

   /* for(var div of deleters) {
        div.onclick = deleteElement;
    }*/

};




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
}

function deleteExistingElement(index) {
    console.log(index);
    $.ajax({
        url: '/api/ExternBlogApi/RemoveById/?id=' + index,
        type: 'GET', //in later release: change to delete!
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            window.location.href = "/ExternBlog/Edit/" + result;
        }
    });
}

function deleteElement(event) {
    var element = event.currentTarget;
    //remove elements
    var sibling = element.nextSibling;
    var parent = element.parentElement;
    parent.removeChild(sibling);
    parent.removeChild(element);
}

function updateBlog() {

    var blog = create();
    Send(blog);
}


function create() {
    var blogdata = document.getElementById("blogdata"); // get blog data
    var blogdatacontinued = document.getElementById("blogdata-continued"); // get blog data in drop area
    var blog = {};
    blog.titel = "";
    blog.subtitel = "";
    blog.text = [];
    blog.images = [];
    blog.videos = [];
    blog.gmapsMarker = null;


    var position = { counter: 0 };
    for (var element of blogdata.children) {
        extratElements(element, blog, position);
    }

    for(var element of blogdatacontinued.children)
    {
        extratElements(element, blog, position);
    }
    console.log(blog);
    return blog;

}

function extratElements(element, blog, position) {
    switch (element.localName) {
        case "div": //go trough children and add accordingly
            //TODO: handle file input! (single) correctly

            if (element.className === "gallery-container" && element.children[0].nodeName.toLowerCase() === "img") {

                //gallery found

                var el = element.nextSibling;
                for(var imgs of element.children)
                {
                    extratElements(imgs, blog, position);
                }

            }



            break;
        case "input":
            if (element.type === "text") {
                if (element.id === "blog-titel") {
                    blog.titel = element.value;
                } else
                    if (element.id === "blog-subtitle") {
                        blog.subtitel = element.value;
                    } else {

                        var text = {};
                        text.value = element.value;
                        text.position = position.counter;
                        blog.text.push(text);
                        position.counter++;
                    }
            }

            break;
        case "textarea":

            var text = {};
            text.value = element.value;
            text.position = position.counter;
            blog.text.push(text);

            position.counter++;
            break;

        case "iframe":
            var video = {};
            video.src = element.src;
            video.position = position.counter;
            blog.videos.push(video);

            position.counter++;
            break;

        case "img":
            var image = {};

            if (element.className.includes("galeryPreview")) {
                image.gallery = true;

                image.name = element.className + "_" + element.name;
                image.position = position.counter;
                image.galleryName = element.className;
                position.counter++;
                blog.images.push(image);
            } else {

                image.gallery = false;
                image.name = element.name;
                image.position = position.counter;
                position.counter++;
                blog.images.push(image);
            }
            break;
    }
}


function cancel() {
    $.ajax({
        type: "GET",
        url: "/api/ExternBlogApi/CancelBlogUpdate",
        contentType: "application/json; charset=utf-8",
        complete: function (data) {
            if (data.readyState === 4 & data.status === 200) {

                //workaround to have the current page be added to browser history
                setTimeout(function () {
                    window.location.href = "/Blog/Overview";
                }, 0)
            }
        }
    });
}

function Send(blog) {
    $.ajax({
        type: "POST",
        url: "/api/ExternBlogApi/UpdateBlog",
        data: JSON.stringify({ blog }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function (data) {
            if (data.readyState === 4 & data.status === 200) {
               
                //workaround to have the current page be added to browser history
                setTimeout(function () {
                    window.location.href = "/Blog/Overview";
                }, 0)
            }
        }
    });
}