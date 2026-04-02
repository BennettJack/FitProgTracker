import Keycloak from "keycloak-js";

export const keycloak = new (Keycloak as any)({
  url: "http://localhost:8080",
  realm: "BennettApps",
  clientId: "amino-frontend",
});

export const initOptions = {
  onLoad: "check-sso",
  flow: "standard",
  pkceMethod: "S256",
  silentCheckSsoRedirectUri: `${window.location.origin}/silent-check-sso.html`,
  checkLoginIframe: true,
  checkLoginIframeInterval: 30,
  enableLogging: true,
};
