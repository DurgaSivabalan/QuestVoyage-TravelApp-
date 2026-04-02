let isOpen = false;

$("#profileToggle").click(function (e) {
    e.stopPropagation();

    if (isOpen) {
        $("#profileBox").removeClass("show");
    } else {
        $("#profileBox").addClass("show");
    }

    isOpen = !isOpen;
});

// prevent closing when clicking inside
$("#profileBox").click(function (e) {
    e.stopPropagation();
});

// click outside → close
$(document).click(function () {
    $("#profileBox").removeClass("show");
    isOpen = false;
});
function toggleEdit() {
    var form = document.getElementById("editForm");
    if (form.style.display === "none") {
        form.style.display = "block";
    }
    else {
        form.style.display = "none";
    }
}