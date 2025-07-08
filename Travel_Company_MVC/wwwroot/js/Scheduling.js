
var tripsTimings;
// Handling Templates ...
document.querySelectorAll('.recurring-radio').forEach(radio => {
    radio.addEventListener('change', function () {
        const content = document.getElementById('recuring-details');
        const template = document.getElementById(`template-recurring-${this.value}`);
        const durationSettings = document.getElementById('duration-settings');

        content.innerHTML = template.innerHTML;

        if (this.classList.contains("custom-recurring")) {

            // Initialize Flatpickr on the newly added input
            const flatpickrInput = content.querySelector('#custom-schedule');
            if (flatpickrInput) {
                flatpickrInput.flatpickr({
                    onReady: function () {
                        this.jumpToDate("today")
                    },
                    mode: "multiple",
                    dateFormat: "Y-m-d",
                    minDate: "today",
                    onChange: function (selectedDates) {
                        const container = document.querySelector('#custom-dates-container');
                        container.innerHTML = '';

                        selectedDates.forEach(date => {
                            const input = document.createElement('input');
                            input.type = 'hidden';
                            input.name = 'CustomDates';
                            input.value = date.toLocaleDateString('en-CA'); // "YYYY-MM-DD"

                            // Enable validation attributes
                            input.setAttribute("data-val", "true");
                            input.setAttribute("data-val-required", "Please select at least one date.");

                            container.appendChild(input);
                        });

                    }

                });
            }   

            durationSettings.style.setProperty("display", "none", "important");
            //durationSettings.style.display = "none"

        }
        else {
   
            durationSettings.style.display = "block";
        
        }

        $.validator.unobtrusive.parse(content); 
    });
});

document.querySelectorAll('input[name="radio_trip_duration"]').forEach(radio => {
    radio.addEventListener('change', function () {
        const content = document.getElementById('duration-options');
        const template = document.getElementById(`template-duration-${this.value}`);
        content.innerHTML = template.innerHTML;

        if (this.value == 1) {

            // Initialize Flatpickr on the newly added input
            const flatpickrInput = content.querySelector('#specific-period');
            if (flatpickrInput) {
                flatpickrInput.flatpickr({
                    altInput: true,
                    altFormat: "F j, Y",
                    dateFormat: "Y-m-d",
                    mode: "range",
                    minDate: "today",

                    onChange: function (selectedDates, dateStr, instance) {
                        if (selectedDates.length === 2) {
                            const startDate = selectedDates[0].toLocaleDateString('en-CA'); // "YYYY-MM-DD"
                            const endDate = selectedDates[1].toLocaleDateString('en-CA');

                            // Set hidden fields or log them
                            document.querySelector('[name="StartDate"]').value = startDate;
                            document.querySelector('[name="EndDate"]').value = endDate;
                        } else {
                            document.querySelector('[name="StartDate"]').value = "";
                            document.querySelector('[name="EndDate"]').value = "";
                        }
                    }
                });
            }


        }

        if (this.value == 2) {

            // fixed-duration

            // Initialize Flatpickr on the newly added input
            const flatpickrInput = content.querySelector('#fixed-duration');
            if (flatpickrInput) {
                flatpickrInput.flatpickr({
                    minDate: "today",
                    jumpToDate:"today"
                });
            }


            const options = document.querySelectorAll('[data-kt-docs-advanced-forms="interactive"]');
            const inputEl = document.querySelector('[name="interactive_amount"]');
            options.forEach(option => {
                option.addEventListener('click', e => {
                    e.preventDefault();

                    inputEl.value = e.target.innerText;
                });
            });



        }



    });
});



// Handling Selecting Route ...

async function  getRouteTripRecurringPatterns(url) {

    try {

        const response = await fetch(url);


        if (!response.ok) {


            throw new Error('Failed to load partial view');

        }

        const html = await response.text();


        document.getElementById("trips-patterns").innerHTML = html;

        let timings = document.getElementById("patterns_timings");


        tripsTimings = undefined;

        if (timings)
            tripsTimings = JSON.parse(timings.value); 

        console.log(tripsTimings);


        InitilaizeMetronicDatatable('Trips_Patterns_Table');  

        KTMenu.createInstances();

    } catch {


    }

}


function enableFilds() {

    const card = document.getElementById('usage');
    const inputs = card.querySelectorAll('input, select, textarea, button');

    inputs.forEach(input => {
        input.disabled = false;
    });

    card.classList.remove('opacity-50');
}

//document.getElementById('trip-time').addEventListener('change', function (event) {

//    //tripsTimings
//    let box = document.getElementById('tripsTimings');

//    if (tripsTimings && tripsTimings.includes(this.value)) {


//        box.innerHTML = `<h1>Done</h1>`

//    }
//    else
//        box.innerHTML = "";


//})
document.body.addEventListener('click', async function (event) {

    if (event.target && event.target.closest('.js-select-route')) {

        event.preventDefault()
        const btn = event.target.closest('.js-select-route');
        const routeId = btn.getAttribute('data-route-id')
        const test = document.getElementById('route-id');
        test.value = routeId;
        enableFilds();
      

        try {

            const response = await fetch(btn.getAttribute('data-url'));


            if (!response.ok)
                throw new Error('Failed to load partial view');

            const html = await response.text();

            HandleAddRouteButton();
            document.getElementById("route-details").innerHTML = html;


            const url = btn.getAttribute('data-trips-patterns-url');
            getRouteTripRecurringPatterns(url);

            //$('#kt_datatable_dom_positioning').DataTable().destroy();
            //let test = document.getElementById('testttt');
            //console.log(test);

            //InitilaizeMetronicDatatable(test); 





        } catch {


        }

        HideModal();


    }
})

function HandleAddRouteButton() {

    document.getElementById('route-background').className = 'd-flex border border-dashed rounded';
    const addButton = document.getElementById('add-route-button');
    addButton.className = 'btn btn-icon btn-bg-light btn-active-color-primary btn-sm me-1 ms-20  js-render-modal animate__animated animate__fadeInDown ';
    addButton.innerHTML = `     <i class="ki-duotone ki-pencil fs-2 ">
                                <span class="path1"></span>
                                <span class="path2"></span>
                            </i>`
}

//InitilaizeMetronicDatatable();

$(document).ready(function () {


    $("#trip-time").flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
        onChange: function (selectedDates, dateStr, instance) {
        
            //tripsTimings
            let box = document.getElementById('pattern_exists');
            let editBox = document.getElementById('editing_checkbox');
            let fullTime = dateStr + ":00";
            let editBoxTemplate = document.getElementById("editing_checkbox_template");

            console.log("Time selected:", fullTime);

            if (tripsTimings && tripsTimings.includes(fullTime)) {

              
                box.innerHTML = `<span class="bullet bullet-dot bg-danger mb-1"></span> <span class="text-gray-900">Selected timing has already existed pattern <a href="#" class="text-primary">Check pattern.</a></span>
`
                editBox.innerHTML = editBoxTemplate.innerHTML;

            }
            else {

                //editStatus.value = 'true';
                box.innerHTML = "";
                editBox.innerHTML = "";
            }
     



            // You can also access selectedDates[0] as a Date object
        }
    });

    $("#reverse-trip-time").flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
    });





});






document.addEventListener('change', function (e) {
    if (e.target.matches('.checkbox-radio')) {

        let editStatus = document.getElementById("edit_status");
        editStatus.value = e.target.value;

        console.log(editStatus.value)

        const name = e.target.name;
        const checkboxes = document.querySelectorAll(`.checkbox-radio[name="${name}"]`);

        if (e.target.checked) {
            // Uncheck all others
            checkboxes.forEach(cb => {
                if (cb !== e.target) cb.checked = false;
            });
        } else {
            // Prevent unchecking the only selected checkbox
            const checkedCount = Array.from(checkboxes).filter(cb => cb.checked).length;
            if (checkedCount === 0) {
                e.target.checked = true; // re-check it
            }
        }
    }
});