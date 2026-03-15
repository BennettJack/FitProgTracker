import LoginSignup from "./Pages/Account/LoginSignup";
import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import AccessTests from "./Pages/Testing/AccessTests";
import { WorkoutProgrammeController } from "./Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeController";
import FiveThreeOneController from "./Components/Gym Components/fiveThreeOneController";
import ExerciseController from "./Components/ExerciseController";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "loginSignup", element: <LoginSignup /> },
      { path: "testAccess", element: <AccessTests /> },
      { path: "test531", element: <FiveThreeOneController /> },
      { path: "addExercise", element: <ExerciseController /> },
      {
        path: "newWorkoutProgramme",
        element: <WorkoutProgrammeController mode={"create"} />,
      },
    ],
  },
]);
