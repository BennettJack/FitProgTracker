import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import {
  Controller,
  useFieldArray,
  useForm,
  useFormContext,
  useWatch,
} from "react-hook-form";
import { useEffect } from "react";
import { Button, Input, Select, TextField } from "@mui/material";
import { NumberField } from "@base-ui/react";
import NumberSpinner from "../../../Global styles/mui/NumberSpinner";
import styles from "../ExerciseSetBlocController/ExerciseSetBlocController.module.css";
import ExerciseSetRecordController from "../ExerciseSetRecordController/ExerciseSetRecordController";
import {
  ExerciseSet,
  WorkoutProgramme,
} from "../../../schemas/workoutProgrammeSchema";
import { RhfSelect } from "../../Inputs/Select";
import { RhfTextField } from "../../Inputs/TextField";

type ExerciseSetProps = {
  exerciseSet: ExerciseSet;
  exerciseSetIndex: number;
  setBlocIndex: number;
  sessionIndex: number;
};

export default function ExerciseSetController({
  exerciseSet,
  exerciseSetIndex,
  setBlocIndex,
  sessionIndex,
}: ExerciseSetProps) {
  const { isEditable, exerciseTypeOptions, exerciseOptions } =
    useWorkoutProgrammeContext();
  const temp = `sessions.${sessionIndex}.setBlocs.${setBlocIndex}.sets.${exerciseSetIndex}`;
  const renderExerciseSelect = () => {
    if (isEditable) {
      return (
        <RhfSelect
          variant={"outlined"}
          options={exerciseOptions}
          name={`${temp}.exerciseId`}
          value={String(exerciseSet.exerciseId)}
          label={"Exercise"}
        />
      );
    } else {
      if (exerciseSet.exerciseId) {
        return (
          <p>
            {
              exerciseOptions.find((o) => {
                return o.value === exerciseSet.exerciseId;
              })?.label
            }
          </p>
        );
      } else {
        return <p>No exercise has been set for this! oops...</p>;
      }
    }
  };

  const renderExerciseTypeSelect = () => {
    if (isEditable) {
      return (
        <RhfSelect
          variant={"outlined"}
          options={exerciseTypeOptions}
          name={`${temp}.exerciseTypeId`}
          value={exerciseSet.exerciseTypeId}
          label={"Exercise type"}
        />
      );
    } else {
      if (exerciseSet.exerciseTypeId) {
        return (
          <p>
            {
              exerciseTypeOptions.find((o) => {
                return o.value === exerciseSet.exerciseTypeId;
              })?.label
            }
          </p>
        );
      } else {
        return <p>No exercise type has been set for this! oops...</p>;
      }
    }
  };

  return (
    <>
      <div className={styles.header}>
        {renderExerciseSelect()}
        {renderExerciseTypeSelect()}
      </div>
      {Number(exerciseSet.exerciseTypeId) === 3 && (
        <button>Check your 5/3/1 settings</button>
      )}
      <RhfTextField
        variant={"outlined"}
        name={`${temp}.repCeiling`}
        value={exerciseSet.repCeiling}
        type={"number"}
        label={"Rep ceiling"}
      />
      <RhfTextField
        variant={"outlined"}
        name={`${temp}.repFloor`}
        value={exerciseSet.repFloor}
        type={"number"}
        label={"Rep floor"}
      />
    </>
  );
}
