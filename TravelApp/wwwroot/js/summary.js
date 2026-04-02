let booking =
    JSON.parse(localStorage.getItem("currentBooking"));

const container =
    document.getElementById("summaryData");

if (!booking) {
    container.innerHTML = "<p>No Booking Found</p>";
}
else {

    booking.bookingId = "QV" + Date.now();
    booking.total = booking.price * booking.members;

    localStorage.setItem(
        "currentBooking",
        JSON.stringify(booking)
    );

    container.innerHTML = `

<div class="details">
<div class="label">Booking ID</div>
<div class="value">${booking.bookingId}</div>

<div class="label">Package</div>
<div class="value">${booking.package}</div>

<div class="label">Customer</div>
<div class="value">${booking.name}</div>

<div class="label">Members</div>
<div class="value">${booking.members}</div>



<div class="label">Departure</div>
<div class="value">${booking.departureDate}</div>

<div class="label">Arrival</div>
<div class="value">${booking.arrivalDate}</div>


<div class="label">Payment</div>
<div class="value">${booking.payment}</div>
</div>

<div class="total-box">
Total Amount<br>
<span>₹${booking.total}</span>
</div>
`;
}

function flexBooking() {

    const currentUser =
        localStorage.getItem("currentUser");

    if (!currentUser) {
        alert("Login required");
        return;
    }

    booking.email = currentUser;
    booking.action = "Pending";
    booking.process = "Booked";
    let userBookings =
        JSON.parse(
            localStorage.getItem(
                "booking_" + currentUser
            )
        ) || [];

    userBookings.push(booking);

    localStorage.setItem(
        "booking_" + currentUser,
        JSON.stringify(userBookings)
    );


    let allBookings =
        JSON.parse(
            localStorage.getItem("bookingHistory")
        ) || [];

    allBookings.push(booking);

    localStorage.setItem(
        "bookingHistory",
        JSON.stringify(allBookings)
    );


    localStorage.setItem(
        "currentBooking",
        JSON.stringify(booking)
    );


}
$(document).ready(function () {



    $("#bookingPopup").fadeIn(400);

    setTimeout(function () {

        $("#bookingPopup").fadeOut(3000);
        window.location.href = "invoice.html";

    }, 2000);
});
