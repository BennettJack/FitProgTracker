import { ExerciseSet, ExerciseSetBloc } from "../../../Types/WorkoutTypes";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { Controller, useForm } from "react-hook-form";
import { useEffect } from "react";
import { Input, TextField } from "@mui/material";
import { NumberField } from "@base-ui/react";
import NumberSpinner from "../../../Global styles/mui/NumberSpinner";

type ExerciseSetProps = {
  exerciseSet: ExerciseSet;
  exerciseSetBloc: ExerciseSetBloc;
};

type ExerciseSetFormData = {
  repCeiling: string;
  repFloor: string;
  description: string;
};

export default function ExerciseSetController({
  exerciseSet,
  exerciseSetBloc,
}: ExerciseSetProps) {
  const { updateExerciseSet, selectedSessionId } = useWorkoutProgrammeContext();

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
    }));
  }, [repCeiling, repFloor, description]);

  useEffect(() => {
    reset({
      repCeiling: exerciseSet.repCeiling,
      repFloor: exerciseSet.repFloor,
      description: exerciseSet.description,
    });
  }, [exerciseSet.tempId || exerciseSet.id]);

  return (
    <>
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
