
var updatedRow;

function UpdateTableRow(newRow, form) {



    if (updatedRow !== undefined) {

        table.row(updatedRow).remove().draw();

        updatedRow = undefined;
    }

    // since add function in datatable accept just array or jquery element we have to converet
    // Html String to jQuery element just lik that

    let jqueryElement = $(newRow)

    table.row.add(jqueryElement).draw();

    SuccessMessage();
    HideModal();
}


InitilaizeMetronicDatatable('Users_List_Table');