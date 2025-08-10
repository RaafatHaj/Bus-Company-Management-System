
var updatedRow;
var html;
var table;
/*const table = InitilaizeMetronicDatatable();*/

function addRowToTable( newRow, form) {



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
         table = $('#' + tableId).DataTable({
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

            const customValidationFunction = event.target.getAttribute('data-custom-validation');

            if (customValidationFunction && typeof window[customValidationFunction] === 'function') {
                let isValid = window[customValidationFunction](event.target);
                if (!isValid)
                    return;

            }


            if (event.target.hasAttribute('data-confirm-message')) {

                let title = event.target.getAttribute('data-title');
                let message = event.target.getAttribute('data-message');

                ConfirmationMessage(title, message).then((confirmed) => {

                    if (confirmed)
                        event.target.submit();



                }).catch(() => {

                    ErrorMessage();
                });

            }
            else {
                event.target.submit();
            }
                
        }


    })

}
async function  _submitAjaxForm(target) {
    try {

        const button = target.querySelector('[type="submit"]');
        button.setAttribute("data-kt-indicator", "on");

        const url = target.getAttribute('data-url');
        const formData = new FormData(target);



        const response = await fetch(url, {
            method: 'POST',
            body: formData
        })

        if (response.ok) {

            let responseData = await response.text();

            //if (event.target.hasAttribute('data-updete')) {
            //    updatedRow = event.target.closest('tr');
            //    tableId = event.target.getAttribute('data-table-id');

            //}



            const callbackFunctionName = target.getAttribute('data-callback');

            if (callbackFunctionName && typeof window[callbackFunctionName] === 'function')
                window[callbackFunctionName](responseData);



            if (target.hasAttribute('success-message')) {

                SuccessMessage(target.getAttribute('success-message'));
            }


            button.removeAttribute("data-kt-indicator");

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
function SubmitAjaxForm() {
    document.body.addEventListener('submit', async function (event) {

    if(event.target && event.target.matches(".js-ajax-form"))
    {
        event.preventDefault();


        if (event.target.hasAttribute('data-confirm-message')) {

            let title = event.target.getAttribute('data-title');
            let message = event.target.getAttribute('data-message');



            ConfirmationMessage(title, message).then(async (confirmed) => {

                if (confirmed) {
                    _submitAjaxForm(event.target);

                }
            });
        }
        else {
            _submitAjaxForm(event.target);

        }

   

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

            const modalBody = document.getElementById("modal-body");

            modalBody.innerHTML = "<h1>Please Wait...</h1>";
            modal.show();


            try {

                const response = await fetch(button.getAttribute('data-url'));

                //if (button.hasAttribute('data-updete'))
                //    updatedRow = event.target.closest('tr');

                //modal-dialog modal-lg
         


                    if (!response.ok)
                    throw new Error('Failed to load partial view');

                 html = await response.text();

            
                

                if (event.target.hasAttribute('data-updete')) 
                    updatedRow = event.target.closest('tr');


                if (button.hasAttribute('data-has-drowpdownlist')) 
                KTMenu.createInstances();

                if (button.hasAttribute('data-title')) 
                    document.getElementById("ModalTitle").innerHTML = button.getAttribute("data-title");

                if (button.hasAttribute('data-sub-title'))
                    document.getElementById("ModalSubTitle").innerHTML = button.getAttribute("data-sub-title");

               
                modalBody.innerHTML = html;
                

                if (button.hasAttribute('data-has-table')) {

                    let tableId = button.getAttribute('data-table-id')
                    //testtt(tableId)
                    const modalTable= InitilaizeMetronicDatatable(tableId);
                  //  InitilaizeDatatable(tableId);

                }
                // Reinitialize validation
                $.validator.unobtrusive.parse(modalBody);


            } catch {


            }

      /*      modal.show();*/


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

                row.child.hide();
            } 
            else {
                // Close any other open child row
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


                    initilazeTimePicker();
                    $.validator.unobtrusive.parse(tr.nextElementSibling);
                } catch {
                }

            }
        }
    });
}

function RenderCard() {

    document.body.addEventListener('click', async function (event) {

        if (event.target && event.target.closest('.js-render-card')) {

            event.preventDefault()
            const button = event.target.closest('.js-render-card');
            const cardId = button.getAttribute('data-card-id');

            button.setAttribute("data-kt-indicator", "on");

            try {

                const response = await fetch(button.getAttribute('data-url'));

                if (!response.ok)
                    throw new Error('Failed to load partial view');

                 html = await response.text();



                document.getElementById(cardId).innerHTML = html;



                if (button.hasAttribute('data-has-table')) {

                    let tableId = button.getAttribute('data-table-id')
                    //testtt(tableId)
                    InitilaizeMetronicDatatable(tableId);
                    //  InitilaizeDatatable(tableId);

                }


                if (button.hasAttribute('data-has-dropdown-list')) {

                    KTMenu.createInstances();

    

                }

                const callbackFunctionName = event.target.getAttribute('data-callback');

                if (callbackFunctionName && typeof window[callbackFunctionName] === 'function')
                    window[callbackFunctionName]();

              

                button.removeAttribute("data-kt-indicator");
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

document.addEventListener('DOMContentLoaded', function () {
    const currentPath = window.location.pathname.toLowerCase();

    document.querySelectorAll('.menu-link[href]').forEach(link => {
        const href = link.getAttribute('href').toLowerCase();
        if (currentPath === href ) {
            link.classList.add('active');
            const accordion = link.closest('.menu-accordion');
            if (accordion) {
                accordion.classList.add('here', 'show');
            }
        }
    });
});



