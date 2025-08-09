
function test() {

    let test = document.getElementById('1');
    let test2 = document.getElementById('2');

    console.log(test.value);
    console.log(test2.value);

    initilazeMainTimePicker();
    initilazeReturnTimePicker();

    initilazeMainDatePicker()
    initilazeReturnDatePicker();
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

        if (e.target.value === 'month') {

            const content = document.getElementById('date-options-box');
            const template = document.getElementById('date-options-box-template-month');
            content.innerHTML = template.innerHTML;

            // Dialer container element
            var dialerElement = document.getElementById("year_dialer");

            // Create dialer object and initialize a new instance
            var dialerObject = new KTDialer(dialerElement, {

                max: 2050,
                step: 1,
                suffix: " Year",
                decimals: 0
            });

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

                ShowSubOptions();



            }
            else {
                const content = document.getElementById('date-search-box');
                content.innerHTML = "";

            }

        })
     


    })


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


