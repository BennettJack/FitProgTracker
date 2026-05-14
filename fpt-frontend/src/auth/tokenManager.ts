import { keycloak } from "./keycloak";

let refreshInterval: number | null = null;

export function startTokenRefresh() {
  if (refreshInterval) return;

  refreshInterval = window.setInterval(async () => {
    try {
      const refreshed = await keycloak.updateToken(60);

      if (refreshed) {
        console.log("Token refreshed");
      }
    } catch (err) {
      console.error("Token refresh failed", err);
      await keycloak.login();
    }
  }, 30000);
}
