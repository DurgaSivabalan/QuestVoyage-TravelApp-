const booking = JSON.parse(localStorage.getItem("currentBooking"));

if (!booking) {
    document.getElementById("invoiceData").innerHTML =
        "<p style='color:red;'>No Invoice Data Found</p>";
}
else {

    document.getElementById("invoiceData").innerHTML = `
        <p><strong>Booking ID:</strong> ${booking.bookingId}</p>
        <p><strong>Customer:</strong> ${booking.name}</p>
        <p><strong>Email:</strong> ${booking.email}</p>
        <p><strong>Package:</strong> ${booking.package}</p>
        <p><strong>Members:</strong> ${booking.members}</p>
        <p><strong>Payment Mode:</strong> ${booking.payment}</p>
        <p class="total">Total Paid: ₹${booking.total}</p>
        <p style="margin-top:15px;color:gray;">
            Thank you for booking with QuestVoyage!
        </p>
    `;
}

function printInvoice() {
    window.print();
}