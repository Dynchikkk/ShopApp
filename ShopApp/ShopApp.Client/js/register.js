export async function render(container) {
    container.innerHTML = `
        <h2>Регистрация</h2>
        <form id="register-form">
            <label>Имя пользователя</label>
            <input type="text" name="username" required />
            <label>Пароль</label>
            <input type="password" name="password" required />
            <button type="submit">Зарегистрироваться</button>
            <p>Уже есть аккаунт? <a href="#/login">Войти</a></p>
        </form>
        <div id="register-error" style="color: red;"></div>
    `;

    document.getElementById("register-form").addEventListener("submit", async (e) => {
        e.preventDefault();
        const form = e.target;
        const data = {
            username: form.username.value,
            password: form.password.value
        };

        try {
            const res = await fetch("https://localhost:7054/api/auth/register", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(data)
            });

            if (!res.ok) throw new Error(await res.text());

            alert("Регистрация прошла успешно. Теперь войдите в систему.");
            location.hash = "#/login";
        } catch (err) {
            document.getElementById("register-error").innerText = err.message;
        }
    });
}
