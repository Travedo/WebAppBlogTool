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
                window.location.replace(url);
            }
        }
    });
}



function create()
{
    var blogdata = document.getElementById("blogdata"); // get blog data
    var blog = {};
    blog.titel = "";
    blog.subtitel = "";
    blog.text = [];
    blog.images = "";
    blog.gmapsMarker = "";

    var positionCounter = 0;
    for (var element of blogdata.children) {

        switch (element.localName) {
            case "input": //go trough children and add accordingly

                if (element.id === "blog-titel")
                {
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
        }
    }
    console.log(blog);

    
    return blog;
   
}