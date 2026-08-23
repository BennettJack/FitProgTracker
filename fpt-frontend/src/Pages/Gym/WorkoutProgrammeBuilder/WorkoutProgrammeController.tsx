import { useWorkoutProgrammeContext } from "./WorkoutProgrammeContext";
import styles from "./WorkoutProgrammeController.module.css";
import ExerciseSessionController from "../../../Components/gym/ExerciseSessionController/ExerciseSessionController";
import { api } from "../../../api/apiClient";
import { Button, ThemeProvider } from "@mui/material";
import aminoTheme from "../../../Global styles/mui/aminoTheme";
import ExerciseSessionSidebar from "../../../Components/gym/ExerciseSessionList/ExerciseSessionSidebar";
import { RhfTextField, TextField } from "../../../Components/Inputs/TextField";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import * as z from "zod";
import { ReactNode, useEffect } from "react";
import { ControllerMode } from "../../../Types/WorkoutTypes";
import { useFormContext } from "react-hook-form";
import { WorkoutProgramme } from "../../../schemas/workoutProgrammeSchema";
import { useFormState } from "react-dom";
import WorkoutProgrammeList from "../../../Components/gym/WorkoutProgrammeList/WorkoutProgrammeList";
import ExerciseSessionList from "../../../Components/gym/ExerciseSessionList/ExerciseSessionList";

export default function WorkoutProgrammeController() {
  const {
    isEditable,
    selectedSession,

    updateProgramme,
    mode,
    setMode,
    createProgramme,
    reset,
  } = useWorkoutProgrammeContext();

  const { watch, getValues } = useFormContext<WorkoutProgramme>();

  useEffect(() => {
    console.log(getValues());
  }, [watch("name")]);

  const onCancel = () => {
    setMode("view");
    reset();
  };
  const renderButtons = () => {
    console.log(mode);
    switch (mode) {
      case "edit":
        return (
          <div>
            <Button onClick={updateProgramme}>Update</Button>
            <Button onClick={() => onCancel()}>Cancel</Button>
          </div>
        );
      case "view":
        return (
          <Button
            onClick={() => {
              setMode("edit");
            }}
          >
            Edit
          </Button>
        );
      case "create":
        return <Button onClick={createProgramme}>Submit</Button>;
    }
  };
  return (
    <ThemeProvider theme={aminoTheme}>
      <div className={styles.programmeContainer}>
        <div className={styles.header}>
          {isEditable ? (
            <div>
              <RhfTextField
                name="name"
                variant="outlined"
                label="Name"
                tooltip={{
                  title: "Enter a name for your workout programme",
                  children: <InfoOutlinedIcon fontSize="small" />,
                }}
              ></RhfTextField>
            </div>
          ) : (
            <h1>{watch("name")}</h1>
          )}
        </div>

        <div className={styles.content}>
          {selectedSession ? (
            <ExerciseSessionController
              sessionIndex={selectedSession.index}
              session={selectedSession.session}
            />
          ) : (
            <div>
              <p>Select a session</p>
              <ExerciseSessionList />
            </div>
          )}
        </div>

        {renderButtons()}
      </div>
      <div className={styles.exerciseSessionList}>
        <ExerciseSessionSidebar />
      </div>
    </ThemeProvider>
  );
}
