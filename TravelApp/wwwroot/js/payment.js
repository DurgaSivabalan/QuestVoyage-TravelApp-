
function startPayment() {

    let selected = document.querySelector("input[name='method']:checked");

    if (!selected) {
        alert("Please select a payment method");
        return;
    }

    let method = selected.value;

    // 🔥 SET VALUES SAFELY
    document.getElementById("paymentMethod").value = method;
    document.getElementById("paymentStatus").value = "Pending";

    // DEBUG (optional)
    console.log("Method:", method);

    document.getElementById("loader").style.display = "block";

    setTimeout(() => {

        let success = Math.random() > 0.2;

        if (success) {

            document.getElementById("paymentStatus").value = "Paid";

            console.log("Submitting:", {
                method: document.getElementById("paymentMethod").value,
                status: document.getElementById("paymentStatus").value
            });

            document.getElementById("confirmForm").submit();
        }
        else {

            document.getElementById("paymentStatus").value = "Failed";

            alert("Payment Failed ❌ Try again");

            document.getElementById("loader").style.display = "none";
        }

    }, 1500);
}
document.addEventListener("DOMContentLoaded", function () {

    const applyBtn = document.getElementById("applyCoupon");
    const couponInput = document.getElementById("couponInput");
    const discountEl = document.getElementById("discount");
    const totalEl = document.getElementById("totalAmount");
    const payAmountEl = document.getElementById("payAmount");

    if (!applyBtn) return;

    let originalAmount = parseFloat(totalEl.innerText);
    let discount = 0;

    applyBtn.addEventListener("click", function () {

        const code = couponInput.value.trim().toUpperCase();

        if (code === "SAVE10") {
            discount = originalAmount * 0.10;
        }
        else if (code === "FLAT500") {
            discount = 500;
        }
        else {
            alert("Invalid Coupon ❌");
            return;
        }

        const finalAmount = originalAmount - discount;

        discountEl.innerText = discount.toFixed(0);
        totalEl.innerText = finalAmount.toFixed(0);
        payAmountEl.innerText = finalAmount.toFixed(0);

        document.getElementById("finalAmountInput").value = finalAmount;    });

});

function goToSummary() {
    window.location.href = "/Book/Summary";
}