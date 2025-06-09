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

async function api(path, method = "GET", data = null) {
    const headers = {
        "Content-Type": "application/json"
    };

    const token = getAccessToken();
    if (token) headers["Authorization"] = "Bearer " + token;

    const res = await fetch(`${API_BASE}/${path}`, {
        method,
        headers,
        body: data ? JSON.stringify(data) : null
    });

    if (res.status === 401 && getRefreshToken()) {
        const ok = await refreshTokens();
        if (ok) return api(path, method, data);
    }

    if (!res.ok) throw new Error(await res.text());
    return await res.json();
}

async function refreshTokens() {
    const refreshToken = getRefreshToken();
    const userId = parseInt(getUserIdFromToken(getAccessToken()));
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

// JWT decode (простая реализация)
function getUserIdFromToken(token) {
    if (!token) return null;
    try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        return payload["nameid"];
    } catch {
        return null;
    }
}

// Экспортируем как глобальные
window.api = api;
window.getAccessToken = getAccessToken;
window.getUserIdFromToken = getUserIdFromToken;
window.saveTokens = saveTokens;
window.clearTokens = clearTokens;
