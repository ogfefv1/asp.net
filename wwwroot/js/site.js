cclass Base64 {
    static #textEncoder = new TextEncoder();
    static #textDecoder = new TextDecoder();

    // https://datatracker.ietf.org/doc/html/rfc4648#section-4
    static encode = (str) => btoa(String.fromCharCode(...Base64.#textEncoder.encode(str)));
    static decode = (str) => Base64.#textDecoder.decode(Uint8Array.from(atob(str), c => c.charCodeAt(0)));

    // https://datatracker.ietf.org/doc/html/rfc4648#section-5
    static encodeUrl = (str) => this.encode(str).replace(/\+/g, '-').replace(/\//g, '_').replace(/=+$/, '');
    static decodeUrl = (str) => this.decode(str.replace(/\-/g, '+').replace(/\_/g, '/'));

    static jwtEncodeBody = (header, payload) => this.encodeUrl(JSON.stringify(header)) + '.' + this.encodeUrl(JSON.stringify(payload));
    static jwtDecodePayload = (jwt) => JSON.parse(this.decodeUrl(jwt.split('.')[1]));
}

document.addEventListener('submit', e => {
    const form = e.target;
    if (form.id == 'auth-form') {
        e.preventDefault();

        const alertBox = document.getElementById("auth-alert");

        const showError = (message) => {
            alertBox.textContent = message;
            alertBox.classList.remove("d-none");
        };

        if (alertBox) alertBox.classList.add("d-none");

        const formData = new FormData(form);
        const login = formData.get("user-login");
        const password = formData.get("user-password");

        if (login.length == 0 || password.length == 0) {
            showError("Заповніть усі поля");
            return;
        }
 
        if (login.includes(':')) {
            showError("У логіні символ ':' не допускається");
            return;
        }

        const userPass = login + ':' + password;
        const basicCredentials = Base64.encode(userPass);

        fetch("/User/SignIn", {
            headers: {
                "Authorization": `Basic ${basicCredentials}`
            }
        }).then(r => r.json()).then(j => {
            if (j.status != 200) {
                showError(j.data || "У вході відмовлено");
            }
            else {
                console.log(j);
                window.location.reload();
            }
        }).catch(err => {
            console.error(err);
            showError("Помилка з'єднання із сервером");
        });
    }
});
/*
Д.З. Реалізувати відображення помилок (повідомлень) автентифікації
у складі модального вікна
Рекомендація: використати Bootstrap-Alerts https://getbootstrap.com/docs/5.3/components/alerts/
у лівій нижній частині діалогу (ліворуч від кнопок)
*/