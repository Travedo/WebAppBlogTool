function createBlog() {

    var blog = create();
    sendData("/blog/index", create());
}

function createDemoBlog() {

    sendData("/Demo/MyTemporaryDemoBlog", create());
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
    blog.images = "";
    blog.gmapsMarker = "";

    var positionCounter = 0;
    for (var element of blogdata.children) {
        extratElements(element, blog, positionCounter);
    }
    
    for(var element of blogdatacontinued.children)
    {
        extratElements(element, blog, positionCounter);
    }
    console.log(blog);

    
    return blog;
   
}

function extratElements(element, blog, positionCounter) {
    switch (element.localName) {
        case "input": //go trough children and add accordingly
            //TODO: handle file input (single) correctly
            if (element.id === "blog-titel") {
                blog.titel = element.value;
            } else
                if (element.id === "blog-subtitle") {
                    blog.subtitel = element.value;
                } else {

                    var text = {};
                    text.value = element.value;
                    text.position = positionCounter;
                    blog.text.push(text);
                    positionCounter++;
                }

            break;
        case "textarea":

            var text = {};
            text.value = element.value;
            text.position = positionCounter;
            blog.text.push(text);

            positionCounter++;
            break;

            //TODO: handle video correctly
        case "iframe":
            var text = {};
            text.value = element.src;
            text.position = positionCounter;
            blog.text.push(text);

            positionCounter++;
            break;

            //TODO: handle gallery correctly
    }
}