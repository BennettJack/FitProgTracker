import axios from "axios";
import { keycloak } from "../auth/keycloak";

export const api = axios.create({
  baseURL: "https://localhost:7206",
});

api.interceptors.request.use(async (config) => {
  // Ensure token is valid before every request
  await keycloak.updateToken(60);

  const token = keycloak.token;

  if (token) {
    config.headers = config.headers ?? {};
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});
