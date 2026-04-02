const form = document.getElementById("subscribeForm");
const msg = document.getElementById("subscribeMessage");

// ✅ SUBSCRIBE LOGIC


const name = document.getElementById("nameInput").value;
const email = document.getElementById("emailInput").value;

fetch('/Subscriber/Subscribe', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify({
        Name: name,
        Email: email
    })
})
    .then(res => res.json())
    .then(data => {

        // ✅ CLEAR FIRST (IMPORTANT)
        msg.innerText = "";
        msg.classList.remove("success", "error");

        // ✅ SET NEW MESSAGE
        msg.innerText = data.message;

        if (data.success) {
            msg.classList.add("success");
            form.reset();
        } else {
            msg.classList.add("error");
        }

        // ✅ AUTO HIDE
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
