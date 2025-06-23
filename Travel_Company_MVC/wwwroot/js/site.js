
var updatedRow;
var html;
const table = InitilaizeMetronicDatatable();

function addRowToTable(newRow, form) {

    console.log(updatedRow);
    console.log(newRow);

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
function InitilaizeDatatable() {


    return new DataTable('#kt_datatable_dom_positioning');
}
function InitilaizeMetronicDatatable(tableId ='#kt_datatable_dom_positioning') {


   // $("#kt_datatable_dom_positioning").DataTable({
   return $(tableId).DataTable({
        "language": {
            "lengthMenu": "Show _MENU_",
        },
        "dom":
            "<'row mb-2'" +
            "<'col-sm-6 d-flex align-items-center justify-conten-start dt-toolbar'l>" +
            "<'col-sm-6 d-flex align-items-center justify-content-end dt-toolbar'f>" +
            ">" +

            "<'table-responsive'tr>" +

            "<'row mt-5'" +
            "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
            "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
            ">"
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

                if (button.hasAttribute('data-updete'))
                    updatedRow = event.target.closest('tr');


                if (button.hasAttribute("data-large-modal"))
                    document.getElementById("ModalDialog").classList.add("modal-xl")
                else
                    document.getElementById("ModalDialog").classList.remove("modal-xl")

                    if (!response.ok)
                    throw new Error('Failed to load partial view');

                 html = await response.text();

            
                

                if (button.hasAttribute('data-updete')) 
                    updatedRow = event.target.closest('tr');

            console.log(updatedRow)

                if (button.hasAttribute('data-title')) 
                    document.getElementById("ModalTitle").innerHTML = button.getAttribute("data-title");

                if (button.hasAttribute('data-sub-title'))
                    document.getElementById("ModalSubTitle").innerHTML = button.getAttribute("data-sub-title");

                let modalBody = document.getElementById("modal-body");

                modalBody.innerHTML = html;
                

                if (button.hasAttribute('data-has-table'))
                    InitilaizeMetronicDatatable();
                // Reinitialize validation
                $.validator.unobtrusive.parse(modalBody);


            } catch {


            }

            modal.show();


        }
    })

}
function handleTableChiled() {

    // let table = $('#kt_datatable_dom_positioning').DataTable();

    document.querySelector('#kt_datatable_dom_positioning tbody').addEventListener('click', async function (e) {



        if (e.target.closest(".js-table-chiled")) {

            let tr = e.target.closest('tr');
            if (!tr) return;

            //let detailsJson = tr.getAttribute("data-details");

            //let detailsObject = JSON.parse(detailsJson);

            let row = table.row(tr);

            if (row.child.isShown()) {
                row.child.hide();
                // Animate hiding
                $('.child-slide', row.child()).slideUp(50, function () {
                    row.child.hide(); // Hide the DataTable row after animation completes
                });
            } else {

                try {


                    let test = e.target.closest('a').getAttribute('data-url');
                    const response = await fetch(e.target.closest('a').getAttribute('data-url'));



                    if (!response.ok)
                        throw new Error('Failed to load partial view');

                     html = await response.text();



                    row.child(html).show();

                    //$('.child-slide', row.child()).slideDown(600); // Animate it
                    initilazeTimePicker();
                    // Reinitialize validation
                    $.validator.unobtrusive.parse(tr.nextElementSibling);


                } catch {


                }







            }
        }








    });

}
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

            container.innerHTML = html;

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

