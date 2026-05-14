import LoginSignup from "./Pages/Account/LoginSignup";
import { createBrowserRouter } from "react-router-dom";
import App from "./App";
import AccessTests from "./Pages/Testing/AccessTests";
import { WorkoutProgrammeController } from "./Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeController";
import FiveThreeOneController from "./Components/gym/FiveThreeOneController";
import ExerciseController from "./Components/gym/ExerciseController";
import ProtectedRoute from "./auth/ProtectedRoute";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "login", element: <LoginSignup /> },

      {
        path: "testAccess",
        element: (
          <ProtectedRoute>
            <AccessTests />
          </ProtectedRoute>
        ),
      },
      {
        path: "test531",
        element: (
          <ProtectedRoute>
            <FiveThreeOneController />
          </ProtectedRoute>
        ),
      },
      {
        path: "addExercise",
        element: (
          <ProtectedRoute>
            <ExerciseController />
          </ProtectedRoute>
        ),
      },
      {
        path: "newWorkoutProgramme",
        element: (
          <ProtectedRoute>
            <WorkoutProgrammeController mode={"input"} />
          </ProtectedRoute>
        ),
      },
    ],
  },
]);
