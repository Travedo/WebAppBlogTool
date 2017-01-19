function createBlog() {

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
    blog.gmapsMarker = "";

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
        case "input": //go trough children and add accordingly
            //TODO: handle file input! (single) correctly

            if (element.type === "file") {
                
                //gallery found
                if (element.nextSibling && element.nextSibling.nodeName.toLowerCase() === "div" && element.nextSibling.children[0].nodeName.toLowerCase() === "img") {
                    var el = element.nextSibling;
                    for(var imgs of el.children)
                    {
                        extratElements(imgs, blog, position);
                    }
                    //single image
                } else if (element.nextSibling.nodeName.toLowerCase() === 'img') {

                    var image = {};
                    image.name = element.nextSibling.name;
                    image.gallery = false;
                    image.position = position.counter;
                    position.counter++;
                    blog.images.push(image);

                }
            }
            else
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

            if (element.className.includes("galery")){
                image.gallery = true;
            



            image.name = element.className+"_"+element.name;
            image.position = position.counter;
            image.galleryName = element.className;
            position.counter++;
            blog.images.push(image);
            }
            break;
    }
}