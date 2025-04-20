

document.querySelectorAll('input[name="radio_buttons_2"]').forEach(radio => {
    radio.addEventListener('change', function () {
        const content = document.getElementById('recuring-details');
        const template = document.getElementById(`template-recurring-${this.value}`);
        content.innerHTML = template.innerHTML;

        if (this.value == 3) {

            // Initialize Flatpickr on the newly added input
            const flatpickrInput = content.querySelector('#custom-schedule');
            if (flatpickrInput) {
                flatpickrInput.flatpickr({
                    onReady: function () {
                        this.jumpToDate("today")
                    },
                    mode: "multiple",
                    dateFormat: "Y-m-d",
                    minDate: "today" 
                });
            }

        }


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
                    minDate: "today" 
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





$(document).ready(function () {


    $("#trip-time").flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
    });

    $("#reverse-trip-time").flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
    });





});
