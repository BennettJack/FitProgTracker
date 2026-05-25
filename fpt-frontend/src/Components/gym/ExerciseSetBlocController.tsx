import { ExerciseSetBloc } from "../../Types/WorkoutTypes";
import { useWorkoutProgrammeContext } from "../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeContext";

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
  } = useWorkoutProgrammeContext();

  const setType = (type: number) => {
    if (selectedSessionId === null) return;
    const setBlocId = setBloc.id ?? setBloc.tempId;
    if (!setBlocId) return;

    switch (type) {
      case 1:
        updateSetBloc(selectedSessionId, setBlocId, (prev) => ({
          ...prev,
          type: type,
        }));
        break;
      case 2:
        updateSetBloc(selectedSessionId, setBlocId, (prev) => ({
          ...prev,
          type: type,
          sets: [],
        }));
        break;
      case 3:
        updateSetBloc(selectedSessionId, setBlocId, (prev) => ({
          ...prev,
          type: type,
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
        value={setBloc.type}
        onChange={(event) => setType(Number(event.target.value))}
      >
        {exerciseTypeOptions.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
      <button disabled={setBloc.type !== 1} hidden={setBloc.type !== 1}>
        {" "}
        Add Set{" "}
      </button>
    </>
  );
}
