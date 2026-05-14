import Keycloak from "keycloak-js";

export const keycloak = new (Keycloak as any)({
  url: "http://localhost:8080",
  realm: "BennettjApps",
  clientId: "amino-frontend",
});

let initPromise: Promise<boolean> | null = null;

export function initKeycloak() {
  if (!initPromise) {
    initPromise = keycloak.init({
      onLoad: "check-sso",
      pkceMethod: "S256",
      checkLoginIframe: false,
    });
  }

  return initPromise;
}

export function login() {
  return keycloak.login({
    redirectUri: window.location.origin,
  });
}

export function logout() {
  return keycloak.logout({
    redirectUri: window.location.origin,
  });
}
