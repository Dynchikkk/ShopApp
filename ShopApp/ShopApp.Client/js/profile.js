export async function render(container) {
    container.innerHTML = `<h2>Профиль</h2><div id="profile-area">Загрузка...</div>`;

    try {
        const profile = await api("profile");
        document.getElementById("profile-area").innerHTML = `
            <form id="profile-form">
                <label>ФИО</label>
                <input type="text" name="fullName" value="${profile.fullName ?? ""}" />
                <label>Адрес</label>
                <input type="text" name="address" value="${profile.address ?? ""}" />
                <label>Телефон</label>
                <input type="text" name="phone" value="${profile.phone ?? ""}" />
                <button type="submit">Сохранить</button>
            </form>
            <div id="profile-success" style="color: green;"></div>
        `;

        document.getElementById("profile-form").addEventListener("submit", async (e) => {
            e.preventDefault();
            const form = e.target;
            const updated = {
                fullName: form.fullName.value,
                address: form.address.value,
                phone: form.phone.value
            };

            try {
                await api("profile", "PUT", updated);
                document.getElementById("profile-success").innerText = "Профиль обновлён.";
            } catch (err) {
                alert("Ошибка обновления профиля: " + err.message);
            }
        });

    } catch (err) {
        container.innerHTML = `<p style="color:red;">Ошибка загрузки профиля: ${err.message}</p>`;
    }
}
