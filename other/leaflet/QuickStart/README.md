# Leaflet Quickstart Guide

## Preparing your page

1. Include Leaflett CSS in the head section of document:

 ```html
 <link rel="stylesheet" href="https://unpkg.com/leaflet@1.2.0/dist/leaflet.css"
   integrity="sha512-M2wvCLH6DSRazYeZRIm1JnYyh22purTM+FDB5CsyxtQJYeKq83arPe5wgbNmcFXGqiSH2XR8dT/fJISVA1r/zQ=="
   crossorigin=""/>
```

2. Include Leaflet JS file **after** Leaflet's CSS
```html
 <!-- Make sure you put this AFTER Leaflet's CSS -->
 <script src="https://unpkg.com/leaflet@1.2.0/dist/leaflet.js"
   integrity="sha512-lInM/apFSqyy1o6s89K4iQUKg6ppXEgsVxT35HbzUupEVRh2Eu9Wdl4tHj7dZO0s1uvplcYGmt3498TtHq+log=="
   crossorigin=""></script>
```

3. Put a div element with an id where your map will be

```html
<div id="mapid"></div>
```

4. Make sure the map element has a defined height
```css
#mapid { height: 180px; }
```

## Setting up the map

1. Initialize the map and set its view to chosen geographical coordinates and zoom level

```js
var mymap = L.map('mapid').setView([51.505, -0.09], 13);
```

By default, all mouse and touch interactions on the map are enabled and it has zoom and attribution controls

2. Add a tile layer to the map. 
  - in this example - Mapbox Streets
  - Creating a tile layer usually involves setting the URL template for the tile images, the attribution text, and the max zoom

```js
L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
    attribution: 'Map data &copy; <a href="http://openstreetmap.org">OpenStreetMap</a> contributors, <a href="http://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="http://mapbox.com">Mapbox</a>',
    maxZoom: 18,
    id: 'mapbox.streets',
    accessToken: 'your.mapbox.access.token'
}).addTo(mymap);
```

Make sure that all code is called after the div and leaflet.js inclusion.

Leaflet is provider agnostic (you can use different map templates - though, Mapbox and Leaflet work well!)

## Markers, Circles, and Polygons

Besides tile layers, you can easily add other things to your map, including markers, polylines, polygons, circles, and popups.

Adding a marker:
```js
var marker = L.marker([51.5, -0.09]).addTo(myMap);
```

Adding a circle:
Same, except you specify the radius in meters as a second argument - and lets you control how it looks by passing arguments as the last argument

```js
var circle = L.circle([51.508, -0.11], {
    color: 'red',
    fillColor: '#f03',
    fillOpacity: 0.5,
    radius: 500
}).addTo(mymap);
```

## Working with popups

Popups are used when you want to add some information to a particular object on a map. 

```js
marker.bindPopup("<b>Hello world!</b><br>I am a popup!").openPopup();
circle.bindPopup("I am a circle.");
polygon.bindPopup("I am a polygon.");
```

The bindPopup() method attaches a popup with the specified HTML content to your marker so the popup appears when you click on the object.

The openPopup() method (for markers only) immediately opens the attached popup.

You can also use popups as layers (when you need something more than attaching a popup to an object):

```js
var popup = L.popup()
    .setLatLng([51.5, -0.09])
    .setContent("I am a standalone popup.")
    .openOn(mymap);
```

Here we use openOn() instead of addTo() because it handles automatic closing of a previously opened popup when opening a new one, which is good for usability.

## Dealing with events

Everytime something happens in Leaflet (eg. user clicks on a marker or map zoom changes) the corresponding objects sends an event which you can subscribe to with a function. It allows you to react to user interaction:

```js
function onMapClick(e) {
    alert("You clicked the map at " + e.latlng);
}

mymap.on('click', onMapClick);
```

Each object has its own set of events ([see documentation](http://leafletjs.com/reference.html))

The first argument of the listener function is an event object - it contains useful information about the event that happened.
  - ex. map click event object
  - has a latlng property - location at which the event occured.

Improving previous example by using a popup instead of an alert:

```js
var popup = L.popup();

function onMapClick(e) {
    popup
        .setLatLng(e.latlng)
        .setContent("You clicked the map at " + e.latlng.toString())
        .openOn(mymap);
}

mymap.on('click', onMapClick);
```