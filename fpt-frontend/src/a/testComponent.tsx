import React from "react";
import { useKeycloak } from "@react-keycloak/web";

export default function UserMenu() {
  const { keycloak, initialized } = useKeycloak();

  // Wait until Keycloak finishes initialization (including silent SSO)
  if (!initialized) {
    return <div>Loading authentication…</div>;
  }

  // If not authenticated yet, show a Login button
  if (!keycloak.authenticated) {
    return <button onClick={() => keycloak.login()}>Login</button>;
  }

  // Once authenticated, extract user info from the parsed token
  const username = (keycloak.tokenParsed as any)?.preferred_username;
  const roles = keycloak.tokenParsed?.realm_access?.roles || [];

  const token = keycloak.tokenParsed as any;

  const clientRoles = token?.resource_access?.[keycloak.clientId!]?.roles || [];

  return (
    <div>
      <span>Hello, {username}</span>
      <span>Roles: {roles.join(", ")}</span>
      <span>Roles: {clientRoles.join(", ")}</span>
      <button onClick={() => keycloak.logout()}>Logout</button>
    </div>
  );
}
