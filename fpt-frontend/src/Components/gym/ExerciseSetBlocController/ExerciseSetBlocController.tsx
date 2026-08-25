import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import ExerciseSetController from "../ExerciseSetController/ExerciseSetController";
import ExerciseSetRecordController from "../ExerciseSetRecordController/ExerciseSetRecordController";
import { useFieldArray, useFormContext } from "react-hook-form";
import styles from "./ExerciseSetBlocController.module.css";
import { Button, Select } from "@mui/material";
import {
  createEmptyExerciseSet,
  ExerciseSetBloc,
  WorkoutProgramme,
} from "../../../schemas/workoutProgrammeSchema";
import { RhfTextField } from "../../Inputs/TextField";

type ExerciseSetBlocControllerProps = {
  setBlocIndex: number;
  sessionIndex: number;
  setBloc: ExerciseSetBloc;
};

export default function ExerciseSetBlocController({
  setBloc,
  setBlocIndex,
  sessionIndex,
}: ExerciseSetBlocControllerProps) {
  const { isEditable, selectedSession } = useWorkoutProgrammeContext();
  const prefix = `sessions.${sessionIndex}.setBlocs.${setBlocIndex}`;
  const { control, watch } = useFormContext<WorkoutProgramme>();

  const { fields, append, remove } = useFieldArray({
    control,
    name: `sessions.${sessionIndex}.setBlocs.${setBlocIndex}.sets`,
  });

  return (
    <div className={styles.container} key={setBloc.id ?? setBloc.tempId}>
      {!isEditable ? (
        <h2>{setBloc.name}</h2>
      ) : (
        <RhfTextField
          name={`${prefix}.name`}
          variant="outlined"
          label="Set bloc name"
          defaultValue={setBloc.name}
        />
      )}
      {setBloc.sets.length > 0 &&
        setBloc.sets.map((set, index) => (
          <div>
            <h3>Set {index + 1}</h3>
            <ExerciseSetController
              exerciseSet={set}
              setBlocIndex={setBlocIndex}
              exerciseSetIndex={index}
              sessionIndex={sessionIndex}
            />
          </div>
        ))}

      {isEditable && (
        <div>
          <Button onClick={() => append(createEmptyExerciseSet())}>
            Add Set
          </Button>
          <Button onClick={() => console.log()}>Duplicate</Button>
        </div>
      )}
    </div>
  );
}
