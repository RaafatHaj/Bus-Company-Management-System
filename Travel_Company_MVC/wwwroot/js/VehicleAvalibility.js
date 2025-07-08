

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