import { ExerciseSet, ExerciseSetBloc } from "../../../Types/WorkoutTypes";
import { useWorkoutProgrammeContext } from "../../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { v4 as uuidv4 } from "uuid";
import ExerciseSetController from "../ExerciseSetController/ExerciseSetController";
import ExerciseSetRecordController from "../ExerciseSetRecordController/ExerciseSetRecordController";
import { useEffect } from "react";
import { useForm } from "react-hook-form";

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

  const setExercise = (exerciseId: number) => {
    if (selectedSessionId === null) return;
    const setBlocId = setBloc.id ?? setBloc.tempId;
    if (!setBlocId) return;
    updateSetBloc(selectedSessionId, setBlocId, (prev) => ({
      ...prev,
      exerciseId: exerciseId,
    }));
  };

  const renderExerciseSelect = () => {
    if (isEditable) {
      return (
        <select
          value={setBloc.exerciseId}
          onChange={(e) => {
            setExercise(Number(e.target.value));
          }}
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
      if (setBloc.exerciseId) {
        return (
          <p>
            {
              exerciseOptions.find((o) => {
                return Number(o.value) === setBloc.exerciseId;
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
          value={setBloc.exerciseTypeId}
          onChange={(event) => setType(Number(event.target.value))}
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
      if (setBloc.exerciseTypeId) {
        return (
          <p>
            {
              exerciseTypeOptions.find((o) => {
                return Number(o.value) === setBloc.exerciseTypeId;
              })?.label
            }
          </p>
        );
      } else {
        return <p>No exercise type has been set for this! oops...</p>;
      }
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
    <>
      <input type="text" {...register("name")} disabled={!isEditable} />
      {renderExerciseSelect()}
      {renderExerciseTypeSelect()}

      {setBloc.exerciseTypeId === 3 && (
        <button>Check your 5/3/1 settings</button>
      )}
      {isEditable && setBloc.exerciseTypeId === 1 && <p>this is editable!</p>}
      {isEditable && setBloc.exerciseTypeId === 1 && (
        <button
          onClick={() => {
            addExerciseSet();
          }}
        >
          Add Set
        </button>
      )}
      {setBloc.sets.length > 0 &&
        setBloc.sets.map((set) =>
          isEditable ? (
            <ExerciseSetController
              exerciseSet={set}
              exerciseSetBloc={setBloc}
            />
          ) : (
            <ExerciseSetRecordController />
          ),
        )}
    </>
  );
}
