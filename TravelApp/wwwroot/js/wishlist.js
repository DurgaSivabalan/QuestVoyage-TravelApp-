$(document).ready(function () {

    const currentUser = localStorage.getItem("currentUser");

    if (!currentUser) {
        alert("Login First");
        window.location.href = "/Login/Login";
        return;
    }

    let wishlist =
        JSON.parse(localStorage.getItem(
            "wishlist_" + currentUser
        )) || [];

    const container = $("#wishlistContainer");

    function renderWishlist() {

        container.empty();

        if (wishlist.length === 0) {
            container.html("<p class='empty'>Wishlist Empty</p>");
            return;
        }

        wishlist.forEach(function (item) {

            container.append(`
<div class="card">
<h3>${item.name}</h3>
<p class="price">₹${item.price}</p>

<div class="btn-group">
<button class="remove" data-id="${item.id}">
Remove
</button>

<button class="book" data-id="${item.id}">
Book Trip
</button>
</div>
</div>
`);

        });

    }

    /* BOOK */
    $(document).on("click", ".book", function () {

        const id = Number($(this).data("id"));

        const selectedPackage =
            wishlist.find(item => item.id === id);

        localStorage.setItem(
            "selectedPackage",
            JSON.stringify(selectedPackage)
        );

        window.location.href = "/Book/Book";

    });

    renderWishlist();

});
function removeFromWishlist(packageId) {
    var card = document.getElementById('card-' + packageId);
    card.classList.add('removing');

    setTimeout(function () {
        // Replace with your actual remove endpoint
        window.location.href = '/Wishlist/Remove/' + packageId;
    }, 300);
}