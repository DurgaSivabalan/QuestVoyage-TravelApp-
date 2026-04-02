document.addEventListener("DOMContentLoaded", function () {
    loadDashboard();
});

function loadDashboard() {

    const bookings =
        JSON.parse(localStorage.getItem("bookingHistory")) || [];

    const users =
        JSON.parse(localStorage.getItem("users")) || [];

    let totalRevenue = 0;
    let totalWishlist = 0;

    const table = document.getElementById("bookingTable");

    if (!table) return; // ✅ fix

    table.innerHTML = "";

    bookings.forEach((booking, index) => {

        totalRevenue += booking.total || 0;

        table.innerHTML += `
<tr>
<td>${booking.bookingId || "-"}</td>
<td>${booking.email || "-"}</td>
<td>${booking.package || "-"}</td>
<td>${booking.members || 0}</td>
<td>₹${booking.total || 0}</td>

<td>
<select onchange="updateAction(${index},this.value)">
<option ${booking.action === "Pending" ? "selected" : ""}>Pending</option>
<option ${booking.action === "Confirmed" ? "selected" : ""}>Confirmed</option>
<option ${booking.action === "Hold" ? "selected" : ""}>Hold</option>
<option ${booking.action === "Cancelled" ? "selected" : ""}>Cancelled</option>
</select>
</td>

<td>
<select onchange="updateProcess(${index},this.value)">
<option ${booking.process === "Booked" ? "selected" : ""}>Booked</option>
<option ${booking.process === "Visa Processing" ? "selected" : ""}>Visa Processing</option>
<option ${booking.process === "Ticket Issued" ? "selected" : ""}>Ticket Issued</option>
<option ${booking.process === "Departed" ? "selected" : ""}>Departed</option>
<option ${booking.process === "Arrived" ? "selected" : ""}>Arrived</option>
<option ${booking.process === "Completed" ? "selected" : ""}>Completed</option>
</select>
</td>
</tr>`;
    });

    document.getElementById("totalBookings").innerText = bookings.length;
    document.getElementById("totalRevenue").innerText = "₹" + totalRevenue;

    users.forEach(user => {
        const list =
            JSON.parse(localStorage.getItem("wishlist_" + user.Email)) || []; // ✅ fix

        totalWishlist += list.length;
    });

    document.getElementById("totalWishlist").innerText = totalWishlist;
}