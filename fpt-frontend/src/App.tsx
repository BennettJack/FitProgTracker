import React from "react";
import styles from "./app.module.css";
import { Outlet } from "react-router";
import NavBar from "./Components/Nav";

function App() {
  return (
    <div className={styles.appWrapper}>
      <NavBar />
      <Outlet />
    </div>
  );
}

export default App;
