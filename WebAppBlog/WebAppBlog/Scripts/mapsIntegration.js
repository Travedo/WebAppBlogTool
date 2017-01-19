function initMap() {

        var mapOptions = {
            center: {  lat: 1.3523784000, lng: 103.9847063000},
            zoom: 8
        };
        var map = new google.maps.Map(document.getElementById('map'), mapOptions);

        google.maps.event.addListener(map, 'click', function (e) {
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