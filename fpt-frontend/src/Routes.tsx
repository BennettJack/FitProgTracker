import LoginSignup from "./Pages/Account/LoginSignup";
import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import AccessTests from "./Pages/Testing/AccessTests";
import { WorkoutProgrammeController } from "./Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeController";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "loginSignup", element: <LoginSignup /> },
      { path: "testAccess", element: <AccessTests /> },
      {
        path: "newWorkoutProgramme",
        element: <WorkoutProgrammeController mode={"create"} />,
      },
    ],
  },
]);
