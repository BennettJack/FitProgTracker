import React from "react";
import styles from "./nav.module.css";
import { keycloak, logout } from "../../auth/keycloak";

const userNavItems = () => {
  return (
    <ul>
      <li>
        <a href={"/newWorkoutProgramme"}>Create Programme</a>
      </li>
      <li>
        <a href={"/myWorkoutProgrammes"}>My Programmes</a>
      </li>
      <li>
        <a href={"/componentPlayground"}>Component Playground</a>
      </li>
    </ul>
  );
};

const adminNavItems = () => {
  return (
    <ul>
      <li>admin link</li>
    </ul>
  );
};
export default function NavBar(): React.ReactElement {
  const realmRoles = keycloak.tokenParsed?.realm_access?.roles ?? [];
  const renderNavItems = () => {
    if (realmRoles.includes("standardUser")) {
      return userNavItems();
    }
    if (realmRoles.includes("admin")) {
      return adminNavItems();
    }
  };
  return (
    <>
      <div className={styles.navContainer}>
        <div className={styles.logo}>Logo</div>
        <div className={styles.links}>{renderNavItems()}</div>
        <div className={styles.userControls}>
          <div className={styles.userControls}>
            {keycloak.authenticated ? (
              <a
                href="#"
                onClick={(e) => {
                  e.preventDefault();
                  logout();
                }}
              >
                Logout
              </a>
            ) : (
              <a
                href="#"
                onClick={(e) => {
                  e.preventDefault();
                  keycloak.login();
                }}
              >
                Log in
              </a>
            )}
          </div>
        </div>
      </div>
    </>
  );
}
