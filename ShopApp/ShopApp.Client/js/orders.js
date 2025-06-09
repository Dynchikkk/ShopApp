export async function render(container) {
    container.innerHTML = `<h2>Мои заказы</h2><div id="order-list">Загрузка...</div>`;

    try {
        const orders = await api("order");
        const list = document.getElementById("order-list");

        if (orders.length === 0) {
            list.innerHTML = "<p>У вас ещё нет заказов.</p>";
            return;
        }

        orders.forEach(order => {
            const div = document.createElement("div");
            div.className = "product-card";
            const itemsHtml = order.items.map(i =>
                `<li>${i.productName} × ${i.quantity} = ${(i.priceAtPurchase * i.quantity).toFixed(2)} ₽</li>`
            ).join("");

            const total = order.items.reduce((sum, i) => sum + i.priceAtPurchase * i.quantity, 0);

            div.innerHTML = `
                <div>
                    <h3>Заказ №${order.id}</h3>
                    <p>Создан: ${new Date(order.createdAt).toLocaleString()}</p>
                    <p>Доставка: ${new Date(order.deliveryDate).toLocaleDateString()} — ${order.deliveryAddress}</p>
                    <ul>${itemsHtml}</ul>
                    <p><strong>Итого: ${total.toFixed(2)} ₽</strong></p>
                </div>
            `;

            list.appendChild(div);
        });

    } catch (err) {
        container.innerHTML = `<p style="color:red;">Ошибка загрузки заказов: ${err.message}</p>`;
    }
}
