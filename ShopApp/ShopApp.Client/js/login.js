export async function render(container) {
    container.innerHTML = `
        <h2>Вход в систему</h2>
        <form id="login-form">
            <label>Имя пользователя</label>
            <input type="text" name="username" required />
            <label>Пароль</label>
            <input type="password" name="password" required />
            <button type="submit">Войти</button>
            <p>Нет аккаунта? <a href="#/register">Зарегистрироваться</a></p>
        </form>
        <div id="login-error" style="color: red;"></div>
    `;

    document.getElementById("login-form").addEventListener("submit", async (e) => {
        e.preventDefault();
        const form = e.target;
        const data = {
            username: form.username.value,
            password: form.password.value
        };

        try {
            const res = await fetch("https://localhost:7054/api/auth/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(data)
            });

            if (!res.ok) throw new Error(await res.text());
            const result = await res.json();

            saveTokens(result.accessToken, result.refreshToken);
            location.hash = "#/products";
        } catch (err) {
            document.getElementById("login-error").innerText = err.message;
        }
    });
}
