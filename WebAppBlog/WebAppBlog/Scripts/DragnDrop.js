﻿
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
    var container = null;

    //if we already have a div and children of img
    if (element.nextSibling && element.nextSibling.nodeName.toLowerCase() === "div" && element.nextSibling.children[0].nodeName.toLowerCase() === "img") {
        var el = element.nextSibling;
        //delete all children
        while (el.hasChildNodes())
            el.removeChild(el.lastChild);

        container = el;
    }
    else {
        //else create new container
        container = document.createElement("div");
        container.style = "position: relative;";
        parent.insertBefore(container, element.children[0]);
    }

    if (element.files) //if files have been selected
     
        for(var file of element.files) {

                //create img tag
                var img = document.createElement("img");
                img.className = classname;
                img.style = "width:100%";
                //add img to container
                container.appendChild(img);
                
                //use filereader to read selected file
                readGallery(file, img);
    }

    //add 'nav' buttons to slideshows
    var button1 = document.createElement('a');
    button1.innerHTML = "&#10094";
    button1.onclick = plusDivs;
    button1.className = "galleryButtonLeft";
    var button2 = document.createElement('a');
    button2.innerHTML = "&#10095;";
    button2.onclick = plusDivs;
    button2.className = "galleryButtonRight";
    container.appendChild(button1);
    container.appendChild(button2);

    //init slide show
    showDivs(1, classname);
}

//reads selected images
function readGallery(file, img) {
    var reader = new FileReader();

    reader.addEventListener("load", function () {
        img.src = reader.result;
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

//+/- slideshow imgs
function plusDivs(event) {
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
        showDivs(i-1, parent.children[0].className);
    } else {
        showDivs(i+1, parent.children[0].className);
    }
}

function showDivs(n, classname) {
    var i;
    var imgInGallery = document.getElementsByClassName(classname);
    if (n > imgInGallery.length) { n = 1 }
    if (n < 1) { n = imgInGallery.length }

    //hide all imgs
    for (i = 0; i < imgInGallery.length; i++) {
        imgInGallery[i].style.display = "none";
    }

    //show img based on calculated index
    imgInGallery[n - 1].style.display = "block";
}

