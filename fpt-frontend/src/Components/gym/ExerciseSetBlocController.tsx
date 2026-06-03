import { ExerciseSet, ExerciseSetBloc } from "../../Types/WorkoutTypes";
import { useWorkoutProgrammeContext } from "../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";
import { v4 as uuidv4 } from "uuid";
import ExerciseSetController from "./ExerciseSetController";

type ExerciseSetBlocControllerProps = {
  setBloc: ExerciseSetBloc;
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
  const setBlocId = setBloc.id ?? setBloc.tempId;
  const addExerciseSet = () => {
    if (selectedSessionId === null) return;
    if (!setBlocId) return;

    const newExerciseSet: ExerciseSet = {
      description: "",
      tempId: uuidv4(),
      name: `Exercise ${(setBloc.sets.length ?? 0) + 1}`,
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
  return (
    <>
      <div>{setBloc.name}</div>
      <select
        value={setBloc.exerciseId}
        onChange={(e) => {
          setExercise(Number(e.target.value));
        }}
      >
        <option value="">Select Exercise</option>
        {exerciseOptions.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
      <select
        value={setBloc.exerciseTypeId}
        onChange={(event) => setType(Number(event.target.value))}
      >
        {exerciseTypeOptions.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
      {setBloc.exerciseTypeId === 3 && (
        <button>Check your 5/3/1 settings</button>
      )}
      {setBloc.exerciseTypeId === 1 && (
        <button
          onClick={() => {
            addExerciseSet();
          }}
        >
          Add Set
        </button>
      )}

      {setBloc.sets.length > 0 &&
        setBloc.sets.map((set) => (
          <ExerciseSetController exerciseSet={set} exerciseSetBloc={setBloc} />
        ))}
    </>
  );
}
