// Add map
var mymap = L.map('mapid').setView([51.505, -0.09], 13);

// Add tiles
L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiZGlnaXRvcG9pIiwiYSI6ImNqYXE1NW8yczBueG4yd3BodW1wcXNzOTkifQ.5gGiW5RYJCqyzTIYfR17zQ', {
    attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
    maxZoom: 18,
    id: 'mapbox.streets',
    accessToken: 'pk.eyJ1IjoiZGlnaXRvcG9pIiwiYSI6ImNqYXE1NW8yczBueG4yd3BodW1wcXNzOTkifQ.5gGiW5RYJCqyzTIYfR17zQ'
}).addTo(mymap);

// Add marker
var marker = L.marker([51.5, -0.09]).addTo(mymap);

// Add a circle
var circle = L.circle([51.508, -0.11], {
    color: 'red',
    fillColor: '#f03',
    fillOpacity: 0.5,
    radius: 500
}).addTo(mymap);

// Add a polygon:
var polygon = L.polygon([
    [51.509, -0.08],
    [51.503, -0.06],
    [51.51, -0.047]
]).addTo(mymap);

//  Add popups
marker.bindPopup("<b>Hello world!</b><br>I am a popup!").openPopup();
circle.bindPopup("I am a circle.");
polygon.bindPopup("I am a polygon.");

//  Standalone popup
var popup = L.popup()
    .setLatLng([51.475, -0.09])
    .setContent("I am a standalone popup.")
    .openOn(mymap);

//  Demonstrating click event
// function onMapClick(e) {
//     alert("You clicked the map at " + e.latlng);
// }

// mymap.on('click', onMapClick);

// Improve previous example by using popup instead of alert
var popup = L.popup();

function onMapClick(e) {
    popup
        .setLatLng(e.latlng)
        .setContent("You clicked the map at " + e.latlng.toString())
        .openOn(mymap);
}

mymap.on('click', onMapClick);