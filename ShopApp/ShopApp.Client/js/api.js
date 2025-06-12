const API_BASE = "https://localhost:7054/api";

function getAccessToken() {
    return localStorage.getItem("accessToken");
}

function getRefreshToken() {
    return localStorage.getItem("refreshToken");
}

function saveTokens(access, refresh) {
    localStorage.setItem("accessToken", access);
    localStorage.setItem("refreshToken", refresh);
}

function clearTokens() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
}

function getUserIdFromToken(token) {
    if (!token) return null;
    try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        return payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
    } catch {
        return null;
    }
}

async function refreshTokens() {
    const refreshToken = getRefreshToken();
    const userId = getUserIdFromToken(getAccessToken());

    if (!refreshToken || !userId) return false;

    const res = await fetch(`${API_BASE}/auth/refresh-token`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ userId, refreshToken })
    });

    if (!res.ok) {
        clearTokens();
        return false;
    }

    const result = await res.json();
    saveTokens(result.accessToken, result.refreshToken);
    return true;
}

async function api(path, method = "GET", data = null) {
    const headers = { "Content-Type": "application/json" };
    const token = getAccessToken();
    if (token) headers["Authorization"] = "Bearer " + token;

    let res = await fetch(`${API_BASE}/${path}`, {
        method,
        headers,
        body: data ? JSON.stringify(data) : null
    });

    // Если токен протух — пробуем обновить и повторить запрос
    if (res.status === 401 && getRefreshToken()) {
        const refreshed = await refreshTokens();
        if (refreshed) {
            const newToken = getAccessToken();
            headers["Authorization"] = "Bearer " + newToken;

            res = await fetch(`${API_BASE}/${path}`, {
                method,
                headers,
                body: data ? JSON.stringify(data) : null
            });
        }
    }

    if (!res.ok) throw new Error(await res.text());
    return await res.json();
}

// Экспорт глобально
window.api = api;
window.getAccessToken = getAccessToken;
window.getRefreshToken = getRefreshToken;
window.saveTokens = saveTokens;
window.clearTokens = clearTokens;
window.getUserIdFromToken = getUserIdFromToken;
