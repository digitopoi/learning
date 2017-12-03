var mymap = L.map('mapid').setView([51.505, -0.09], 13);

L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiZGlnaXRvcG9pIiwiYSI6ImNqYXE1NW8yczBueG4yd3BodW1wcXNzOTkifQ.5gGiW5RYJCqyzTIYfR17zQ', {
    attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
    maxZoom: 18,
    id: 'mapbox.streets',
    accessToken: 'pk.eyJ1IjoiZGlnaXRvcG9pIiwiYSI6ImNqYXE1NW8yczBueG4yd3BodW1wcXNzOTkifQ.5gGiW5RYJCqyzTIYfR17zQ'
}).addTo(mymap);

console.log(mymap);