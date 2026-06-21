import { ExerciseSetBloc, Session } from "../../../Types/WorkoutTypes";
import styles from "./ExerciseSessionController.module.css";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { v4 as uuidv4 } from "uuid";
import ExerciseSetBlocController from "../ExerciseSetBlocController/ExerciseSetBlocController";
import { Button } from "@mui/material";

export default function ExerciseSessionController() {
  const {
    selectedSession,
    updateSession,
    isEditable,
    exerciseTypeOptions,
    updateSetBloc,
  } = useWorkoutProgrammeContext();

  const addSetBloc = () => {
    if (!isEditable) return;
    if (selectedSession === null) return;

    const newSetBloc: ExerciseSetBloc = {
      tempId: uuidv4(),
      name: `Exercise ${(selectedSession.setBlocs.length ?? 0) + 1}`,
      sets: [],
      exerciseTypeId: 0,
    };

    const sessionId = selectedSession.id ?? selectedSession.tempId;

    if (!sessionId) return;

    updateSession(sessionId, (prev) => ({
      ...prev,
      setBlocs: [...prev.setBlocs, newSetBloc],
    }));
  };

  return (
    <div className={styles.container}>
      {selectedSession?.setBlocs.map((setBloc) => (
        <ExerciseSetBlocController
          key={setBloc.id ?? setBloc.tempId}
          setBloc={setBloc}
        />
      ))}
      {isEditable && (
        <Button variant={"contained"} onClick={() => addSetBloc()}>
          Add Exercise
        </Button>
      )}
    </div>
  );
}
