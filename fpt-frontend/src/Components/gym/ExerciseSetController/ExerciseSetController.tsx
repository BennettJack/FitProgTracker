import { ExerciseSet, ExerciseSetBloc } from "../../../Types/WorkoutTypes";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { Controller, useForm } from "react-hook-form";
import { useEffect } from "react";
import { Button, Input, Select, TextField } from "@mui/material";
import { NumberField } from "@base-ui/react";
import NumberSpinner from "../../../Global styles/mui/NumberSpinner";
import styles from "../ExerciseSetBlocController/ExerciseSetBlocController.module.css";
import ExerciseSetRecordController from "../ExerciseSetRecordController/ExerciseSetRecordController";

type ExerciseSetProps = {
  exerciseSet: ExerciseSet;
  exerciseSetBloc: ExerciseSetBloc;
};

type ExerciseSetFormData = {
  repCeiling: string;
  repFloor: string;
  description: string;
  exerciseId: number;
  exerciseTypeId: number;
};

export default function ExerciseSetController({
  exerciseSet,
  exerciseSetBloc,
}: ExerciseSetProps) {
  const {
    updateExerciseSet,
    selectedSessionId,
    isEditable,
    exerciseTypeOptions,
    exerciseOptions,
  } = useWorkoutProgrammeContext();

  const { control, register, watch, reset } = useForm<ExerciseSetFormData>({
    defaultValues: {
      repCeiling: exerciseSet.repCeiling,
      repFloor: exerciseSet.repFloor,
      description: exerciseSet.description,
    },
  });

  const repCeiling = watch("repCeiling");
  const repFloor = watch("repFloor");
  const description = watch("description");
  const exerciseId = watch("exerciseId");
  const exerciseTypeId = watch("exerciseTypeId");

  useEffect(() => {
    const setBlocId = exerciseSetBloc.id ?? exerciseSetBloc.tempId;
    const exerciseSetId = exerciseSet.id ?? exerciseSet.tempId;

    if (exerciseSetId === undefined || setBlocId === undefined) return;
    console.log(repCeiling);
    updateExerciseSet(selectedSessionId, setBlocId, exerciseSetId, (prev) => ({
      ...prev,
      repCeiling,
      repFloor,
      description,
      exerciseId,
      exerciseTypeId,
    }));
  }, [repCeiling, repFloor, description, exerciseId, exerciseTypeId]);

  useEffect(() => {
    reset({
      repCeiling: exerciseSet.repCeiling,
      repFloor: exerciseSet.repFloor,
      description: exerciseSet.description,
    });
  }, [exerciseSet.tempId || exerciseSet.id]);

  useEffect(() => {
    console.log(exerciseSet);
  }, [exerciseId, exerciseTypeId]);
  const renderExerciseSelect = () => {
    if (isEditable) {
      return (
        <select
          value={exerciseSet.exerciseId}
          {...register("exerciseId")}
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
