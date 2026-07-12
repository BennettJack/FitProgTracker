import { api } from "../../../api/apiClient";
import { WorkoutProgramme } from "../../../schemas/workoutProgrammeSchema";
import { SelectOption } from "../../../Components/CustomElements/MultiSelect/Select";
import { Exercise } from "../../../Types/ModelTypes";
import { Select } from "@base-ui/react";

export const fetchTodayRecords = async (sessionId: number | string) => {
  try {
    const response = await api.get(`api/SetRecord/GetTodayRecords/`, {
      params: {
        sessionId: sessionId,
      },
    });
    return response.data;
  } catch (error) {
    console.error("Failed to fetch today's records", error);
    return {};
  }
};

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
  try {
    const response = await api.get<WorkoutProgramme>(
      `/api/WorkoutProgramme/GetWorkoutProgramme/${programmeId}`,
    );
    return response.data;
  } catch (error) {
    console.log(error);
  }
};
