function initMap() {
    var contentString = '<b>Delmo Chicken & Agro (pvt) Ltd</b>,<br /> ' +
                            '<address> No. 21, Pahurumankadawatta,<br /> ' +
                            'Waradala,' +
                            'Mellawagedara,<br /> ' +
                            'Sri Lanka.<br /> ' +
                            '<abbr title="Phone">P:</abbr>+94332240800</address>';

    var infowindow = new google.maps.InfoWindow({
        content: contentString
    });


    var myLatLng = { lat: 7.29679, lng: 80.039240 };
    var mapDiv = document.getElementById('mapapi');
    var map = new google.maps.Map(mapDiv, {
        center: myLatLng,
        zoom: 16
    });
    var marker = new google.maps.Marker({
        position: myLatLng,
        map: map,
        title: 'Delmo Chicken'
    });
    marker.addListener('click', function () {
        infowindow.open(map, marker);
    });
}
window.onload = function () {
    initMap();
};