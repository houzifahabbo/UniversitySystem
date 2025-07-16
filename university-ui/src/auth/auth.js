import { jwtDecode } from "jwt-decode";

const TOKEN_KEY = "token";

export const getToken = () => {
  return localStorage.getItem(TOKEN_KEY);
};

export const isLoggedIn = () => {
  const token = getToken();
  if (!token) return false;
  try {
    const decodedToken = jwtDecode(token);
    const currentTime = Date.now() / 1000;
    return decodedToken.exp && decodedToken.exp > currentTime;
  } catch (error) {
    return false;
  }
};

export const logout = () => {
  localStorage.removeItem(TOKEN_KEY);
  window.location.href = "/login";
};

export const getUserInfo = () => {
  const token = getToken();
  if (!token) return null;
  try {
    return jwtDecode(token);
  } catch (error) {
    return null;
  }
};
