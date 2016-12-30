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
            input.onchange = previewFile;
           
            break;
        case "videoAdd":
            input = document.createElement("input");
            input.type = "file";
            input.className = "videoo";
            break;
    }
    return input;
}
