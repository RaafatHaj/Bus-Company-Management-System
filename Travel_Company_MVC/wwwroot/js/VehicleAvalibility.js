

function test() {



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
        maxDate:"",
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