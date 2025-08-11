var resultTableId = 'Trips_Patterns_Table';
function initilizeDateTimePickers() {


    $(".flatpicker-time").flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i"
    });

    $(".flatpicker-date").flatpickr({
        minDate: "today",
        maxDate: "",
        jumpToDate: "today"
    });



    //let test = document.getElementById('1');
    //let test2 = document.getElementById('2');

    //console.log(test.value);
    //console.log(test2.value);

    //initilazeMainTimePicker();
    //initilazeReturnTimePicker();

    //initilazeMainDatePicker()
    //initilazeReturnDatePicker();
}


function initilazeMainTimePicker() {
    $(".flatpickr-main-time").flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i"
    });
}

function initilazeReturnTimePicker() {
    $(".flatpickr-return-timr").flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",

    });
}

function initilazeMainDatePicker() {
    $(".flatpickr-main-date").flatpickr({
        minDate: "today",
        maxDate: "",
        jumpToDate: "today"
    });
}

function initilazeReturnDatePicker() {
    $(".flatpickr-return-date").flatpickr({
        minDate: "today",
        maxDate: "",
        jumpToDate: "today"
    });
}



//  Handle Search Options ...
function HandleSearchOptions() {


    $('#search-options-select2').select2({
        minimumResultsForSearch: Infinity // hides search box
    });


    $('#search-options-select2').on('change', function (e) {

        if (e.target.value === 'SpecificMonth') {

            const content = document.getElementById('date-options-box');
            const template = document.getElementById('date-options-box-template-month');
            content.innerHTML = template.innerHTML;

            HandleDialerEvent();

            //// Dialer container element
            //var dialerElement = document.getElementById("year_dialer");

            //// Create dialer object and initialize a new instance
            //var dialerObject = new KTDialer(dialerElement, {

            //    max: 2050,
            //    step: 1,
            //    suffix: " Year",
            //    decimals: 0
            //});

            $('#month-options-select2').select2();

        }
        else {

            const content = document.getElementById('date-options-box');
            const template = document.getElementById('date-options-box-template-date');
            content.innerHTML = template.innerHTML;

            $(".flatpickr-input").flatpickr({
                jumpToDate: "today"
            });

        }

    });
}
function ShowSubOptions() {

    const content = document.getElementById('date-search-box');
    const template = document.getElementById('date-search-box-template');
    content.innerHTML = template.innerHTML;

    HandleSearchOptions();


}
function HandleRadioButtonsSubOptions() {


    document.querySelectorAll('.trip-search-radio').forEach(radio => {

        radio.addEventListener('change', function () {

           

            if (radio.hasAttribute('data-has-sub-options')) {

                resultTableId = 'Scheduled_Trips_Table';

                ShowSubOptions();



            }
            else {

                resultTableId = 'Trips_Patterns_Table';

                const content = document.getElementById('date-search-box');
                content.innerHTML = "";

            }

        })
     


    })


}



function HandleDialerEvent() {



    // Dialer container element
    var dialerElement = document.getElementById("year_dialer");

    // Create dialer object and initialize a new instance
    var dialerObject = new KTDialer(dialerElement, {

        max: 2050,
        step: 1,
        suffix: " Year",
        decimals: 0
    });



    let hiddenInput = document.getElementById("hidden-year");

    let value = document.getElementById('search-year').value.split(" ");

    hiddenInput.value = value[0];

    document.getElementById('search-year').addEventListener("change", function () {

        value = this.value.split(" ");

        hiddenInput.value = value[0];

    })
}


function RenderSearchResult(data) {


    const resultBox = document.getElementById('search-result');

    resultBox.innerHTML = data;

    const scroiingButton = document.getElementById("scrolling-button");
    scroiingButton.click();

    InitilaizeMetronicDatatable(resultTableId);  

    KTMenu.createInstances();
}


HandleRadioButtonsSubOptions();


function HandleAddRouteButton() {

    document.getElementById('route-background').className = 'd-flex border border-dashed border-hover-primary rounded';
    const addButton = document.getElementById('add-route-button');
    addButton.className = 'btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1 ms-20  js-render-modal animate__animated animate__fadeInDown ';
    addButton.innerHTML = `     <i class="ki-duotone ki-pencil fs-2 ">
                                <span class="path1"></span>
                                <span class="path2"></span>
                            </i>`
}

document.body.addEventListener('click', async function (event) {

    if (event.target && event.target.closest('.js-select-route')) {

        event.preventDefault()

        const btn = event.target.closest('.js-select-route');
        const routeId = btn.getAttribute('data-route-id')
        const test = document.getElementById('route-id');
        test.value = routeId;
       
        try {

            const response = await fetch(btn.getAttribute('data-url'));


            if (!response.ok)
                throw new Error('Failed to load partial view');

            const html = await response.text();

            HandleAddRouteButton();
            document.getElementById("route-details").innerHTML = html;





        } catch {


        }

        HideModal();


    }
})


function HandleTripEditingChild() {

    initilizeDateTimePickers();

    // initilize dialers
    KTDialer.createInstances();

    HandleLongBreakEvents();
}

async function FillStationsSelectList(checkbox) {

    

    let routeId = document.getElementById(`${checkbox.id}-route-id`);

    $(`#${checkbox.id}-select-station`).select2();


    if (routeId === null)
        return;

    let url = checkbox.getAttribute("data-url") + routeId.value;

    const response = await fetch(url);


    if (!response.ok)
        throw new Error('Failed to load partial view');

    const routeStationsJson = await response.json();
    const routeStationsDropdown = document.getElementById(`${checkbox.id}-select-station`);

    routeStationsJson.forEach(s => {

        if (s.stationOrder !== routeStationsJson.length) {
            // Create a new option element
            let newOption = document.createElement("option");
            newOption.value = s.stationOrder + 1;
            newOption.text = s.text;

            // Append the new option to the Dropdown List

            routeStationsDropdown.appendChild(newOption);
        }


    })

}
function HandleLongBreakEvents() {

    const checkboxes = document.querySelectorAll('.long-break-checkbox');

    checkboxes.forEach(checkbox => {

        checkbox.addEventListener('change', async function (event) {

            let container = document.getElementById(`${this.id}-trip-break-container`);
            let template = document.getElementById(`${this.id}-trip-break-template`);

            if (event.target.checked) {
          

                container.innerHTML = template.innerHTML;

                // initilize dialers
                KTDialer.createInstances();



                FillStationsSelectList(event.target);





            } else {

                container.innerHTML = "";
            }
      



        })
    })
    //document.addEventListener('change', async function (e) {

    //    if (e.target.matches('.long-break-checkbox')) {
    //        let brakSettingsContainer = document.getElementById("break_details");


    //        if (e.target.checked) {

    //            const template = document.getElementById("long_break_template");

    //            brakSettingsContainer.innerHTML = template.innerHTML;

    //            let routeId = document.getElementById("Route_Id").value;

    //            let url = e.target.getAttribute("data-url") + routeId;

    //            const response = await fetch(url);


    //            if (!response.ok)
    //                throw new Error('Failed to load partial view');

    //            const routeStationsJson = await response.json();
    //            //const routeStations = JSON.parse(routeStationsJson)
    //            const routeStationsDropdown = document.getElementById("Select_Station");

    //            routeStationsJson.forEach(s => {

    //                if (s.stationOrder !== routeStationsJson.length) {
    //                    // Create a new option element
    //                    let newOption = document.createElement("option");
    //                    newOption.value = s.stationOrder + 1;
    //                    newOption.text = s.text;

    //                    // Append the new option to the Dropdown List

    //                    routeStationsDropdown.appendChild(newOption);
    //                }


    //            })

    //            $('#Select_Station').select2();

    //            //break_dialer

    //            // Dialer container element
    //            var dialerElement = document.getElementById("break_dialer");

    //            // Create dialer object and initialize a new instance
    //            var dialerObject = new KTDialer(dialerElement, {
    //                min: 5,
    //                max: 40,
    //                step: 5,
    //                suffix: " Minutes",
    //                decimals: 0
    //            });


    //            let hiddenInput = document.getElementById("Hidden_Break_Minutes");

    //            let value = document.getElementById('Break_Minutes').value.split(" ");

    //            hiddenInput.value = value[0];

    //            document.getElementById('Break_Minutes').addEventListener("change", function () {



    //                //let hiddenInput = document.getElementById("Hidden_Break_Minutes");

    //                value = this.value.split(" ");

    //                hiddenInput.value = value[0];

    //            })

    //        }
    //        else {
    //            brakSettingsContainer.innerHTML = "";
    //        }


    //    }
    //});





}
function HandleSelectVehicleEvent() {

    document.body.addEventListener('click', async function (event) {

        if (event.target && event.target.closest('.js-select-vehicle')) {

            event.preventDefault()

            const button = event.target.closest('.js-select-vehicle');

            //const routeId = btn.getAttribute('data-route-id')

            //const test = document.getElementById('route-id');
            //test.value = routeId;

            try {

                const jsonData = button.getAttribute('data-json-data');

                const response = await fetch(button.getAttribute('data-url'), {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json' // IMPORTANT!
                    },
                    body: jsonData
                })



                if (!response.ok)
                    throw new Error('Failed to load partial view');

                let container = document.getElementById("vehicle-details")

                container.innerHTML = await response.text();





            } catch {


            }

            HideModal();


        }
    })

}

HandleSelectVehicleEvent();