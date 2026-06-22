import { ExerciseSet, ExerciseSetBloc } from "../../../Types/WorkoutTypes";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { v4 as uuidv4 } from "uuid";
import ExerciseSetController from "../ExerciseSetController/ExerciseSetController";
import ExerciseSetRecordController from "../ExerciseSetRecordController/ExerciseSetRecordController";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import styles from "./ExerciseSetBlocController.module.css";
import { Button, Select } from "@mui/material";

type ExerciseSetBlocControllerProps = {
  setBloc: ExerciseSetBloc;
};
type ExerciseSetBlocFormData = {
  name: string;
};
export default function ExerciseSetBlocController({
  setBloc,
}: ExerciseSetBlocControllerProps) {
  const {
    updateSetBloc,
    exerciseOptions,
    exerciseTypeOptions,
    selectedSessionId,
    isEditable,
  } = useWorkoutProgrammeContext();

  const { register, watch, reset } = useForm<ExerciseSetBlocFormData>({
    defaultValues: {
      name: setBloc.name,
    },
  });

  const name = watch("name");

  const setBlocId = setBloc.id ?? setBloc.tempId;
  const addExerciseSet = () => {
    if (selectedSessionId === null) return;
    if (!setBlocId) return;

    const newExerciseSet: ExerciseSet = {
      description: "",
      tempId: uuidv4(),
      repCeiling: "0",
      repFloor: "0",
      exerciseTypeId: 1,
    };

    updateSetBloc(selectedSessionId, setBlocId, (prev) => ({
      ...prev,
      sets: [...prev.sets, newExerciseSet],
    }));
  };

  const setType = (type: number) => {
    if (selectedSessionId === null) return;

    if (!setBlocId) return;

    switch (type) {
      case 1:
        updateSetBloc(selectedSessionId, setBlocId, (prev) => ({
          ...prev,
          exerciseTypeId: type,
        }));
        break;
      case 2:
        updateSetBloc(selectedSessionId, setBlocId, (prev) => ({
          ...prev,
          exerciseTypeId: type,
          sets: [],
        }));
        break;
      case 3:
        updateSetBloc(selectedSessionId, setBlocId, (prev) => ({
          ...prev,
          exerciseTypeId: type,
          sets: [],
        }));
        break;
      default:
        break;
    }
  };

  useEffect(() => {
    if (selectedSessionId === null || !setBlocId) return;

    updateSetBloc(selectedSessionId, setBlocId, (prev) => ({
      ...prev,
      name,
    }));
  }, [name]);

  // Reset form when setBloc changes
  useEffect(() => {
    reset({
      name: setBloc.name,
    });
  }, [setBloc.id || setBloc.tempId]);

  return (
    <div className={styles.container} key={setBloc.id ?? setBloc.tempId}>
      {!isEditable ? (
        <h2>{setBloc.name}</h2>
      ) : (
        <input type="text" {...register("name")} />
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
          <Button
            onClick={() => {
              addExerciseSet();
            }}
          >
            Add Set
          </Button>
          <Button
            onClick={() => {
              addExerciseSet();
            }}
          >
            Duplicate
          </Button>
        </div>
      )}
    </div>
  );
}
