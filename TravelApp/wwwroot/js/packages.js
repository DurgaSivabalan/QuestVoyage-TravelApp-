    displayPackages(packages);


function displayPackages(data) {

    let container = document.getElementById("packageContainer");

    if (data.length === 0) {
        container.innerHTML = "<p>No packages found 😕</p>";
        return;
    }

    let html = "";

    data.forEach(p => {

        let img = p.image
            ? `<img src="data:image/jpeg;base64,${p.image}" />`
            : `<div>No Image</div>`;

        html += `
            <div class="card">

                ${img}

                <h3>${p.name}</h3>
                <p class="price">₹${p.price}</p>
                <p>${p.type}</p>
                <p>${p.duration} Days</p>

                <div class="button-group">
                    <button onclick="addToWishlist(${p.id})">
                        Wishlist
                    </button>

                    <button class="book-btn" onclick="window.location.href='/Book/Book?packageId=${p.id}'">
                        Book
                    </button>
                </div>

            </div>
        `;
    });

    container.innerHTML = html;
}
    function filterPackages() {

        let price = document.getElementById("priceFilter").value;
        let type = document.getElementById("typeFilter").value;
        let duration = document.getElementById("durationFilter").value;

        let filtered = packages.filter(p => {
            return (
                (price === "all" ||
                    (price === "low" && p.price < 20000) ||
                    (price === "mid" && p.price >= 20000 && p.price <= 40000) ||
                    (price === "high" && p.price > 40000)) &&

                (type === "all" || p.type === type) &&

                (duration === "all" ||
                    (duration === "short" && p.duration <= 2) ||
                    (duration === "medium" && p.duration <= 5) ||
                    (duration === "long" && p.duration >= 6))
            );
        });

        displayPackages(filtered);
    }

    function addToWishlist(id) {
        fetch('/Wishlist/Add', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: `id=${id}`
        })
            .then(response => {
                if (response.status === 400) {
                    alert("Already added ❌");
                    return;
                }

                if (response.ok) {
                    alert("Added to wishlist ✅");
                } else {
                    alert("Login first");
                }
            });
    }