document.addEventListener("DOMContentLoaded", function () {

    const pkg = document.getElementById("packageName");
    const adults = document.getElementById("adults");
    const children = document.getElementById("children");
    const totalPrice = document.getElementById("totalPrice");

    const departureInput = document.getElementById("departureDate");
    const arrivalInput = document.getElementById("arrivalDate");
    const errorMsg = document.getElementById("dateError");

    const travellersContainer = document.getElementById("travellersContainer");

    const priceInput = document.getElementById("priceInput");
    const membersInput = document.getElementById("membersInput");

    const form = document.getElementById("bookingForm");
    const btn = document.getElementById("addTravellersBtn");

    // ✅ PRICE FUNCTION
    function calculatePrice() {

        if (!pkg || !adults || !children) return;

        const selectedId = pkg.value;
        if (!selectedId) return;

        fetch(`/Book/GetPrice?packageId=${selectedId}`)
            .then(res => res.json())
            .then(data => {

                const price = data.price || 0;

                const memberCount =
                    (parseInt(adults.value) || 0) +
                    (parseInt(children.value) || 0);

                const total = price * memberCount;

                if (totalPrice) totalPrice.textContent = total;
                if (priceInput) priceInput.value = price;
            });
    } // ✅ CLOSED PROPERLY

    // ✅ EVENT LISTENERS (OUTSIDE FUNCTION)
    if (pkg) pkg.addEventListener("change", calculatePrice);
    if (adults) adults.addEventListener("input", calculatePrice);
    if (children) children.addEventListener("input", calculatePrice);

    if (adults) adults.addEventListener("input", generateTravellers);
    if (children) children.addEventListener("input", generateTravellers);

    // ---------------- DATE VALIDATION ----------------
    function validateDates() {

        if (!departureInput || !arrivalInput) return true;

        if (!departureInput.value || !arrivalInput.value) return true;

        const dep = new Date(departureInput.value);
        const arr = new Date(arrivalInput.value);

        if (arr <= dep) {
            if (errorMsg) {
                errorMsg.innerText = "Arrival date must be after departure date";
                arrivalInput.style.borderColor = "red";
            }
            return false;
        }

        if (errorMsg) errorMsg.innerText = "";
        arrivalInput.style.borderColor = "#ccc";

        return true;
    }

    if (departureInput) departureInput.addEventListener("input", validateDates);
    if (arrivalInput) arrivalInput.addEventListener("input", validateDates);

    // ---------------- TRAVELLERS ----------------
    function generateTravellers() {

        const total =
            (parseInt(adults.value) || 0) +
            (parseInt(children.value) || 0);

        const existing = travellersContainer.children.length;

        while (travellersContainer.children.length > total) {
            travellersContainer.removeChild(travellersContainer.lastChild);
        }

        for (let i = existing; i < total; i++) {
            const html = `
            <div class="traveller-box">
                <input type="text" name="Travellers[${i}].Name" placeholder="Name" required />
                <input type="number" name="Travellers[${i}].Age" placeholder="Age" required />
                <select name="Travellers[${i}].Gender">
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                </select>
            </div>`;
            travellersContainer.insertAdjacentHTML("beforeend", html);
        }
    }

    if (btn) {
        btn.addEventListener("click", generateTravellers);
    }

    // ---------------- FORM SUBMIT ----------------
    if (form) {
        form.addEventListener("submit", function (e) {

            if (!validateDates()) {
                e.preventDefault();
                return;
            }

            const adultCount = parseInt(adults.value) || 0;
            const childCount = parseInt(children.value) || 0;

            if (membersInput)
                membersInput.value = adultCount + childCount;
        });
    }

    // ✅ INITIAL LOAD
    calculatePrice();
    generateTravellers();
});