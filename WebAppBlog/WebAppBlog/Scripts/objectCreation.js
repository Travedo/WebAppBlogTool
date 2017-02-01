function createBlog() {
    var titel = $("#blog-titel");
    if (titel[0].value === "undefined" ||titel[0].value === "") {
        titel.addClass("error-empty-title");
       
        return;
    } else {
        titel.removeClass("error-empty-title");
    }
    var blog = create();
    sendData("/blog/preview", create());
}



function sendData(url, blog){
    $.ajax({
        type: "POST",
        url: "/api/BlogApi/CreateBlog/",
        data: JSON.stringify({ blog }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function (data) {
            if (data.readyState === 4 & data.status === 200) {
                savePartiallyChangedView();
                //workaround to have the current page (create page) be added to browser history
                setTimeout(function () {
                    window.location.href=url;
                }, 0)
            }
        }
    });
}



function create()
{
    var blogdata = document.getElementById("blogdata"); // get blog data
    var blogdatacontinued = document.getElementById("blogdata-continued"); // get blog data in drop area
    var blog = {};
    blog.titel = "";
    blog.subtitel = "";
    blog.text = [];
    blog.images = [];
    blog.videos = [];
    blog.gmapsMarker = gmapsMarkerArray;
    

    var position = { counter:0};
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

            if (element.className.includes("galeryPreview")){
                image.gallery = true;
  
            image.name = element.className+"_"+element.name;
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