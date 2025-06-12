const app = document.getElementById("app");
const nav = document.getElementById("nav-container");

const routes = {
    login: "login",
    register: "register",
    products: "products",
    cart: "cart",
    orders: "orders",
    profile: "profile",
    admin: "admin"
};

window.addEventListener("DOMContentLoaded", () => {
    renderRoute();
    renderHeader();
});

window.addEventListener("hashchange", () => {
    renderRoute();
    renderHeader();
});

async function renderRoute() {
    const route = location.hash.slice(2) || "products";
    const page = routes[route];

    if (!page) {
        app.innerHTML = "<h2>Страница не найдена</h2>";
        return;
    }

    try {
        const module = await import(`./${page}.js`);
        await module.render(app);
    } catch (e) {
        app.innerHTML = `<h2>Ошибка загрузки страницы: ${e.message}</h2>`;
    }
}

function renderHeader() {
    const token = getAccessToken();
    let userInfo = "Инкогнито";
    let isLoggedIn = false;
    let role = null;

    if (token) {
        try {
            const payload = JSON.parse(atob(token.split(".")[1]));
            const name = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
            role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            userInfo = role === "Admin" ? `[АДМИН] ${name}` : name;
            isLoggedIn = true;
        } catch (e) {
            console.warn("Ошибка разбора токена:", e);
        }
    }

    let html = `
        <a href="#/products">Каталог</a>
        <a href="#/cart">Корзина</a>
        <a href="#/orders">Заказы</a>
        <a href="#/profile">Профиль</a>
    `;

    if (role === "Admin") {
        html += `<a href="#/admin">Добавить товар</a>`;
    }

    html += isLoggedIn
        ? `<span style="margin-left:auto;">${userInfo}</span><a href="#" onclick="logout()">Выйти</a>`
        : `<a href="#/login" style="margin-left:auto;">Войти</a><a href="#/register">Регистрация</a>`;

    nav.innerHTML = html;
}

function logout() {
    clearTokens();
    location.hash = "#/login";
    renderHeader();
}
