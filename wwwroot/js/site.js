// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function displayNoneVariables() {
    document.getElementById("chooseBoradSize").style.display = "none";
    document.getElementById("labelInsertVariables").style.display = "none";

    document.getElementById("simulatedAnnealingVar").style.display = "none";
    document.getElementById("localBeamSearchVar").style.display = "none";
    document.getElementById("geneticAlgorithmsVar").style.display = "none";
    document.getElementById("hillClimbingVar").style.display = "none";
    document.getElementById("algorithm").style.display = "none";
    
}
function displayBlockVariables()
{
    document.getElementById("chooseBoradSize").style.display = "block";
    document.getElementById("labelInsertVariables").style.display = "block";
   

}


function hillClimbingClicked() {
    
    displayNoneVariables();
    displayBlockVariables()
    document.getElementById("chooseBoradSize").style.display = "block";

    document.getElementById("algorithm").value = "hillClimbing";
    document.getElementById("hillClimbingVar").style.display = "block";
    

}

function simulatedAnnealingClicked() {
    displayNoneVariables()
    displayBlockVariables();
    document.getElementById("simulatedAnnealingVar").style.display = "block";

    document.getElementById("algorithm").value = "simulatedAnnealing";
}

function localBeamSearchClicked() {
    displayNoneVariables()
    displayBlockVariables();
    document.getElementById("localBeamSearchVar").style.display = "block";

    document.getElementById("algorithm").value = "localBeamSearch";
}

function geneticAlgorithmsClicked() {
    displayNoneVariables()
    displayBlockVariables();
    document.getElementById("geneticAlgorithmsVar").style.display = "block";

    document.getElementById("algorithm").value = "geneticAlgorithm";
}

