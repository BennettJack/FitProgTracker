import React from "react";
import styles from "./app.module.css";
import { Outlet } from "react-router";
import NavBar from "./Components/nav";

function App() {
  return (
    <div className={styles.appWrapper}>
      <NavBar username={"test user"} userType={2} />
      <Outlet />
    </div>
  );
}

export default App;
