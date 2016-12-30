
var numGallery = 0;
function allowDrop(ev) {
    ev.preventDefault();
}

function drag(ev) {
    ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("text");
    ev.currentTarget.appendChild(chooseElement(data)); //add new created element
}

function chooseElement(id) {
    var input;

    switch (id) { //switch on element id from home/index, create html element accordingly
        case "textAdd":
            input = document.createElement("textarea");
            input.placeholder = "Add more text";
            input.className = "pText";  /* p-element für css ansprechbar machen */
            break;
        case "imgAdd":
            input = document.createElement("input");
            input.type = "file";
            input.className = "image";
            input.onchange = previewImage;
            break;

        case "galleryAdd":
            input = document.createElement("input");
            input.type = "file";
            input.multiple = true;
            input.className = "image";
            input.onchange = previewGallery;
            numGallery++;
            break;

        case "videoAdd":
            input = document.createElement("input");
            input.type = "file";
            input.className = "videoo";
            break;
    }
    return input;
}

function previewImage(event) {
    var element = event.currentTarget;
    var file = element.files[0];
    var parent = element.parentElement;
    var img = null;
    if (element.nextSibling == null || !element.nextSibling.nodeName.toLowerCase() === 'img') {
        img = document.createElement("img");
        parent.insertBefore(img, element.children[0]);
    } else {
        img = element.nextSibling;
    }

    var reader = new FileReader();

    reader.addEventListener("load", function () {
        img.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

function previewGallery(event) {

    var element = event.currentTarget;
    var classname = "galeryPreview" + numGallery;
    var parent = element.parentElement;
    var container = document.createElement("div");
    parent.insertBefore(container, element.children[0]);
    if (element.files)
     
    for(var file of element.files) {
        if (element.nextSibling.nodeName.toLowerCase() === 'div') {

                var img = document.createElement("img");
                img.className = classname;
                img.style = "width:100%";
                container.appendChild(img);
                readGallery(file, img);
        }
    }

    var button1 = document.createElement('a');
    button1.innerHTML = "Button One";
    button1.onclick = plusDivs;
    var button2 = document.createElement('a');
    button2.innerHTML = "Button Two";
    button2.onclick = plusDivs;
    container.appendChild(button1);
    container.appendChild(button2);

    showDivs(1, classname);
    
}

function readGallery(file, img) {
    var reader = new FileReader();

    reader.addEventListener("load", function () {
        img.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}


var slideIndex = 1;


function plusDivs() {
    alert("Button clicked");
    //TODO: add cyclin between images.
    //showDivs(slideIndex += n, classname);
}

function showDivs(n, classname) {
    var i;
    var x = document.getElementsByClassName(classname);
    if (n > x.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = x.length }
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    x[slideIndex - 1].style.display = "block";
}

