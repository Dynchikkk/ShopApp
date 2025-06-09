const app = document.getElementById("app");

window.addEventListener("hashchange", renderRoute);
window.addEventListener("DOMContentLoaded", renderRoute);

// Маршруты: путь → модуль
const routes = {
    login: "login",
    register: "register",
    products: "products",
    cart: "cart",
    orders: "orders",
    profile: "profile"
};

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

// Выход
document.getElementById("logout-link").addEventListener("click", (e) => {
    e.preventDefault();
    clearTokens();
    location.hash = "#/login";
});
