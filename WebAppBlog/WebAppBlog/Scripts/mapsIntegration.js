var gmapsMarkerArray = [];

function initMap() {

    var mapOptions = {
        center: {  lat: 1.3523784000, lng: 103.9847063000},
        zoom: 8
    };
    var map = new google.maps.Map(document.getElementById('map'), mapOptions);
      
    var createblog = false;
    var blog = false;
    var externblog = false;
    var preview = false;
    var edit = false;
    var url = window.location.href.split("/");
        
    if (url.indexOf("ExternBlog") > -1) { blog = true; }
    if (url.indexOf("ViewBlogFromExtern") > -1) { externblog = true; }
    if (url.indexOf("Create") > -1) { createblog = true; }
    if (url.indexOf("preview") > -1) preview = true;
    if (url.indexOf("Edit") > -1) edit = true;



    if (blog && externblog)
    {
        var parturl = window.location.search;
        if (parturl.substring(0, 1) == '?') {
            parturl = parturl.substring(1);
        }
        var resturl = "/api/ExternBlogApi/GetMapMarkersForExtern/?userid=";
        var getParams = new URLSearchParams(parturl);
        for (let p of getParams) {
            if (p[0] === "userid")
            {
                var id = p[1].split(':');
                resturl += id[0];
            }
            if (p[0] === "blogid") {
                resturl += "&blogid=" + p[1];
            }
        }

        getData(resturl, map);

    } else
        if (blog) {
            var id = window.location.pathname.split("/").slice(-1)[0]; //get last part of url
            var url = "/api/ExternBlogApi/GetMapMarkers/?id=" + id;
            getData(url,map);

        } else
            if ((createblog && !blog && !externblog) || (preview && !blog && !externblog)) {
                var url = "/api/BlogApi/GetGMapsMarkers/";
                getData(url, map);
            
            }
        

    if (createblog && !blog && !externblog) {
            google.maps.event.addListener(map, 'click', function (e) {
                postMarker(e.latLng.lat(), e.latLng.lng(),"/api/BlogApi/AddGMapsMarker/");
                placeMarker(e.latLng, map);
            });
    } else if (edit && blog) {
        google.maps.event.addListener(map, 'click', function (e) {
            postMarker(e.latLng.lat(), e.latLng.lng(), "/api/ExternBlogApi/AddMapsMarker/");
            placeMarker(e.latLng, map);
        });
    }

        

       

        function placeMarker(position, map) {
            var marker = new google.maps.Marker({
                position: position,
                map: map
            });
            map.panTo(position);
        }

}

function drawGMapsMarker(map, markerarray) {
   
    var lastposition = null;

    if (markerarray != undefined && markerarray!=null){

    for(var marker of markerarray) {
        lastposition={ lat: marker.Latitude, lng: marker.Longitude }
        var marker = new google.maps.Marker({
            position: lastposition,
            map: map
        });
    }
    if(lastposition!=null)
        map.panTo(lastposition);
    }
}

function postMarker(lat, lng,url) {
    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify({ "Latitude": lat, "Longitude":lng }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function (data) {
            if (data.readyState === 4 & data.status === 200) {
               
            }
        }
    });
}

function getData(url,map) {
    $.ajax({
        type: "Get",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function (data) {
            if (data.readyState === 4 & data.status === 200) {
               
               drawGMapsMarker(map, data.responseJSON);
            }
        }
    });
}