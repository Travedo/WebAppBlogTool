function create()
{
    var blogdata = document.getElementById("blogdata");
    var jsonObject = {};
    jsonObject.text = [];
    jsonObject.images = [];
    jsonObject.gmapsMarker = {};

    var positionCounter = 0;
    for (var element of blogdata.children) {
        switch (element.localName) {
            case "input":

                if (element.id === "blog-titel")
                {
                    jsonObject.titel = element.value;
                } else
                    if (element.id === "blog-subtitle") {
                    jsonObject.subtitel = element.value;
                } else {

                    var text = {};
                    text.value = element.value;
                    text.position = positionCounter;
                    jsonObject.text.push(text);
                    positionCounter++;
                }
               
                break;
            case "textarea":

                var text = {};
                text.value = element.value;
                text.position = positionCounter;
                jsonObject.text.push(text);

                positionCounter++;
                break;
            
            
        }
        
    }
    console.log(jsonObject);

    $.ajax({
        type: "POST",
        url: "/api/BlogApi/CreateBlog/",
        data: JSON.stringify({ jsonObject }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { alert("sucess"); },
        failure: function (errMsg) {
            alert("fail");
        }
    });
}