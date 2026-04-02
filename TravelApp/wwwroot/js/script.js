document.addEventListener("DOMContentLoaded", function () {
    console.log("JS READY");

    const form = document.getElementById("subscribeForm");
    const msg = document.getElementById("subscribeMessage");

    // ✅ SUBSCRIBE LOGIC
    if (form) {
        form.addEventListener("submit", function (e) {
            e.preventDefault(); // stop page reload

            const name = document.getElementById("nameInput").value;
            const email = document.getElementById("emailInput").value;

            fetch('/Subscriber/Subscribe', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ Name: name, Email: email })
            })
                .then(res => res.json())
                .then(data => {
                    msg.innerText = "";
                    msg.classList.remove("success", "error");

                    msg.innerText = data.message;

                    if (data.success) {
                        msg.classList.add("success");
                        form.reset();
                    } else {
                        msg.classList.add("error");
                    }

                    setTimeout(() => {
                        msg.innerText = "";
                        msg.classList.remove("success", "error");
                    }, 3000);
                })
                .catch(err => {
                    console.error("ERROR:", err);
                });
        });
    }

    // ✅ HAMBURGER MENU
    console.log("JS Loaded ✅");

    const hamburger = document.getElementById("hamburger");
    const sidebar = document.getElementById("sidebar");
    const overlay = document.getElementById("overlay");
    const closeBtn = document.getElementById("closeBtn");

    if (hamburger && sidebar && overlay && closeBtn) {
        hamburger.addEventListener("click", function () {
            sidebar.classList.add("active");
            overlay.classList.add("active");
        });

        closeBtn.addEventListener("click", function () {
            sidebar.classList.remove("active");
            overlay.classList.remove("active");
        });

        overlay.addEventListener("click", function () {
            sidebar.classList.remove("active");
            overlay.classList.remove("active");
        });
    } else {
        console.log("Element missing ❌");
    }
});
