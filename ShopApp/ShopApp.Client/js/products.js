export async function render(container) {
    container.innerHTML = `<h2>Каталог товаров</h2><div id="product-list">Загрузка...</div>`;

    try {
        const products = await api("product");
        const list = document.getElementById("product-list");

        if (products.length === 0) {
            list.innerHTML = "<p>Товаров пока нет.</p>";
            return;
        }

        list.innerHTML = "";
        products.forEach(product => {
            const item = document.createElement("div");
            item.className = "product-card";
            item.innerHTML = `
                <div>
                    <h3>${product.name}</h3>
                    <p>${product.description ?? ""}</p>
                    <p><strong>${product.price.toFixed(2)} ₽</strong></p>
                    <p>В наличии: ${product.stock}</p>
                </div>
                <div>
                    <input type="number" min="1" max="${product.stock}" value="1" id="qty-${product.id}" />
                    <button onclick="addToCart(${product.id})">В корзину</button>
                </div>
            `;
            list.appendChild(item);
        });
    } catch (err) {
        container.innerHTML = `<p style="color: red;">Ошибка загрузки товаров: ${err.message}</p>`;
    }
}

// Глобальная функция — вызывает api для добавления в корзину
window.addToCart = async function (productId) {
    const input = document.getElementById(`qty-${productId}`);
    const quantity = parseInt(input.value);

    try {
        await api("cart", "POST", { productId, quantity });
        alert("Товар добавлен в корзину!");
    } catch (err) {
        alert("Ошибка при добавлении: " + err.message);
    }
};
