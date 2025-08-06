
var stationAId;
var stationBId;

const travelContainer = document.getElementById("suitable-travels-container");

function findSuitableTravels(results ) {


    travelContainer.innerHTML = "";
    travelContainer.innerHTML = results;



}



const stations = JSON.parse(document.getElementById("JsonStations").value);

function FillStationBDropdownList(selectedStationId) {

    let stationBOptions = stations.filter(s => s.StationId != selectedStationId)

    let stationBDropdownList = document.getElementById("js-stationB");

    stationBDropdownList.innerHTML = "";

    // Append the new Empty option to the Dropdown List

    let newOption = document.createElement("option");
    newOption.text = "";
    stationBDropdownList.appendChild(newOption);

    stationBOptions.forEach(s => {

        // Create a new option element
        let newOption = document.createElement("option");
        newOption.value = s.StationId;
        newOption.text = s.StationName;

        // Append the new option to the Dropdown List

        stationBDropdownList.appendChild(newOption);

    })
}


$(document).ready(function () {

    $('#js-stationA').on('select2:select', function () {

        FillStationBDropdownList(this.value)
    });



    $('.flatpickr-input').flatpickr({
     
        dateFormat: "Y-m-d",
        minDate: "today",
        defaultDate: "today"
    })

});

document.addEventListener('DOMContentLoaded', function () {
document.body.addEventListener('click',async function (e) {

    const button = e.target.closest(".js-details-child");

    if (button) {

      

        let childId = button.getAttribute("data-child-id");
        let child = document.getElementById(childId);

        //const allTrips = Array.from(document.querySelectorAll('.js-details-child')).filter(t => t.getAttribute('data-child-id') != childId);
        const allTrips = document.querySelectorAll(`.js-details-child:not([data-child-id="${childId}"])`);
        allTrips.forEach(t => {
            let containerId = t.getAttribute('data-child-id');
            document.getElementById(containerId).innerHTML = "";
        })

   

        if (child.innerHTML == "") {

            try {

                const url = button.getAttribute('data-url');
                const response = await fetch(url);

                if (!response.ok)
                    throw new Error('Failed to load partial view');


                const html = await response.text();



                child.innerHTML = html;


                document.querySelectorAll('.seat.Empty').forEach(seat => {
                    seat.addEventListener('click', function () {
                        this.classList.toggle('selected');
                        document.querySelector('.error-block').innerHTML = "";
                    });
                });

            }
            catch
            {

            }

        }
        else
            child.innerHTML = "";


    }

})
 
});


function validateSelectedSeats(form) {

    const checkboxes = form.querySelectorAll('input[type="checkbox"][name*="BookedSeats"][name$=".IsSelected"]');

    const selectedSeats = Array.from(checkboxes).filter(cb => cb.checked);

    if (selectedSeats.length === 0) {


        document.querySelector('.error-block').innerHTML="* Select one seat at least to book."

        return false; 
    }

    document.querySelector('.error-block').innerHTML = ""

    return true; 

}