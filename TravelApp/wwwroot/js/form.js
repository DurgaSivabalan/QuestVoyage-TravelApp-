document.getElementById("contactForm").addEventListener("submit", function (e) {

    e.preventDefault();

    const form = this;
    const data = new FormData(form);

    fetch('/Contact/Submit', {
        method: 'POST',
        body: data
    })
        .then(res => res.json())
        .then(result => {

            if (result.success) {
                document.getElementById("successMessage").innerText = result.message;
                document.getElementById("errorMessage").innerText = "";
                form.reset();
            }
            else {
                document.getElementById("errorMessage").innerText = "Something went wrong";
            }

        })
        .catch(() => {
            document.getElementById("errorMessage").innerText = "Server error";
        });

});