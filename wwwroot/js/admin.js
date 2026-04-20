document.addEventListener('DOMContentLoaded', () => {
    const adminDiscountBtn = document.getElementById("admin-discount-btn");
    if (adminDiscountBtn) {
        adminDiscountBtn.addEventListener('click', adminDiscountClick);
    }
    else {
        console.error("#admin-discount-btn not found");
    }
    const adminDiscountDetailBtn = document.getElementById("discount-detail-btn");
    if (adminDiscountDetailBtn) {
        adminDiscountDetailBtn.addEventListener('click', adminDiscountDetailClick);
    }
    else {
        console.error("#discount-detail-btn not found");
    }
});
function adminDiscountDetailClick(e) {
    const form = e.target.closest("form");
    if (!form) throw "adminDiscountDetailClick: Closest form not found";
    const formData = new FormData(form);

    fetch("/Shop/DiscountDetailFormReceiver", {
        method: "POST",
        body: formData
    }).then(r => r.json()).then(j => {
        if (typeof j.status != 'undefined' && j.status == 'OK') {
            window.location.reload();
        }
        else {
   
            console.log("Помилка від сервера:", j); 

            const errorBox = document.getElementById("product-validation-error");

            if (errorBox) {
                if (j.ProductId && j.ProductId.errors && j.ProductId.errors.length > 0) {
                    errorBox.innerText = j.ProductId.errors[0].errorMessage;
                } else {
                    errorBox.innerText = "Щось пішло не так при додаванні.";
                }
            }
        }
    });
}

function adminDiscountClick(e) {
    const form = e.target.closest("form");
    if (!form) throw "adminDiscountClick: Closest form not found";
    const formData = new FormData(form);
    fetch("/Shop/DiscountFormReceiver", {
        method: "POST",
        body: formData
    }).then(r => r.json()).then(j => {
        if (typeof j.status != 'undefined' && j.status == 'OK') {
            window.location.reload();
        }
        else console.log(j);
    });
}