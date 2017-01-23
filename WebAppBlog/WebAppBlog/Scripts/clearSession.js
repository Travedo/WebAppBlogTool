function isStorageUsable() {
    if (typeof (Storage) !== "undefined") {
        return true;
    } else {
        return false;
    }
}

$(document).ready(function () {

    if (isStorageUsable) {
        sessionStorage.createPage = undefined;
    }
       
});