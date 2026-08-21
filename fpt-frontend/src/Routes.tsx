import LoginSignup from "./Pages/Account/LoginSignup";
import { createBrowserRouter, useParams } from "react-router-dom";
import App from "./App";
import AccessTests from "./Pages/Testing/AccessTests";
import FiveThreeOneController from "./Components/gym/FiveThreeOneController/FiveThreeOneController";
import ExerciseController from "./Components/gym/ExerciseController/ExerciseController";
import ProtectedRoute from "./auth/ProtectedRoute";
import { WorkoutProgrammeProvider } from "./Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import WorkoutProgrammeController from "./Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeController";
import WorkoutProgrammeList from "./Components/gym/WorkoutProgrammeList/WorkoutProgrammeList";
import { ControllerMode } from "./Types/WorkoutTypes";
import ComponentPlayground from "./Components/ComponentPlayground/ComponentPlayground";
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
            <WorkoutProgrammeProvider mode="create">
              <WorkoutProgrammeController />
            </WorkoutProgrammeProvider>
          </ProtectedRoute>
        ),
      },
      {
        path: "workoutProgramme/:workoutProgrammeId",
        element: (
          <ProtectedRoute>
            <WorkoutProgrammeProvider mode="view">
              <WorkoutProgrammeController />
            </WorkoutProgrammeProvider>
          </ProtectedRoute>
        ),
      },
      {
        path: "myWorkoutProgrammes",
        element: (
          <ProtectedRoute>
            <WorkoutProgrammeList />
          </ProtectedRoute>
        ),
      },
      {
        path: "componentPlayground",
        element: (
          <ProtectedRoute>
            <ComponentPlayground />
          </ProtectedRoute>
        ),
      },
    ],
  },
]);
