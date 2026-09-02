import { api } from "../../../api/apiClient";
import {
  ExerciseSetRecord,
  WorkoutProgramme,
} from "../../../schemas/workoutProgrammeSchema";
import { SelectOption } from "../../../Components/CustomElements/MultiSelect/Select";

export const getExerciseTypeOptions = async () => {
  try {
    const response = await api.get<SelectOption[]>(
      "/api/ExerciseType/GetExerciseTypes",
    );
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

export const getExercises = async () => {
  try {
    const response = await api.get<SelectOption[]>(
      "/api/Exercise/GetExercises",
    );
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

export const getWorkoutProgramme = async (programmeId: number) => {
  console.log("getting programme");
  try {
    const response = await api.get<WorkoutProgramme>(
      `/api/WorkoutProgramme/GetWorkoutProgramme/${programmeId}`,
    );
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

export const getExerciseTypesByExerciseId = async (exerciseId: number) => {
  try {
    const response = await api.get<SelectOption[]>(
      `/api/ExerciseType/GetExerciseTypesByExercise/${exerciseId}`,
    );
    return response.data;
  } catch (error) {}
};

export const getTodaysRecordsBySession = async (
  sessionId: number,
): Promise<Record<number, ExerciseSetRecord>> => {
  try {
    const response = await api.get<{
      setRecords: Record<number, ExerciseSetRecord>;
    }>(`/api/SetRecord/GetTodaysRecords/${sessionId}`);
    return response.data.setRecords || {};
  } catch (error) {
    console.log(error);
  }
  return {};
};

export const getMostRecentRecords = async (exerciseIds: number[]) => {
  try {
    const response = await api.post<{
      setRecords: Record<number, ExerciseSetRecord>;
    }>(`/api/SetRecord/GetMostRecentRecords/`, exerciseIds);
    return response.data.setRecords || {};
  } catch (error) {}
  return {};
};
