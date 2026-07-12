import React, {
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
  type ReactNode,
} from "react";
import { useParams } from "react-router-dom";
import {
  createEmptyWorkoutProgramme,
  ExerciseSet,
  ExerciseSetBloc,
  ExerciseSetRecord,
  Session,
  WorkoutProgramme,
  WorkoutProgrammeSchema,
} from "../../../schemas/workoutProgrammeSchema";
import * as apiCalls from "./WorkoutProgrammeApiCalls";
import { SelectOption } from "../../../Components/CustomElements/MultiSelect/Select";
import { FormProvider, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { ControllerMode } from "../../../Types/WorkoutTypes";
import { api } from "../../../api/apiClient";

type WorkoutProgrammeControllerProps = {
  children: ReactNode;
  mode: ControllerMode;
  workoutProgrammeId?: number;
};

type WorkoutProgrammeContextValue = {
  //State management
  mode: ControllerMode;
  setMode: React.Dispatch<React.SetStateAction<ControllerMode>>;
  isEditable: boolean;

  selectedSessionId: number | string | null;
  setSelectedSessionId: React.Dispatch<
    React.SetStateAction<number | string | null>
  >;
  selectedSession: Session | null;

  loading: boolean;
  saving: boolean;

  createProgramme: () => Promise<void>;
  updateProgramme: () => Promise<void>;

  //Data from API
  exerciseTypeOptions: SelectOption[];
  exerciseOptions: SelectOption[];
};

const WorkoutProgrammeContext =
  createContext<WorkoutProgrammeContextValue | null>(null);

function getEntityId(entity: { id?: number; tempId?: string }) {
  return entity.id ?? entity.tempId;
}

export function useWorkoutProgrammeContext() {
  const context = useContext(WorkoutProgrammeContext);

  if (!context) {
    throw new Error(
      "useWorkoutProgramme must be used inside WorkoutProgrammeController",
    );
  }

  return context;
}

export function WorkoutProgrammeProvider({
  children,
  mode,
  workoutProgrammeId,
}: WorkoutProgrammeControllerProps): React.ReactElement {
  const params = useParams();

  // state management
  const [currentMode, setCurrentMode] = useState<ControllerMode>(mode);
  const isEditable = currentMode === "edit" || currentMode === "create";
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  //Data
  const [exerciseTypeOptions, setExerciseTypeOptions] = useState<
    SelectOption[]
  >([]);
  const [exerciseOptions, setExerciseOptions] = useState<SelectOption[]>([]);

  const [selectedSessionId, setSelectedSessionId] = useState<
    number | string | null
  >(null);

  const isCreateMode = currentMode === "create";
  const isEditMode = currentMode === "edit";
  const isViewMode = currentMode === "view";

  const methods = useForm<WorkoutProgramme>({
    resolver: zodResolver(WorkoutProgrammeSchema),
    defaultValues: createEmptyWorkoutProgramme(),
  });
  const { reset, watch, control } = methods;
  //region useEffects
  useEffect(() => {
    setLoading(true);
    const initialise = async () => {
      //get and set dropdown data
      try {
        const [exerciseTypeOptions, exerciseOptions] = await Promise.all([
          await apiCalls.getExercises(),
          await apiCalls.getExerciseTypeOptions(),
        ]);
        setExerciseTypeOptions(exerciseTypeOptions ?? []);
        setExerciseOptions(exerciseOptions ?? []);

        //Fetch workout programme data
        if (workoutProgrammeId) {
          await apiCalls.getWorkoutProgramme(workoutProgrammeId).then((res) => {
            reset(res);
          });
        }
      } finally {
        setLoading(false);
      }
    };
    initialise();
  }, []);
  useEffect(() => {
    setCurrentMode(mode);
  }, [mode]);

  const createProgramme = methods.handleSubmit(async (data) => {
    setSaving(true);
    try {
      const response = await api.post<WorkoutProgramme>(
        "/api/WorkoutProgramme/newWorkoutProgramme",
        data,
      );

      reset(response.data);
    } catch (error) {
      console.error(error);
    }
    setSaving(false);
  });

  const updateProgramme = methods.handleSubmit(async (data) => {
    setSaving(true);
    try {
      const response = await api.put<WorkoutProgramme>(
        `/api/WorkoutProgramme/updateWorkoutProgramme/${data.id}`,
        data,
      );

      reset(response.data);
    } catch (error) {
      console.error("Failed to update workout programme", error);
      return null;
    }
    setSaving(false);
  });

  const sessions = watch("sessions");
  const selectedSession =
    sessions?.find(
      (session) => (session.id ?? session.tempId) === selectedSessionId,
    ) ?? null;

  const value = useMemo<WorkoutProgrammeContextValue>(
    () => ({
      mode: currentMode,
      setMode: setCurrentMode,
      isEditable,

      exerciseTypeOptions,
      exerciseOptions,

      selectedSessionId,
      setSelectedSessionId,
      selectedSession,

      loading,
      saving,

      createProgramme,
      updateProgramme,
    }),
    [
      currentMode,
      isCreateMode,
      isEditMode,
      isViewMode,
      isEditable,
      exerciseOptions,
      exerciseTypeOptions,
      selectedSessionId,
      selectedSession,
      loading,
      saving,
      createProgramme,
      updateProgramme,
    ],
  );

  return (
    <WorkoutProgrammeContext.Provider value={value}>
      <FormProvider {...methods}>{children}</FormProvider>
    </WorkoutProgrammeContext.Provider>
  );
}
