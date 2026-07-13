import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { v4 as uuidv4 } from "uuid";
import ExerciseSetController from "../ExerciseSetController/ExerciseSetController";
import ExerciseSetRecordController from "../ExerciseSetRecordController/ExerciseSetRecordController";
import { useEffect } from "react";
import { useFieldArray, useForm, useFormContext } from "react-hook-form";
import styles from "./ExerciseSetBlocController.module.css";
import { Button, Select } from "@mui/material";
import {
  ExerciseSetBloc,
  WorkoutProgramme,
} from "../../../schemas/workoutProgrammeSchema";
import { RhfTextField } from "../../../Global styles/mui/ControlledComponents/TextField";

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
          name={`sessions.${sessionIndex}.setBlocs.${setBlocIndex}.name`}
          variant="outlined"
          label="Name"
          defaultValue={setBloc.name}
        />
      )}
      {setBloc.sets.length > 0 &&
        setBloc.sets.map((set) =>
          isEditable ? (
            <ExerciseSetController
              exerciseSet={set}
              exerciseSetBloc={setBloc}
            />
          ) : (
            <ExerciseSetRecordController exerciseSet={set} />
          ),
        )}

      {isEditable && (
        <div>
          <Button onClick={() => console.log()}>Add Set</Button>
          <Button onClick={() => console.log()}>Duplicate</Button>
        </div>
      )}
    </div>
  );
}
