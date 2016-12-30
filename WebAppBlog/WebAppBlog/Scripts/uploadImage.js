function previewFile(event) {
    var element=event.currentTarget;
    var file = element.files[0];
    var parent = element.parentElement;
    var img = null;
    if (element.nextSibling==null || !element.nextSibling.nodeName.toLowerCase() === 'img') {
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