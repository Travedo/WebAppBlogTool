
var numGallery = 0;
var myId = null;

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
            input.className = "gallery";
            input.onchange = previewGallery;
            numGallery++;
            break;

        case "videoAdd":
            input = document.createElement("textarea");
            input.placeholder = "insert Youtube link and hit enter";
            input.onkeydown = insertYoutubeVideoOnEnter;
            input.className = "videoo";
            break;

        case "mp3Add":
            input = document.createElement("input");
            input.type = "file";
            input.onchange = createMusicPlayer;
            break;
    }
    return input;
}

//==== image creation ====/

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

        var image = [{ name: file.name, base64: reader.result }];
        sendImageData(image);
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

//==== gallery creation =====/

function previewGallery(event) {

    var element = event.currentTarget;
    var classname = "galeryPreview" + numGallery;
    var parent = element.parentElement;
    var container = null;
    var status = { info: 0 };

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
   
    if (element.files) { //if files have been selected

        var image = [];
        for(var file of element.files) {

              //create img tag
            var img = document.createElement("img");
            img.className = classname;
            img.style = "width:100%";
             //add img to container
            container.appendChild(img);

            var currentImg = { name: classname + "_" + file.name, base64: "" };
            image.push(currentImg);
             //use filereader to read selected file
            readGallery(file, img, currentImg, status)

        }
    }
    
    //wait until all images have been processed, then send to server
    myId =setInterval(function(){ sendGallery(status,element.files.length,image,myId)},2000);


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


function sendGallery(status, length,images,myId)
{
    if (status.info === length) {
        sendImageData(images);
        clearInterval(myId);
    }
}

//reads selected images
function readGallery(file, img, currentImg, status) {
    var reader = new FileReader();

    reader.addEventListener("load", function () {
        img.src = reader.result;
        currentImg.base64 = reader.result;
        status.info++;
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


//====== youtube video creation =======//

function insertYoutubeVideoOnEnter(event){
    if (event.code === "Enter") {
        var currentTarget = event.currentTarget;
        var value = currentTarget.value;
        if(value.startsWith("https://www.youtube.")){
           
            var parent = currentTarget.parentElement;
            var video = document.createElement("iframe");
            video.height = 315;
            video.width = 560;
            value = value.replace('watch?v=', 'embed/');
            video.src = value;
            video.frameBorder = "0";
            video.allowFullscreen = true;

            parent.insertBefore(video, currentTarget.children[0]);
            parent.removeChild(currentTarget);
        }
    }
}


//==== Music player ==== //
function createMusicPlayer(event) {
    var element = event.currentTarget;
    var file = element.files[0];
    var parent = element.parentElement;
    var audio = null;
    if (element.nextSibling == null || !element.nextSibling.nodeName.toLowerCase() === 'audio') {
        audio = document.createElement("audio");
        audio.controls = true;
        parent.insertBefore(audio, element.children[0]);
    } else {
        audio = element.nextSibling;
    }

    //todo: send data to rest api
    //insert audio source

}

//ajax send imges

function sendImageData( images) {
    $.ajax({
        type: "POST",
        url: "/api/BlogApi/AddImages/",
        data: JSON.stringify({ images }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    });
}
