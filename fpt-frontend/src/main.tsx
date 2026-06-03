import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import "./Global styles/Global colours.css";
import reportWebVitals from "./reportWebVitals";
import { RouterProvider } from "react-router-dom";
import { router } from "./Routes";

import { initKeycloak } from "./auth/keycloak";
import { startTokenRefresh } from "./auth/tokenManager";

async function bootstrap() {
  const authenticated = await initKeycloak();

  if (authenticated) {
    startTokenRefresh();
  }

  ReactDOM.createRoot(document.getElementById("root")!).render(
    <React.StrictMode>
      <RouterProvider router={router} />
    </React.StrictMode>,
  );
}

bootstrap();

reportWebVitals();
