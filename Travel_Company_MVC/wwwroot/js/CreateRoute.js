function CreateMap() {
    // Creating map options
    var mapOptions = {
        center: [34.8021, 38.9968], // Initial center
        zoom: 7, // Initial zoom level
     //   minZoom: 6.1, // Minimum zoom level
       // maxZoom: 8, // Maximum zoom level
        scrollWheelZoom: false, // Disable default scroll zoom
        attributionControl: false,
        zoomControl: false,
        boxZoom:false,
    };
    // Initialize the map
    var map = L.map("map", mapOptions);

    //  map.zoomControl.remove();
    // map.touchZoom.disable();
    // map.doubleClickZoom.disable();
    // map.scrollWheelZoom.disable();
    // map.boxZoom.disable(); // Disable zooming via box selection


    // Add a tile layer
    var layer = L.tileLayer(
        "https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png"
    ).addTo(map);
    map.addLayer(layer);

    // Define Syria's geographic bounds
    var syriaBounds = L.latLngBounds(
        L.latLng(32.0, 35.5), // Southwest corner
        L.latLng(37.5, 42.0) // Northeast corner
    );
    //   map.fitBounds(syriaBounds);
    // Set the max bounds to Syria
    map.setMaxBounds(syriaBounds);


    // const test = await fetch("\Home\GetStatiokns")

    // if (test.ok) {
    //     log(test.json()
    // }



    return map;
}

function CreateIcon(imageUrl, size) {
    var iconOptions = {
        iconUrl: imageUrl,
        iconSize: size,
    };

    return L.icon(iconOptions);
}

var map = CreateMap();

mainIcon = CreateIcon("/images/map/dot-inside-a-circle-blue.png", [25, 25]);
hoverIcon = CreateIcon("/images/map/dot-inside-a-circle-yellow.png", [25, 25]);
clickedIcon = CreateIcon("/images/map/dot-inside-a-circle.png", [25, 25]);

let selectedCities = [];
// Draw route lines
let polyline = null;
function drawRoute() {
    // Remove the existing route
    if (polyline) {
        map.removeLayer(polyline);
    }

    // Create a new route with selected cities
    if (selectedCities.length > 1) {
        const latlngs = selectedCities.map((city) => [city.latitude, city.longitude]);
        polyline = L.polyline(latlngs, { color: "red" }).addTo(map);
    }
}
var zoomMessage = document.getElementById("zoomMessage");
// Add event listener for scroll events
map.getContainer().addEventListener("wheel", (event) => {
    if (!event.ctrlKey) {

        zoomMessage.style.display = "block"; // Show message
        setTimeout(() => {
            zoomMessage.style.display = "none"; // Hide message after 1.5s
        }, 1500);
        event.preventDefault(); // Prevent accidental scrolling

    } else {
        // Allow zoom if Ctrl is pressed
        map.scrollWheelZoom.enable();
        setTimeout(() => {
            map.scrollWheelZoom.disable();
        }, 50); // Re-disable zoom after a short delay
        event.preventDefault(); // Prevent default scroll to enable zoom
    }
});

function ClikckPoint(pin, city) {


    const index = selectedCities.findIndex((c) => c.stationName === city.stationName);

    if (index === -1) {
        pin.setIcon(clickedIcon);

        selectedCities.push(city);
        pin.isSelected = true;
    } else {
        if (index === selectedCities.length - 1) {
            pin.setIcon(mainIcon);
            selectedCities.splice(index, 1);
            pin.isSelected = false;
        }
    }
    pin.closePopup();
    drawRoute();
}

let pins = [];
let cities;
async function AddEventsToPoints() {

    let response = await fetch("/Stations/FetchAllStations");

    if (response.ok) {

        cities = await response.json();

        cities.forEach(function (city) {
            var pin = L.marker([city.latitude, city.longitude], { icon: mainIcon });
            pins.push(pin);

            pin.addTo(map);

            pin.on("mouseover", function () {
                pin.bindPopup(city.stationName).openPopup();
                if (!pin.isSelected) {
                    pin.setIcon(hoverIcon);
                }
            });

            pin.on("mouseout", function () {
                if (!pin.isSelected) {
                    pin.setIcon(mainIcon);
                }
                pin.closePopup();
            });

            pin.on("click", () => ClikckPoint(pin, city));
        });


    }


}

AddEventsToPoints();