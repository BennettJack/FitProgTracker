import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import "./Global styles/Global colours.css";
import reportWebVitals from "./reportWebVitals";
import { RouterProvider } from "react-router-dom";
import { router } from "./Routes";
import { keycloak, initOptions } from "./keycloak.ts";
import { ReactKeycloakProvider } from "@react-keycloak/web";

ReactDOM.createRoot(document.getElementById('root')!).render(
  // Note: no <React.StrictMode> wrapper here
  <ReactKeycloakProvider authClient={keycloak} initOptions={initOptions}>
    <RouterProvider router={router} />
  </ReactKeycloakProvider>
);
// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
