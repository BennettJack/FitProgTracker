import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import {
  Controller,
  useFieldArray,
  useForm,
  useFormContext,
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


  const { control, register, watch } = useFormContext<WorkoutProgramme>();
  const temp = `sessions.${sessionIndex}.setBlocs.${setBlocIndex}.sets.${exerciseSetIndex}`
  const renderExerciseSelect = () => {
    if (isEditable) {
      return (
        <select
          value={exerciseSet.exerciseId}
          {...register("temp.")}
          disabled={!isEditable}
        >
          <option value="">Select Exercise</option>
          {exerciseOptions.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
      );
    } else {
      if (exerciseSet.exerciseId) {
        return (
          <p>
            {
              exerciseOptions.find((o) => {
                return Number(o.value) === exerciseSet.exerciseId;
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
        <select
          value={exerciseSet.exerciseTypeId}
          {...register("exerciseTypeId")}
          disabled={!isEditable}
        >
          {exerciseTypeOptions.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
      );
    } else {
      if (exerciseSet.exerciseTypeId) {
        return (
          <p>
            {
              exerciseTypeOptions.find((o) => {
                return Number(o.value) === exerciseSet.exerciseTypeId;
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
      {exerciseSet.exerciseTypeId === 3 && (
        <button>Check your 5/3/1 settings</button>
      )}
      <NumberSpinner<ExerciseSetFormData>
        name={"repCeiling"}
        control={control}
        label={"Rep Ceiling"}
        id={"repCeilingInput"}
        min={0}
        max={99}
      />

      <NumberSpinner<ExerciseSetFormData>
        name={"repFloor"}
        control={control}
        label={"Rep Floor"}
        id={"repCeilingInput"}
        min={0}
        max={99}
      />

      <TextField
        label="Description"
        variant={"standard"}
        id={"descriptionInput"}
        type={"text"}
        value={exerciseSet.description}
        {...register("description")}
      />
    </>
  );
}
