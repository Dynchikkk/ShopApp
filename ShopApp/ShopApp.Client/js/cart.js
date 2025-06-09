export async function render(container) {
    container.innerHTML = `<h2>Корзина</h2><div id="cart-list">Загрузка...</div>`;

    try {
        const items = await api("cart");
        const list = document.getElementById("cart-list");

        if (items.length === 0) {
            list.innerHTML = "<p>Корзина пуста.</p>";
            return;
        }

        let total = 0;
        list.innerHTML = `<ul id="cart-items" style="list-style:none; padding:0;"></ul>`;
        const ul = document.getElementById("cart-items");

        items.forEach(item => {
            total += item.total;

            const li = document.createElement("li");
            li.className = "product-card";
            li.innerHTML = `
                <div>
                    <h3>${item.productName}</h3>
                    <p>Цена: ${item.price.toFixed(2)} ₽</p>
                    <p>Количество: ${item.quantity}</p>
                    <p><strong>Итого: ${item.total.toFixed(2)} ₽</strong></p>
                </div>
                <div>
                    <button onclick="removeFromCart(${item.id})">Удалить</button>
                </div>
            `;
            ul.appendChild(li);
        });

        list.innerHTML += `
            <h3>Общая сумма: ${total.toFixed(2)} ₽</h3>
            <form id="order-form">
                <label>Адрес доставки</label>
                <input type="text" name="address" required />
                <label>Дата доставки</label>
                <input type="date" name="date" required />
                <button type="submit">Оформить заказ</button>
            </form>
        `;

        document.getElementById("order-form").addEventListener("submit", async (e) => {
            e.preventDefault();
            const form = e.target;
            try {
                await api("order", "POST", {
                    deliveryAddress: form.address.value,
                    deliveryDate: form.date.value
                });
                alert("Заказ оформлен!");
                location.reload();
            } catch (err) {
                alert("Ошибка оформления заказа: " + err.message);
            }
        });

    } catch (err) {
        container.innerHTML = `<p style="color: red;">Ошибка загрузки корзины: ${err.message}</p>`;
    }
}

window.removeFromCart = async function (itemId) {
    try {
        await api(`cart/${itemId}`, "DELETE");
        location.reload();
    } catch (err) {
        alert("Не удалось удалить товар: " + err.message);
    }
};
