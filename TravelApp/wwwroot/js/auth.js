console.log("Auth JS Connected");

document.addEventListener("DOMContentLoaded", function () {

    window.logoutUser = function () {
        window.location.href = "/Login/Logout";
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
function showTab(tabId) {

    document.querySelectorAll(".tab-content").forEach(tab => {
        tab.classList.remove("active")
    })

    document.querySelectorAll(".tab-btn").forEach(btn => {
        btn.classList.remove("active")
    })

    document.getElementById(tabId).classList.add("active")
    event.target.classList.add("active")

}
//function showTab(tabName) {
//    document.querySelectorAll(".tab-content").forEach(t => t.classList.remove("active"));
//    document.querySelectorAll(".tab-btn").forEach(b => b.classList.remove("active"));

//    document.getElementById(tabName).classList.add("active");
//    event.target.classList.add("active");
//}
function updateStatus(id, status) {
    fetch('/Admin/UpdateStatus', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: `id=${id}&status=${status}`
    });
}

function updateProcess(id, process) {
    fetch('/Admin/UpdateProcess', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: `id=${id}&process=${process}`
    });
}

function updatePayment(id, payment) {
    fetch('/Admin/UpdatePayment', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: `id=${id}&payment=${payment}`
    });
}

function deleteBooking(id) {
    if (confirm("Delete this booking?")) {
        window.location.href = "/Admin/DeleteBooking?id=" + id;
    }
}

function saveData() {
    alert("Data saved successfully ✅");
}