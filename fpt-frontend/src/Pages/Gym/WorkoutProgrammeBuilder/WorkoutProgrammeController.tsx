import { useWorkoutProgrammeContext } from "./WorkoutProgrammeContext";
import styles from "./WorkoutProgrammeController.module.css";
import ExerciseSessionController from "../../../Components/gym/ExerciseSessionController/ExerciseSessionController";
import { api } from "../../../api/apiClient";
import { Button, ThemeProvider } from "@mui/material";
import aminoTheme from "../../../Global styles/mui/aminoTheme";
import ExerciseSessionList from "../../../Components/gym/ExerciseSessionList/ExerciseSessionList";
import { TextField } from "../../../Global styles/mui/ControlledFields/TextField";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import * as z from "zod";

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
      <div className={styles.programmeContainer}>
        <div className={styles.header}>
          {isEditable ? (
            <div>
              <TextField
                label="Name"
                value={workoutProgrammeData.name}
                variant="outlined"
                helperText={
                  workoutProgrammeData.name.length === 0
                    ? "Name is required"
                    : ""
                }
                onChange={(e) => updateProgrammeField("name", e.target.value)}
                tooltip={{
                  title: "Enter a name for your workout programme",
                  children: <InfoOutlinedIcon fontSize="small" />,
                }}
              ></TextField>
            </div>
          ) : (
            <h1>{workoutProgrammeData.name}</h1>
          )}
        </div>

        <div className={styles.content}>
          {selectedSession && <ExerciseSessionController />}
        </div>

        {renderButtons()}
      </div>
      <div className={styles.exerciseSessionList}>
        <ExerciseSessionList />
      </div>
    </ThemeProvider>
  );
}
