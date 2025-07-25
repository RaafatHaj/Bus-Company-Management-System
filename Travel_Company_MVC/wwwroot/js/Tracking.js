
InitilaizeMetronicDatatable('Tracking_Table');

// Note : in select2 Library we can not use Vanilla js to cahch chsnge on select input but JQuery
//         becose it has custom handling , where select2 hide the oraganal select input and replace it with
//          custom UI

$('#Select_Station').on('change', function () {
    let url = '/Trips/TrackStationTrips?StationId=' + this.value;
    $('#Track_Button').attr('data-url', url);
});
//document.getElementById("Select_Station").addEventListener("change", function () {

//    let url = '/Trips/TrackStationTrips?StationId=' + this.value;

//    console.log(url);

//    document.getElementById("Track_Button").setAttribute("data-url",
//        url);


//});

function liveTable() {

    setInterval(() => {
        let tableBody = $('#Tracking_Table tbody');
        let firstRow = tableBody.find('tr:first');

        // Animate row upwards and move to end
        firstRow.fadeOut(0, function () {
            $(this).appendTo(tableBody).fadeIn(0);
        });

    }, 4000);

}