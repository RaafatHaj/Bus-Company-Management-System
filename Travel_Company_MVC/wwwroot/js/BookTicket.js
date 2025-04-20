
var stationAId;
var stationBId;
function  childRowContent(details) {



    try {
        let url = "/Booking/GetAvaliableSeats";

        fetch(url, {
            method: 'Get',
            headers: {
                'Content-Type': 'application/json', // Make sure to set the correct content type
            },
            body: details
        }).then(response => response.json())
            .then(data => {
                return (
                    '<dl class="child-row-content">' +
                    '<dt>Full name:</dt>' +
                    '<dd>' + "[test]" + '</dd>' +
                    '<dt>Position:</dt>' +
                    '<dd>' + "[test]" + '</dd>' +
                    '<dt>Office:</dt>' +
                    '<dd>' + "[test]" + '</dd>' +
                    '<dt>Salary:</dt>' +
                    '<dd>' + "[test]" + '</dd>' +
                    '</dl>'
                );

            })

  
    }
    catch {
        console.log("Cancelled.");

    }


    // Steps ...
    // 1.Deserilize details attribute ...
    // 2.Ajax requst to with Data ...
    // 3.Render result ...

   // Parse the JSON stored in 'data-details'



}

function test2(s) {
   
    return ` <button class="btn btn-sm btn-primary" >${s}</button>`;
}
function test(data) {

   
  

    let result = `<div class="child-row-content">`

    for (var i = 0; i < data.length; i++) {

    }


    data.forEach((s) => {

   
        result += test2(s);
        
    })

    result += ` </div>`;
   
    return result;
}


const travelContainer = document.getElementById("suitable-travels-container");

function findSuitableTravels(results , form) {


    travelContainer.innerHTML = "";
    travelContainer.innerHTML = results;

    const table = InitilaizeDatatable();


    document.querySelector('#datatable tbody').addEventListener('click',async function (e) {

        if (e.target.closest('.child-row-content')) {
            return; 
        }

        if (e.target.matches(".js-table-actions")) {
            return;
        }

        let tr = e.target.closest('tr');
        if (!tr) return; 

        let detailsJson = tr.getAttribute("data-details");

        let detailsObject = JSON.parse(detailsJson);

        let row = table.row(tr);

        let url = tr.getAttribute("data-url");
        stationAId = document.getElementById("js-stationA").value;
        stationBId = document.getElementById("js-stationB").value;
        console.log(stationAId);
        console.log(stationBId);

        detailsObject.stationAId = stationAId;
        detailsObject.stationBId = stationBId;

        detailsJson = JSON.stringify(detailsObject);

        try {
         //   let url = "/Booking/GetAvaliableSeats";

             let response = await fetch(url, {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json', // Make sure to set the correct content type
                },
                body: detailsJson
            });

            if (response.ok) {

                let data = await response.text();


                if (row.child.isShown()) {

                    row.child.hide();
                } else {
                    console.log(data)
                    row.child(data).show();
                }

            }



  


        }
        catch {
            console.log("Cancelled.");

        }

     
    });

}


















const stations = JSON.parse(document.getElementById("JsonStations").value);

function FillStationBDropdownList(selectedStationId) {

    let stationBOptions = stations.filter(s => s.StationId != selectedStationId)

    let stationBDropdownList = document.getElementById("js-stationB");

    stationBDropdownList.innerHTML = "";

    // Append the new Empty option to the Dropdown List

    let newOption = document.createElement("option");
    newOption.text = "";
    stationBDropdownList.appendChild(newOption);

    stationBOptions.forEach(s => {

        // Create a new option element
        let newOption = document.createElement("option");
        newOption.value = s.StationId;
        newOption.text = s.StationName;

        // Append the new option to the Dropdown List

        stationBDropdownList.appendChild(newOption);

    })
}
document.getElementById("js-stationA").addEventListener("change", function () {

    FillStationBDropdownList(this.value) 

})

