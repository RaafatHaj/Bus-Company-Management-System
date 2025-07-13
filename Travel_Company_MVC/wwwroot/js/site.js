
var updatedRow;
var html;
/*const table = InitilaizeMetronicDatatable();*/

function addRowToTable(newRow, form) {

 
    if (updatedRow !== undefined) {

        table.row(updatedRow).remove().draw();

        updatedRow = undefined;
    }

    // since add function in datatable accept just array or jquery element we have to converet
    // Html String to jQuery element just like that


    let jqueryElement = $(newRow)
    const row = table.row.add(jqueryElement).draw().node();


  


    $(row).addClass("animate__animated animate__fadeInDown ").one('animationend', function () {
        $(this).removeClass("animate__animated animate__fadeInDown ");
    });


 //   SuccessMessage();
    HideModal();
}

function testtt(tableId) {
    table = InitilaizeMetronicDatatable(tableId);
}
function InitilaizeDatatable(tableId = '#kt_datatable_dom_positioning') {


    return new DataTable(tableId);
}
function InitilaizeMetronicDatatable(tableId ='Data_Table') {
    $(document).ready(function () {

        // Store DataTable instance in a variable
        const table = $('#' + tableId).DataTable({
            language: {
                lengthMenu: "Show _MENU_",
            },
            dom:
                "<'row mb-2 mt-2 me-2'" +
                "<'col-sm-6 d-flex align-items-center justify-conten-start dt-toolbar xxx'l>" +
                "<'col-sm-6 d-flex align-items-center justify-content-end dt-toolbar 'f>" +
                ">" +

                "<'table-responsive'tr>" +

                "<'row mt-5'" +
                "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
                "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
                ">"
        });

        //// Create the dropdown
        //const $dropdown = $(`
        //    <select id="statusFilter" class="form-select form-select-sm ms-19" style="width: auto;">
        //        <option value="">All Statuses</option>
        //        <option value="Pending">Pending</option>
        //        <option value="Unassigned">Unassigned</option>
        //    </select>
        //`);


        //// Append dropdown inside the custom search container

        //$('.xxx').append($dropdown2);

        //// The index of your status column (0-based)
        //const statusColumnIndex = 1; // adjust if needed

        //// Bind change event on dropdown, using the table variable
        //$('#statusFilter').on('change', function () {
        //    const value = $(this).val();
        //    table.column(statusColumnIndex).search(value).draw();
        //});
    });
}
function ErrorMessage(errorMessage = "Something went wrong!", errorTitle ="Error Message") {

    Swal.fire({
        icon: "error",
        title: errorTitle,
        text: errorMessage
    });
}
function SuccessMessage(message ='Done successfully ..') {

    Swal.fire({
        icon: "success",
        title: "Successed",
        text: message
    });
}
function ConfirmationMessage(title = "Confirming", message = "Are you sure would you like to confirm ?") {

    return Swal.fire({
        title: title,
        text: message,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
    }).then((result) =>  result.isConfirmed);

   
}
function HideModal() {

    const myModal = document.getElementById('Modal');
    const ModalInstance = bootstrap.Modal.getInstance(myModal);

    if (ModalInstance) {
        ModalInstance.hide();
    }
}
function Toggle() {


    document.body.addEventListener('click', async function (event) {

        if (event.target && event.target.matches('.js-toggle')) {

            event.preventDefault()
            const button = event.target;

            const message = event.target.getAttribute('data-message');
            const title = event.target.getAttribute('data-title');


            ConfirmationMessage(title, message).then(async (confirmed) => {

                if (confirmed) {

                    try {

                        const response = await fetch(button.getAttribute('data-url'), {

                            method: 'POST'
                        });

                        if (!response.ok)
                            throw new Error('Failed to load partial view');

                        if (button.hasAttribute("data-update-ui")) {

                            // Here will be the logic of updation the UI ...
                        }

                        SuccessMessage();

                    } catch {

                        ErrorMessage();
                    }

                }
            })

       


        }
    })

}
function SumbitForm() {

    document.body.addEventListener('submit',  function (event) {

        if (event.target && event.target.matches(".js-confirm-form")) {
            event.preventDefault();

            ConfirmationMessage().then( (confirmed) => {

                if (confirmed) 
                    event.target.submit(); 

               

            }).catch(() => {

                ErrorMessage();
            });

        }


    })

}
function SubmitAjaxForm() {
    document.body.addEventListener('submit', async function (event) {

    if(event.target && event.target.matches(".js-ajax-form"))
    {
        event.preventDefault();

        ConfirmationMessage().then(async (confirmed) => {

            if (confirmed) {
                try {

                    const url = event.target.getAttribute('data-url');
                    const formData = new FormData(event.target);

               
           
                    const response = await fetch(url, {
                        method: 'POST',
                        body: formData
                    })

                    if (response.ok) {

                        let responseData = await response.text();

                        if (event.target.hasAttribute('data-updete'))
                            updatedRow = event.target.closest('tr');

                        const callbackFunctionName = event.target.getAttribute('data-callback');

                        if (callbackFunctionName && typeof window[callbackFunctionName] === 'function')
                            window[callbackFunctionName](responseData, event.target);



                        if (event.target.hasAttribute('success-message')) {

                            SuccessMessage(event.target.getAttribute('success-message'));
                        }
                      


                        HideModal();

                    }
                    else {
                        const errorData = await response.json(); // ← this is the key fix
                        ErrorMessage(errorData.errorMessage, errorData.errorTitle);

                    }
  
                }
                catch {
                    ErrorMessage();

                }

            } 
        });

    }


    })


}
function RenderModal() {

  //  const modal = document.getElementById("Modal");
    const modal =new bootstrap.Modal("#Modal");

    document.body.addEventListener('click', async function (event) {
        
        if (event.target && event.target.closest('.js-render-modal')) {

            event.preventDefault()
            const button = event.target.closest('.js-render-modal');
           // const id = button.getAttribute('data-travel-id');
            // modal-xl
            try {

                const response = await fetch(button.getAttribute('data-url'));

                //if (button.hasAttribute('data-updete'))
                //    updatedRow = event.target.closest('tr');

                //modal-dialog modal-lg
                if (button.hasAttribute("data-large-modal")) {
                    document.getElementById("ModalDialog").classList.add("modal-lg")
                    document.getElementById("ModalDialog").classList.add("modal-xl")

                }
                else if (button.hasAttribute("data-middle-modal")) {
                    document.getElementById("ModalDialog").classList.add("modal-lg")
                    document.getElementById("ModalDialog").classList.remove("modal-xl")

                }
                else {
                    document.getElementById("ModalDialog").classList.remove("modal-xl")
                    document.getElementById("ModalDialog").classList.add("modal-lg")
                }
               



                    if (!response.ok)
                    throw new Error('Failed to load partial view');

                 html = await response.text();

            
                

                if (button.hasAttribute('data-updete')) 
                    updatedRow = event.target.closest('tr');


                if (button.hasAttribute('data-title')) 
                    document.getElementById("ModalTitle").innerHTML = button.getAttribute("data-title");

                if (button.hasAttribute('data-sub-title'))
                    document.getElementById("ModalSubTitle").innerHTML = button.getAttribute("data-sub-title");

                let modalBody = document.getElementById("modal-body");

                modalBody.innerHTML = html;
                

                if (button.hasAttribute('data-has-table')) {

                    let tableId = button.getAttribute('data-table-id')
                    //testtt(tableId)
                    InitilaizeMetronicDatatable(tableId);
                  //  InitilaizeDatatable(tableId);

                }
                // Reinitialize validation
                $.validator.unobtrusive.parse(modalBody);


            } catch {


            }

            modal.show();


        }
    })

}

function handleTableChiled() {
    document.body.addEventListener('click', async function (e) {

        if (e.target.closest(".js-table-chiled")) {

            let tr = e.target.closest('tr');
            if (!tr) return;

            let button = e.target.closest('a');

            // Dynamically get the correct DataTable instance
            let dt = $(tr).closest('table').DataTable();
            let row = dt.row(tr);

            // If this row is already open, just close it
            if (row.child.isShown()) {
                $('.child-slide', row.child()).slideUp(50, function () {
                    row.child.hide();
                });
            } 
            else {
                // ❗Close any other open child row
                dt.rows().every(function () {
                    if (this.child.isShown()) {
                        this.child.hide();
                    }
                });



                try {
                    const url = button.getAttribute('data-url');
                    let response;

                    if (button.hasAttribute('data-json-data')) {
                        const jsonData = button.getAttribute('data-json-data');
                        response = await fetch(url, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json'
                            },
                            body: jsonData
                        });
                    } else {
                        response = await fetch(url);
                    }

                    if (!response.ok)
                        throw new Error('Failed to load partial view');

                    let html = await response.text();
                    row.child(html).show();

                    // Optional animation (make sure child has class `child-slide` if you want this)
                    // $('.child-slide', row.child()).slideDown(200);

                    initilazeTimePicker();
                    $.validator.unobtrusive.parse(tr.nextElementSibling);
                } catch (err) {
                    console.error('Child row load error:', err);
                }

            }
        }
    });
}


//function handleTableChiled() {

//    // let table = $('#kt_datatable_dom_positioning').DataTable();

//    document.body.addEventListener('click', async function (e) {
//    //document.querySelector('#kt_datatable_dom_positioning tbody').addEventListener('click', async function (e) {



//        if (e.target.closest(".js-table-chiled")) {

//            let tr = e.target.closest('tr');
//            if (!tr) return;

//            let button = e.target.closest('a');

//            //let detailsJson = tr.getAttribute("data-details");

//            //let detailsObject = JSON.parse(detailsJson);

//            let dt = $(tr).closest('table').DataTable();
//            let row = dt.row(tr);

//            //let row = table.row(tr);

//            console.log(row);
//            console.log(tr);

//            if (row.child.isShown()) {
//                row.child.hide();
//                // Animate hiding
//                $('.child-slide', row.child()).slideUp(50, function () {
//                    row.child.hide(); // Hide the DataTable row after animation completes
//                });
//            }
//        } else {
//            // ❗Close any other open child row
//            dt.rows().every(function () {
//                if (this.child.isShown()) {
//                    this.child.hide();
//                }
//            });






//                try {


//                    const url = button.getAttribute('data-url');

//                    let response;

//                    if (button.hasAttribute('data-json-data')) {

//                        const jsonData = button.getAttribute('data-json-data');

//                         response = await fetch(url, {
//                            method: 'POST',
//                            headers: {
//                                'Content-Type': 'application/json' // IMPORTANT!
//                            },
//                            body: jsonData
//                        })

//                    }
//                    else 
//                        response = await fetch(url);



//                    if (!response.ok)
//                        throw new Error('Failed to load partial view');

//                     html = await response.text();



//                    row.child(html).show();

//                    initilazeTimePicker();
//                    $.validator.unobtrusive.parse(tr.nextElementSibling);


//                } catch {


//                }







            
//        }








//    });

//}
function RenderCard() {

    document.body.addEventListener('click', async function (event) {

        if (event.target && event.target.matches('.js-render-card')) {

            event.preventDefault()
            const button = event.target;
            const cardId = button.getAttribute('data-card-id');

            try {

                const response = await fetch(button.getAttribute('data-url'));

                if (!response.ok)
                    throw new Error('Failed to load partial view');

                 html = await response.text();



                document.getElementById(cardId).innerHTML = html;


                // Reinitialize validation
                $.validator.unobtrusive.parse(modalBody);


            } catch {


            }


        }
    })

}

function GoToNextPageAjax() {

    document.body.addEventListener('click', async function (event) {

        if (event.target && event.target.closest('.js-next-page-ajax')) {


            event.preventDefault()
            const button = event.target.closest('.js-next-page-ajax');

            try {

                //const response = await fetch(button.getAttribute('data-url'));

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

                let container = document.getElementById("PageContainer")

                container.innerHTML = await response.text();

                //const callbackFunctionName = button.getAttribute('data-callback');

                for (let callbackFunctionName of button.attributes) {

                    if (callbackFunctionName.name.startsWith('data-callback-') && typeof window[callbackFunctionName.value] === 'function') {

                        window[callbackFunctionName.value]();
                        
                    }
                }


                //if (callbackFunctionName && typeof window[callbackFunctionName] === 'function')
                //    window[callbackFunctionName]();

                //initilazeTimePicker();

            } catch {



            }


        }


    })
}
function GoToPreviousPage() {

    document.body.addEventListener('click',  function (event) {

        if (event.target && event.target.closest('.js-previous-page')) {


            let container = document.getElementById("PageContainer")

            container.outerHTML = html;

        }
    })

}

//function initilazeTimePicker() {
//    $(".flatpickr-input").flatpickr({
//        enableTime: true,
//        noCalendar: true,
//        dateFormat: "H:i",
//    });
//}

//function initilazeDateePicker() {
//    $(".flatpickr-date").flatpickr();
//}

RenderModal();
RenderCard();
Toggle();
SubmitAjaxForm();
SumbitForm();
handleTableChiled();
GoToNextPageAjax();
GoToPreviousPage();

