
//function handleTableChiled() {

//   // let table = $('#kt_datatable_dom_positioning').DataTable();

//    document.querySelector('#kt_datatable_dom_positioning tbody').addEventListener('click', async function (e) {

    

//        if (e.target.closest(".js-table-chiled")) {

//            let tr = e.target.closest('tr');
//            if (!tr) return;

//            //let detailsJson = tr.getAttribute("data-details");

//            //let detailsObject = JSON.parse(detailsJson);

//            let row = table.row(tr);

//            if (row.child.isShown()) {
//                row.child.hide();
//                // Animate hiding
//                $('.child-slide', row.child()).slideUp(50, function () {
//                    row.child.hide(); // Hide the DataTable row after animation completes
//                });
//            } else {

//                try {


//                    let test = tr.getAttribute('data-url');
//                    const response = await fetch(tr.getAttribute('data-url'));



//                    if (!response.ok)
//                        throw new Error('Failed to load partial view');

//                    const html = await response.text();

                 

//                    row.child(html).show();
         
//                    //$('.child-slide', row.child()).slideDown(600); // Animate it
//                    initilazeTimePicker();
//                    // Reinitialize validation
//                    $.validator.unobtrusive.parse(tr.nextElementSibling);


//                } catch {


//                }





   

//            }
//        }
      


        




//    });

//}

function initilazeTimePicker() {
    $(".flatpickr-input").flatpickr({
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
    });
}
//$(document).ready(function () {
   

//    $("#trip-time").flatpickr({
//        enableTime: true,
//        noCalendar: true,
//        dateFormat: "H:i",
//    });



//    $("#reverse-trip-time").flatpickr({
//        enableTime: true,
//        noCalendar: true,
//        dateFormat: "H:i",
//    });





//});
/*handleTableChiled();*/