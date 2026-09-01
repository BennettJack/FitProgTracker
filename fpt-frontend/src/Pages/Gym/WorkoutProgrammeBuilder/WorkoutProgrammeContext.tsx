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
type SelectedSession = {
  index: number;
  session: Session;
};

type WorkoutProgrammeContextValue = {
  //State management
  mode: ControllerMode;
  setMode: React.Dispatch<React.SetStateAction<ControllerMode>>;
  isEditable: boolean;

  setSelectedSession: React.Dispatch<
    React.SetStateAction<SelectedSession | null>
  >;
  selectedSession: SelectedSession | null;

  loading: boolean;
  saving: boolean;

  createProgramme: () => Promise<void>;
  updateProgramme: () => Promise<void>;

  //Data from API
  exerciseTypeOptions: SelectOption[];
  exerciseOptions: SelectOption[];

  //records
  todaySessionRecords: Record<number, ExerciseSetRecord>;

  reset: () => void;
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
  const params = useParams<{ workoutProgrammeId: string }>();

  // state management
  const [currentMode, setCurrentMode] = useState<ControllerMode>(mode);
  const isEditable = currentMode === "edit" || currentMode === "create";
  const [loading, setLoading] = useState(false);
  const [saving, setSaving] = useState(false);
  const [selectedSession, setSelectedSession] =
    useState<SelectedSession | null>(null);
  //Data
  const [exerciseTypeOptions, setExerciseTypeOptions] = useState<
    SelectOption[]
  >([]);
  const [exerciseOptions, setExerciseOptions] = useState<SelectOption[]>([]);
  const [todaySessionRecords, setTodaySessionRecords] = useState<
    Record<number, ExerciseSetRecord>
  >({});

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
          await apiCalls.getExerciseTypeOptions(),
          await apiCalls.getExercises(),
        ]);
        setExerciseTypeOptions(exerciseTypeOptions ?? []);
        setExerciseOptions(exerciseOptions ?? []);

        console.log(
          `this should be getting the with id ${params.workoutProgrammeId}`,
        );
        //Fetch workout programme data
        if (workoutProgrammeId || params.workoutProgrammeId) {
          await apiCalls
            .getWorkoutProgramme(
              Number(params.workoutProgrammeId) ?? workoutProgrammeId,
            )
            .then((res) => {
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

  const updateProgramme = methods.handleSubmit(
    async (data) => {
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
      } finally {
        setSaving(false);
      }
    },
    (errors) => {
      console.error("Validation errors:", errors);
    },
  );

  const resetForm = async () => {
    setSelectedSession(null);
    await apiCalls
      .getWorkoutProgramme(
        Number(params.workoutProgrammeId) ?? workoutProgrammeId,
      )
      .then((res) => {
        reset(res);
      });
  };

  //the debugging useEffect
  useEffect(() => {
    console.log(watch());
  }, [watch()]);

  useEffect(() => {
    if (selectedSession?.session.id && mode === "view") {
      console.log("fetching records");
      apiCalls
        .getTodaysRecordsBySession(selectedSession.session.id)
        .then((res) => {
          setTodaySessionRecords(res);
          console.log(res);
        });
    }
  }, [selectedSession]);

  const value = useMemo<WorkoutProgrammeContextValue>(
    () => ({
      mode: currentMode,
      setMode: setCurrentMode,
      isEditable,

      exerciseTypeOptions,
      exerciseOptions,

      setSelectedSession,
      selectedSession,

      todaySessionRecords: todaySessionRecords,

      loading,
      saving,

      createProgramme,
      updateProgramme,

      reset: resetForm,
    }),
    [
      currentMode,
      isCreateMode,
      isEditMode,
      isViewMode,
      isEditable,
      exerciseOptions,
      exerciseTypeOptions,
      setSelectedSession,
      selectedSession,
      loading,
      saving,
      createProgramme,
      updateProgramme,
      reset,
    ],
  );

  return (
    <WorkoutProgrammeContext.Provider value={value}>
      <FormProvider {...methods}>{children}</FormProvider>
    </WorkoutProgrammeContext.Provider>
  );
}
