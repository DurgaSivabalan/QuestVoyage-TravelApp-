displayPackages(packages);
function displayPackages(data) {

    const container = document.getElementById("packageContainer");
    container.innerHTML = "";

    data.forEach(pkg => {

        let img = pkg.image
            ? `<img src="data:image/jpeg;base64,${pkg.image}" />`
            : `<div>No Image</div>`;

        container.innerHTML += `
        <div class="card">

            ${img}

            <h3>${pkg.name}</h3>
            <p class="price">₹${pkg.price}</p>
            <p>${pkg.type}</p>
            <p>${pkg.duration} Days</p>

            <div class="button-group">
                <button onclick="addToWishlist(${pkg.id})">
    Wishlist
</button>

                <button class="book-btn"
                onclick="window.location.href='/Book/Book'">
                    Book
                </button>
            </div>

        </div>`;
    });
}
function filterPackages() {

    const price = document.getElementById("priceFilter").value;
    const type = document.getElementById("typeFilter").value;
    const duration = document.getElementById("durationFilter").value;

    const filtered = packages.filter(pkg => {

        let priceMatch =
            price === "all" ||
            (price === "low" && pkg.Price < 20000) ||
            (price === "mid" && pkg.Price >= 20000 && pkg.Price <= 40000) ||
            (price === "high" && pkg.Price > 40000);

        let typeMatch =
            type === "all" ||
            pkg.Type.toLowerCase() === type.toLowerCase();

        let durationMatch =
            duration === "all" ||
            (duration === "short" && pkg.Duration <= 2) ||
            (duration === "medium" && pkg.Duration >= 3 && pkg.Duration <= 5) ||
            (duration === "long" && pkg.Duration > 5);

        return priceMatch && typeMatch && durationMatch;
    });

    displayPackages(filtered);
}

function addToWishlist(id) {

    fetch('/Wishlist/Add', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: `id=${id}` // ✅ IMPORTANT
    })
        .then(response => {
            if (response.status === 400) {
                alert("Already added ❌");
                return;
            }

            if (response.ok) {
                alert("Added to wishlist ✅");
            }
            else {
                alert("Login first");
            }
        });
}

   
