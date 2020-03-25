// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function hillClimbingClicked() {
    document.getElementById("chooseBoradSize").style.display = "block";
    document.getElementById("hillClimbingVar").style.display = "none";
    document.getElementById("labelInsertVariables").style.display = "none";
}

function simulatedAnnealingClicked() {
    document.getElementById("chooseBoradSize").style.display = "block";
    document.getElementById("simulatedAnnealingVar").style.display = "block";
    document.getElementById("labelInsertVariables").style.display = "block";
}
