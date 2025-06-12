export async function render(container) {
    const token = getAccessToken();
    const payload = JSON.parse(atob(token.split('.')[1]));
    const role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    if (role !== "Admin") {
        container.innerHTML = "<h2>Доступ запрещён</h2>";
        return;
    }

    container.innerHTML = `
        <h2>Добавить товар</h2>
        <form id="product-form">
            <label>Название</label>
            <input name="name" required />
            <label>Описание</label>
            <textarea name="description"></textarea>
            <label>Цена</label>
            <input name="price" type="number" step="0.01" required />
            <label>Остаток на складе</label>
            <input name="stock" type="number" required />
            <button type="submit">Добавить</button>
        </form>
        <div id="product-msg" style="margin-top:0.5rem;"></div>
    `;

    document.getElementById("product-form").addEventListener("submit", async (e) => {
        e.preventDefault();
        const data = Object.fromEntries(new FormData(e.target).entries());

        data.price = parseFloat(data.price);
        data.stock = parseInt(data.stock);

        try {
            await api("product", "POST", data);
            document.getElementById("product-msg").textContent = "Товар добавлен!";
            document.getElementById("product-msg").style.color = "lightgreen";
            e.target.reset();
        } catch (err) {
            document.getElementById("product-msg").textContent = "Ошибка: " + err.message;
            document.getElementById("product-msg").style.color = "red";
        }
    });
}
