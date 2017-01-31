var gmapsMarkerArray = [];

function initMap() {

        var mapOptions = {
            center: {  lat: 1.3523784000, lng: 103.9847063000},
            zoom: 8
        };
        var map = new google.maps.Map(document.getElementById('map'), mapOptions);

        getData(map);

        google.maps.event.addListener(map, 'click', function (e) {
           // gmapsMarkerArray.push({ "Latitude": e.latLng.lat(), "Longitude": e.latLng.lng() });
            postMarker(e.latLng.lat(), e.latLng.lng());
            placeMarker(e.latLng, map);
        });

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

function postMarker(lat, lng) {
    $.ajax({
        type: "POST",
        url: "/api/BlogApi/AddGMapsMarker/",
        data: JSON.stringify({ "Latitude": lat, "Longitude":lng }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function (data) {
            if (data.readyState === 4 & data.status === 200) {
               
            }
        }
    });
}

function getData(map) {
    $.ajax({
        type: "Get",
        url: "/api/BlogApi/GetGMapsMarkers/",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        complete: function (data) {
            if (data.readyState === 4 & data.status === 200) {
               
               drawGMapsMarker(map, data.responseJSON);
            }
        }
    });
}