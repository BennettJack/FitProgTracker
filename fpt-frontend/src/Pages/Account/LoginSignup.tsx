import React from "react";
import { login, keycloak } from "../../auth/keycloak";
import { Navigate } from "react-router-dom";

export default function LoginSignup() {
  // If already authenticated, skip login page
  if (keycloak.authenticated) {
    return <Navigate to="/" replace />;
  }

  return (
    <div style={{ padding: 40 }}>
      <h1>Login</h1>

      <p>You must log in to continue.</p>

      <button
        onClick={() => login()}
        style={{
          padding: "10px 16px",
          fontSize: 16,
          cursor: "pointer",
        }}
      >
        Login with Keycloak
      </button>
    </div>
  );
}
