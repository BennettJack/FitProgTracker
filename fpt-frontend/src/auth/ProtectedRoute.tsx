import { Navigate } from "react-router-dom";
import { keycloak } from "./keycloak";
import React from "react";

type Props = {
  children: React.ReactNode;
};

export default function ProtectedRoute({ children }: Props) {
  if (!keycloak.authenticated) {
    return <Navigate to="/login" replace />;
  }

  return <>{children}</>;
}
