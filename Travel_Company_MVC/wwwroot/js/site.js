
function InitilaizeDatatable() {


    return new DataTable('#datatable');
}

function ErrorMessage() {

    Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Something went wrong!"
    });
}
function SuccessMessage() {

    Swal.fire({
        icon: "success",
        title: "Successed",
        text: "it is successed"
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
function SubmitAjaxForm() {
    document.body.addEventListener('submit', async function (event) {

    if(event.target && event.target.matches(".js-ajax-form"))
    {
        event.preventDefault();

        ConfirmationMessage().then(async (confirmed) => {

            if (confirmed) {
                try {

                    const url = event.target.getAttribute('data-url');
                    const callbackName = event.target.getAttribute('data-callback');
                    const formData = new FormData(event.target);

           
                    const response = await fetch(url, {
                        method: 'POST',
                        body: formData
                    })

                    if (response.ok) {

                        let responseData = await response.text();

                        if (callbackName && typeof window[callbackName] === 'function') 
                            window[callbackName](responseData, event.target);

                    }
                    else 
                        throw new Error('Failed to load partial view');
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
        
        if (event.target && event.target.matches('.js-render-modal')) {

            event.preventDefault()
            const button = event.target;
           // const id = button.getAttribute('data-travel-id');
            // modal-xl
            try {

                const response = await fetch(button.getAttribute('data-url'));

                if (!response.ok)
                    throw new Error('Failed to load partial view');

                const html = await response.text();

                if (button.hasAttribute("data-large-modal"))
                    document.getElementById("ModalDialog").classList.add("modal-xl")


                if (event.target.hasAttribute('data-updete')) 
                    updatedRow = event.target.closest('tr');

                document.getElementById("ModalTitle").innerHTML = button.getAttribute("data-title");

                let modalBody = document.getElementById("modal-body");
                modalBody.innerHTML = html;
                


                // Reinitialize validation
                $.validator.unobtrusive.parse(modalBody);


            } catch {


            }

            modal.show();


        }
    })

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

                const html = await response.text();



                document.getElementById(cardId).innerHTML = html;


                // Reinitialize validation
                $.validator.unobtrusive.parse(modalBody);


            } catch {


            }


        }
    })

}


RenderModal();
RenderCard();
Toggle();
SubmitAjaxForm();
const table = InitilaizeDatatable();