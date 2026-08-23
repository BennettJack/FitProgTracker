import styles from "./ExerciseSessionController.module.css";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { v4 as uuidv4 } from "uuid";
import ExerciseSetBlocController from "../ExerciseSetBlocController/ExerciseSetBlocController";
import { Button } from "@mui/material";
import { useFieldArray, useFormContext } from "react-hook-form";
import {
  createEmptyExerciseSetBloc,
  Session,
  WorkoutProgramme,
} from "../../../schemas/workoutProgrammeSchema";
import { RhfTextField } from "../../Inputs/TextField";

type Props = {
  sessionIndex: number;
  session: Session;
};
export default function ExerciseSessionController({
  sessionIndex,
  session,
}: Props) {
  const { selectedSession, isEditable, exerciseTypeOptions } =
    useWorkoutProgrammeContext();

  const { control, watch } = useFormContext<WorkoutProgramme>();
  const { fields, append, remove } = useFieldArray({
    control,
    name: `sessions.${sessionIndex}.setBlocs`,
  });

  return (
    <div className={styles.container}>
      {isEditable ? (
        <RhfTextField
          variant={"outlined"}
          value={session.name}
          name={`sessions.${sessionIndex}.name`}
          label={"Session name"}
        />
      ) : (
        <h2>{session.name}</h2>
      )}
      {session?.setBlocs.map((setBloc, index) => (
        <ExerciseSetBlocController
          key={setBloc.id ?? setBloc.tempId}
          sessionIndex={sessionIndex}
          setBlocIndex={index}
          setBloc={setBloc}
        />
      ))}
      {isEditable && (
        <Button
          variant={"contained"}
          onClick={() => append(createEmptyExerciseSetBloc())}
        >
          Add Exercise
        </Button>
      )}
    </div>
  );
}
