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