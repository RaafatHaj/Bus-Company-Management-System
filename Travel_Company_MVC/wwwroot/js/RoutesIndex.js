

$("#kt_datatable_dom_positioning").DataTable({
    "language": {
        "lengthMenu": "Show _MENU_",
    },
    "dom":
        "<'row mb-2'" +
        "<'col-sm-6 d-flex align-items-center justify-conten-start dt-toolbar'l>" +
        "<'col-sm-6 d-flex align-items-center justify-content-end dt-toolbar'f>" +
        ">" +

        "<'table-responsive'tr>" +

        "<'row'" +
        "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
        "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
        ">"
});




const button = document.querySelectorAll('.js-select-route')

button.forEach(e => e.addEventListener('click', e => {
    e.preventDefault();

    Swal.fire({
        text: "Here's a basic example of SweetAlert!",
        icon: "success",
        buttonsStyling: false,
        confirmButtonText: "Ok, got it!",
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}))







