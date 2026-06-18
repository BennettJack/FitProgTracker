import { useWorkoutProgrammeContext } from "./WorkoutProgrammeContext";
import { use, useEffect } from "react";
import styles from "./WorkoutProgrammeController.module.css";
import ExerciseSessionController from "../../../Components/gym/ExerciseSessionController/ExerciseSessionController";
import { api } from "../../../api/apiClient";
import { WorkoutProgramme } from "../../../Types/WorkoutTypes";
import { Button, ThemeProvider } from "@mui/material";
import aminoTheme from "../../../Global styles/mui/aminoTheme";
import { useParams } from "react-router-dom";
import ExerciseSessionList from "../../../Components/gym/ExerciseSessionList/ExerciseSessionList";

export default function WorkoutProgrammeController() {
  const {
    workoutProgrammeData,
    addSession,
    isEditable,
    updateProgrammeField,
    selectedSession,
    setSelectedSessionId,
    updateProgramme,
    mode,
    setMode,
  } = useWorkoutProgrammeContext();

  const submitWorkout = async () => {
    try {
      const res = api.post(
        "https://localhost:7206/api/WorkoutProgramme/newWorkoutProgramme",
        workoutProgrammeData,
      );
    } catch (error) {
      console.log(error);
    }
  };

  const renderButtons = () => {
    console.log(mode);
    switch (mode) {
      case "edit":
        return (
          <div>
            <Button onClick={updateProgramme}>Update</Button>
            <Button onClick={() => setMode("view")}>Cancel</Button>
          </div>
        );
      case "view":
        return <Button onClick={() => setMode("edit")}>Edit</Button>;
      case "create":
        return <Button onClick={submitWorkout}>Submit</Button>;
    }
  };
  return (
    <ThemeProvider theme={aminoTheme}>
      <div className={styles.wrapper}>
        <div className={styles.programmeContainer}>
          <div className={styles.header}>
            {isEditable ? (
              <input
                type="text"
                value={workoutProgrammeData.name}
                onChange={(e) => updateProgrammeField("name", e.target.value)}
              />
            ) : (
              <p>{workoutProgrammeData.name}</p>
            )}
          </div>

          <div className={styles.content}>
            {selectedSession && <ExerciseSessionController />}
          </div>
        </div>
        <Button
          onClick={() => {
            submitWorkout();
          }}
        >
          Submit
        </Button>
        {renderButtons()}
        <div className={styles.exerciseSessionList}>
          <ExerciseSessionList />
        </div>
      </div>
    </ThemeProvider>
  );
}
