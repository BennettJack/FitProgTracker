import React from "react";
import styles from "../Styles/nav.module.css";
interface nav {
  username: string;
  userType: number;
}

const visitorNavItems = () => {
  return (
    <ul>
      <li>visitor link</li>
    </ul>
  );
};

const userNavItems = () => {
  return (
    <ul>
      <li>
        <a href={"/newWorkoutProgramme"}>Create Programme</a>
      </li>
      <li>
        <a href={"/myProgrammes"}>My Programmes</a>
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
export default function NavBar({
  username,
  userType,
}: nav): React.ReactElement {
  const renderNavLink = () => {
    switch (userType) {
      case 1:
        return visitorNavItems();
      case 2:
        return userNavItems();
      case 3:
        return adminNavItems();
    }
  };
  return (
    <>
      <div className={styles.navContainer}>
        <div className={styles.logo}>Logo</div>
        <div className={styles.links}>{renderNavLink()}</div>
        <div className={styles.userControls}>
          <a href={"#"}>Log in</a>
        </div>
      </div>
    </>
  );
}
