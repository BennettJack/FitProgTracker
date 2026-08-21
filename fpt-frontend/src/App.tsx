import React from "react";
import styles from "./app.module.css";
import { Outlet } from "react-router";
import NavBar from "./Components/Nav/Nav";
import { ThemeProvider } from "@mui/material";
import aminoTheme from "./Global styles/mui/aminoTheme";

function App() {
  return (
    <ThemeProvider theme={aminoTheme}>
      <div className={styles.appWrapper}>
        <NavBar />
        <Outlet />
      </div>
    </ThemeProvider>
  );
}

export default App;
