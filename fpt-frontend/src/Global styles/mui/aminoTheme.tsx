import { createTheme } from "@mui/material";

const aminoTheme = createTheme({
  palette: {
    mode: "dark",
    background: {
      default: "#18181b",
      paper: "#27272a",
    },
    text: {
      primary: `#f5f5f5`,
      secondary: "#a1a1aa",
      disabled: "#52525b",
    },
    primary: {
      main: "#7c3aed",
      contrastText: "#f5f5f5",
    },
    secondary: {
      main: "#34d399",
      contrastText: "#18181b",
    },
    info: {
      main: "#38bdf8",
      contrastText: "#18181b",
    },
    success: {
      main: "#22c55e",
      dark: "#006926",
      contrastText: "#18181b",
    },
    warning: {
      main: "#facc15",
      contrastText: "#18181b",
    },
    error: {
      main: "#ef4444",
      contrastText: "#f5f5f5",
    },
    divider: "#3f3f46",
    action: {
      active: "#a1a1aa",
      hover: "#7c3aed",
      selected: "#323234",
      disabled: "#52525b",
      disabledBackground: "#1f1f23",
    },
  },
});

export default aminoTheme;
