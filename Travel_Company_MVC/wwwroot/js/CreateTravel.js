



//let selectdDaysInWeek = JSON.parse(document.getElementById("SelectedDaysInWeek").value);
let selectdDaysInWeek=[];
//console.log(selectdDaysInWeek);


document.querySelectorAll(".js-item-checkbox").forEach((checkBox) => {


  

    checkBox.addEventListener("change", function() {

        const value = this.value;
        console.log(value);

        //if (!selectdDaysInWeek.includes(x=>x.id===value)) {
        //    let x=new Object();
        //    x.Id = value;
        //    selectdDaysInWeek.push(x);
             if (!selectdDaysInWeek.includes(value)) {
         
                 selectdDaysInWeek.push(value);
        }
        else {
            selectdDaysInWeek = selectdDaysInWeek.filter(item => item !== value);
        }


        console.log(selectdDaysInWeek);
    })
})

function FireSweetAlert() {

    Swal.fire({
        title: 'Are you sure?',
        text: "Do you want to apply?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, apply!'
    }).then((result) => {
        if (result.isConfirmed) {
            showLoadingAndSendAjax();
        }
    });
}
document.getElementById("travel-form").addEventListener("submit", async function (event) {


   //    const x = document.getElementById("SelectedDaysInWeek");

    const y = document.getElementById("JsonDates");

    if (dates.length > 0) {
        y.value = JSON.stringify(dates);
    }

    //if (selectdDaysInWeek.length > 0) {

    //    x.value = JSON.stringify(selectdDaysInWeek);
    //}




    //event.preventDefault();

    //if ($(this).valid()) {


    //    const x = document.getElementById("SelectedDaysInWeek");

    //    const y = document.getElementById("JsonDates");

    //    if (dates.length > 0) {
    //        y.value = JSON.stringify(dates);
    //    }

    //    if (selectdDaysInWeek.length > 0) {

    //        x.value = JSON.stringify(selectdDaysInWeek);
    //    }


    //   // FireSweetAlert();




    //    console.log(selectdDaysInWeek);
    //    console.log(x.value);

    //}
    //else {

    //    Swal.fire({
    //        icon: 'success',
    //        title: 'Success!',
    //        text: 'Your application was successful!'
    //    });
    ////    Swal.fire({
    ////        title: "There are some invalid inputs!",
    ////        showClass: {
    ////            popup: `
    ////  animate__animated
    ////  animate__fadeInUp
    ////  animate__faster
    ////`
    ////        },
    ////        hideClass: {
    ////            popup: `
    ////  animate__animated
    ////  animate__fadeOutDown
    ////  animate__faster
    ////`
    ////        }
    ////    });
    //}



})

function showLoadingAndSendAjax() {
    Swal.fire({
        title: 'Processing...',
        html: 'Please wait while we process your application.',
        didOpen: () => {
            Swal.showLoading(); // Show loading spinner
           // sendAjaxRequest(); // Send AJAX request
        },
        allowOutsideClick: false, // Prevent closing by clicking outside
        allowEscapeKey: false, // Prevent closing by pressing Escape
        showConfirmButton: false // Hide the "OK" button
    });
}



let dates = [];

let jsonDates = document.getElementById("JsonDates").value;
if (jsonDates) {
    dates = JSON.parse(jsonDates);


    const container = document.getElementById("dates-table");


    if (dates.length > 0) {
        container.innerHTML = "";
    }
    console.log(dates);
    dates.forEach(d => {
        AddDateHtml(d);
    })
  
}

document.querySelector(".js-add-date").addEventListener("click", function () {

    let date = document.getElementById("js-date-value").value;

    if (!dates.includes(date)) {
        dates.push(date);

        AddDateHtml(date);
    }
  

    console.log(dates);

})

function AddDateHtml(date) {

    const container = document.getElementById("dates-table");

   
    if (dates.length === 1) {
        container.innerHTML = "";
    }

    container.innerHTML += GetDateInnerHtml(date);
}


function RemoveDateHtml( date) {

    let index = dates.findIndex(i => i === date);

    dates.splice(index, 1);

    const container = document.getElementById("dates-table");


    if (dates.length === 0) {
        container.innerHTML = GetNoDatesInnerHtml();
    }
    else {
        container.innerHTML = "";
        dates.forEach(function (date) {

            container.innerHTML += GetDateInnerHtml(date);

        })
    }

}

function GetDateInnerHtml(date) {


    return `

      <div class="item-list m-0 p-0">
             <div class="info-user ms-3">
                 <div class="date pe-0"><span style="color:dimgrey;"  >${date}</span></div>
             </div>

             <a href="#" class="text-decoration-none btn=sm " onclick = "event.preventDefault(); RemoveDateHtml('${date}')">
               
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3" viewBox="0 0 16 16">
                <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5M11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47M8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5"></path>
                </svg>
              </a>
 
        </div>

        `;


}

function GetNoDatesInnerHtml() {

    return ` <div class="alert alert-secondary mt-4" role="alert">
                   <span style="color:dimgrey;"> No dates added yet!</span>
             </div>  `;
}
