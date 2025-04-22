

function handleTableChiled() {
    let table = $('#kt_datatable_dom_positioning').DataTable();

    document.querySelector('#kt_datatable_dom_positioning tbody').addEventListener('click', async function (e) {

    

        if (e.target.closest(".js-table-chiled")) {

            let tr = e.target.closest('tr');
            if (!tr) return;

            //let detailsJson = tr.getAttribute("data-details");

            //let detailsObject = JSON.parse(detailsJson);

            let row = table.row(tr);

            if (row.child.isShown()) {

                // Animate hiding
                $('.child-slide', row.child()).slideUp(50, function () {
                    row.child.hide(); // Hide the DataTable row after animation completes
                });
            } else {
                
                row.child('<div class="child-slide" style="display:none;">Hi There</div>').show();

                        $('.child-slide', row.child()).slideDown(100); // Animate it

            }
        }
            //let url = tr.getAttribute("data-url");
            //stationAId = document.getElementById("js-stationA").value;
            //stationBId = document.getElementById("js-stationB").value;
            //console.log(stationAId);
            //console.log(stationBId);

            //detailsObject.stationAId = stationAId;
            //detailsObject.stationBId = stationBId;

            //detailsJson = JSON.stringify(detailsObject);

            //try {
            //    //   let url = "/Booking/GetAvaliableSeats";

            //    let response = await fetch(url, {
            //        method: 'Post',
            //        headers: {
            //            'Content-Type': 'application/json', // Make sure to set the correct content type
            //        },
            //        body: detailsJson
            //    });

            //    if (response.ok) {

            //        let data = await response.text();


            //        if (row.child.isShown()) {

            //            row.child.hide();
            //        } else {
            //            console.log(data)
            //            row.child(data).show();
            //        }

            //    }






            //}
            //catch {
            //    console.log("Cancelled.");

            //}



        




    });

}


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
handleTableChiled();