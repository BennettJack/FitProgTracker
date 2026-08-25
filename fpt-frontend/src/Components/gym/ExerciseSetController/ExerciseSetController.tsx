import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { FormProvider, useForm } from "react-hook-form";
import { useEffect, useState } from "react";

import styles from "../ExerciseSetBlocController/ExerciseSetBlocController.module.css";
import ExerciseSetRecordController from "../ExerciseSetRecordController/ExerciseSetRecordController";
import {
  createExerciseSetRecord,
  ExerciseSet,
  ExerciseSetRecord,
  ExerciseSetRecordSchema,
} from "../../../schemas/workoutProgrammeSchema";
import { RhfSelect } from "../../Inputs/Select";
import { RhfTextField } from "../../Inputs/TextField";
import { SelectOption } from "../../CustomElements/MultiSelect/Select";
import { getExerciseTypesByExerciseId } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeApiCalls";
import { zodResolver } from "@hookform/resolvers/zod";

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
  const [exerciseTypeList, setExerciseTypeList] = useState<SelectOption[]>([]);
  useEffect(() => {
    if (exerciseSet.exerciseId)
      getExerciseTypesByExerciseId(Number(exerciseSet.exerciseId)).then((res) =>
        setExerciseTypeList(res ?? []),
      );
  }, [exerciseSet.exerciseId]);
  const { mode, isEditable, exerciseTypeOptions, exerciseOptions } =
    useWorkoutProgrammeContext();
  const temp = `sessions.${sessionIndex}.setBlocs.${setBlocIndex}.sets.${exerciseSetIndex}`;
  const renderExerciseSelect = () => {
    if (isEditable) {
      return (
        <RhfSelect
          variant={"outlined"}
          options={exerciseOptions}
          name={`${temp}.exerciseId`}
          value={exerciseSet.exerciseId}
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
          options={exerciseTypeList}
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

  const renderReps = () => {
    if (isEditable) {
      return (
        <div>
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
        </div>
      );
    } else {
      return (
        <div>
          <h4>Rep range</h4>
          <p>{`${exerciseSet.repFloor} - ${exerciseSet.repCeiling}`}</p>
        </div>
      );
    }
  };
  const methods = useForm<ExerciseSetRecord>({
    resolver: zodResolver(ExerciseSetRecordSchema),
    defaultValues: createExerciseSetRecord({
      exerciseId: Number(exerciseSet.exerciseId),
      exerciseTypeId: Number(exerciseSet.exerciseTypeId),
      exerciseSetId: String(exerciseSet.id) ?? exerciseSet.tempId,
    }),
  });
  return (
    <>
      <div className={styles.header}>
        {renderExerciseSelect()}
        {renderExerciseTypeSelect()}
      </div>
      {Number(exerciseSet.exerciseTypeId) === 3 && (
        <button>Check your 5/3/1 settings</button>
      )}
      {renderReps()}
      {mode === "view" && (
        <FormProvider {...methods}>
          <ExerciseSetRecordController exerciseSet={exerciseSet} />
        </FormProvider>
      )}
    </>
  );
}
