function savePartiallyChangedView() {
  
    if (isStorageUsable) {
        var dynamic = [];
        values = [];
        var data=document.getElementById("blogdata-continued");
        var children = document.getElementById("blogdata-continued").children;
        for(var element of children)
        {
            processData(element, dynamic);
        }

        try{
            sessionStorage.createPage = JSON.stringify({ dynamic });
        } catch (error) {
            //send data to server!
            console.log("Data exceeded session storage range, sending to server instead.");
            sessionStorage.createPage = null;
        }

    } else {
        alert("Information entered on Create page could NOT be saved dynamically. Please use the edit button for editing.");
    }


};

function processData(element, dynamic) {
    switch (element.localName)
    {
        case "textarea":
            var obj = { html: element.outerHTML, value: element.value };
            dynamic.push(obj);

            break;

        case "input":
            var obj = { html: element.outerHTML};
            dynamic.push(obj);

            break;
        case "div":
            var obj = { html: element.outerHTML };
            dynamic.push(obj);
            break;

        case "iframe":
            var obj = { html: element.outerHTML };
            dynamic.push(obj);
            break;
        case "img":
            var obj = { html: element.outerHTML };
            dynamic.push(obj);
            break;
    }


};

function retrieveSessionData()
{
    if (isStorageUsable) {
        if (sessionStorage.createPage)
            return sessionStorage.createPage
        else {
            //get data from server instead
            return null;
        }
    }
}

function isStorageUsable() {
    if (typeof (Storage) !== "undefined") {
        return true;
    } else {
        return false;
    }
}

$(document).ready(function () {

    if (retrieveSessionData()) {
        var data = retrieveSessionData();
        var container = document.getElementById("blogdata-continued");
        var existingChilden = [];

        //copy already existing children of container
        for(var child of container.children)
        {
            existingChilden.push(child.outerHTML);
        }

        //delete all children
        while (container.hasChildNodes())
            container.removeChild(container.lastChild);

        //get json object from storage
        var object = $.parseJSON(data);

        //iterate over elements in array
        for (var element of object.dynamic)
        {
            var div = document.createElement('div');
            //copy element.html into div (=> child of div)
            div.innerHTML = element.html;

            //get restored html object
            var elements = div.children[0];

            //add value, if available
            if (element.value)
                elements.value = element.value;

            //append recunstructed html object
            container.appendChild(elements);
        }

        //add back prior removed objects
        for(var child of existingChilden)
        {
            var div = document.createElement('div');
            div.innerHTML = child;
            var elements = div.childNodes;
            container.appendChild(elements[0]);
        }

    } else
        console.log("no session data found");

});