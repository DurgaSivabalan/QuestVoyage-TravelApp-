console.log("Auth JS Connected");

document.addEventListener("DOMContentLoaded", function () {

    window.logoutUser = function () {
        window.location.href = "/Login/Login";
    };

});
const toggle = document.getElementById("profileToggle");
const box = document.getElementById("profileBox");

if (toggle) {
    toggle.addEventListener("click", () => {
        box.style.display =
            box.style.display === "block" ? "none" : "block";
    });
}