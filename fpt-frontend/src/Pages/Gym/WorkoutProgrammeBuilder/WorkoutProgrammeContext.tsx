import React, {
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
  type ReactNode,
} from "react";
import { useParams } from "react-router-dom";
import { v4 as uuidv4 } from "uuid";
import {
  ControllerMode,
  ExerciseSet,
  ExerciseSetBloc,
  Session,
  WorkoutProgramme,
} from "../../../Types/WorkoutTypes";
import { api } from "../../../api/apiClient";
import { SelectOption } from "../../../Components/CustomElements/Select";

type WorkoutProgrammeControllerProps = {
  children: ReactNode;
  mode: ControllerMode;
  workoutProgrammeId?: number;
};

type WorkoutProgrammeContextValue = {
  mode: ControllerMode;
  isCreateMode: boolean;
  isEditMode: boolean;
  isViewMode: boolean;
  isEditable: boolean;

  workoutProgrammeData: WorkoutProgramme;
  setWorkoutProgrammeData: React.Dispatch<
    React.SetStateAction<WorkoutProgramme>
  >;

  selectedSessionId: number | string | null;
  setSelectedSessionId: React.Dispatch<
    React.SetStateAction<number | string | null>
  >;
  selectedSession: Session | null;
  updateProgrammeField: <K extends keyof WorkoutProgramme>(
    field: K,
    value: WorkoutProgramme[K],
  ) => void;

  updateSession: (
    sessionId: number | string | null,
    updater: (session: Session) => Session,
  ) => void;

  updateSetBloc: (
    sessionId: number | string | null,
    setBlocId: number | string | null,
    updater: (setBloc: ExerciseSetBloc) => ExerciseSetBloc,
  ) => void;

  updateExerciseSet: (
    sessionId: number | string | null,
    setBlocId: number | string | null,
    setId: number | string | null,
    updater: (set: ExerciseSet) => ExerciseSet,
  ) => void;

  loading: boolean;
  saving: boolean;
  error: string | null;

  addSession: () => void;
  removeSession: (sessionId: number | string | null) => void;
  updateProgrammeName: (name: string) => void;

  createProgramme: () => Promise<void>;
  updateProgramme: () => Promise<void>;
  reloadProgramme: () => Promise<void>;

  exerciseTypeOptions: SelectOption[];
  exerciseOptions: SelectOption[];
  getExerciseOptions: () => Promise<void>;
  getExerciseTypeOptions: () => Promise<void>;
};

const emptyProgramme: WorkoutProgramme = {
  name: "New Programme",
  sessions: [],
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

  const routeWorkoutProgrammeId = params.workoutProgrammeId
    ? Number(params.workoutProgrammeId)
    : null;

  const resolvedWorkoutProgrammeId =
    workoutProgrammeId ?? routeWorkoutProgrammeId;

  const [workoutProgrammeData, setWorkoutProgrammeData] =
    useState<WorkoutProgramme>(emptyProgramme);

  const [selectedSessionId, setSelectedSessionId] = useState<
    number | string | null
  >(null);

  const [loading, setLoading] = useState<boolean>(false);
  const [saving, setSaving] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const isCreateMode = mode === "create";
  const isEditMode = mode === "edit";
  const isViewMode = mode === "view";
  const isEditable = isCreateMode || isEditMode;

  const [exerciseOptions, setExerciseOptions] = useState<SelectOption[]>([]);
  const [exerciseTypeOptions, setExerciseTypeOptions] = useState<
    SelectOption[]
  >([]);

  const selectedSession =
    workoutProgrammeData.sessions.find(
      (session) => getEntityId(session) === selectedSessionId,
    ) ?? null;

  const updateProgrammeField = <K extends keyof WorkoutProgramme>(
    field: K,
    value: WorkoutProgramme[K],
  ) => {
    if (!isEditable) return;

    setWorkoutProgrammeData((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const updateSession = (
    sessionId: number | string | null,
    updater: (session: Session) => Session,
  ) => {
    if (!isEditable) return;

    setWorkoutProgrammeData((prev) => ({
      ...prev,
      sessions: prev.sessions.map((session) =>
        getEntityId(session) === sessionId ? updater(session) : session,
      ),
    }));
  };

  const updateSetBloc = (
    sessionId: number | string | null,
    setBlocId: number | string | null,
    updater: (setBloc: ExerciseSetBloc) => ExerciseSetBloc,
  ) => {
    if (!isEditable) return;

    updateSession(sessionId, (session) => ({
      ...session,
      setBlocs: session.setBlocs.map((setBloc) =>
        getEntityId(setBloc) === setBlocId ? updater(setBloc) : setBloc,
      ),
    }));
  };

  const updateExerciseSet = (
    sessionId: number | string | null,
    setBlocId: number | string | null,
    exerciseSetId: number | string | null,
    updater: (exerciseSet: ExerciseSet) => ExerciseSet,
  ) => {
    if (!isEditable) return;

    updateSetBloc(sessionId, setBlocId, (setBloc) => ({
      ...setBloc,
      sets: setBloc.sets.map((exerciseSet) =>
        getEntityId(exerciseSet) === exerciseSetId
          ? updater(exerciseSet)
          : exerciseSet,
      ),
    }));
  };

  const loadProgramme = async (id: number) => {
    const response = await api.get<WorkoutProgramme>(
      "/api/WorkoutProgramme/getWorkoutProgramme",
      {
        params: { id },
      },
    );

    setWorkoutProgrammeData(response.data);
  };

  const reloadProgramme = async () => {
    if (!resolvedWorkoutProgrammeId) {
      setError("Workout programme id is required.");
      return;
    }

    setLoading(true);
    setError(null);

    try {
      await loadProgramme(resolvedWorkoutProgrammeId);
    } catch {
      setError("Failed to load workout programme.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    const initialiseProgramme = async () => {
      setError(null);
      setSelectedSessionId(null);

      if (isCreateMode) {
        setWorkoutProgrammeData({
          name: "New Programme",
          sessions: [],
        });
        return;
      }

      if (!resolvedWorkoutProgrammeId) {
        setError("Workout programme id is required for edit/view mode.");
        return;
      }

      setLoading(true);

      try {
        await loadProgramme(resolvedWorkoutProgrammeId);
      } catch {
        setError("Failed to load workout programme.");
      } finally {
        setLoading(false);
      }
    };

    initialiseProgramme();
  }, [isCreateMode, resolvedWorkoutProgrammeId]);

  const updateProgrammeName = (name: string) => {
    if (!isEditable) return;

    setWorkoutProgrammeData((prev) => ({
      ...prev,
      name,
    }));
  };

  const addSession = () => {
    if (!isEditable) return;

    setWorkoutProgrammeData((prev) => {
      const sessionCount = prev.sessions.length + 1;

      const newSession: Session = {
        tempId: uuidv4(),
        name: `Session ${sessionCount}`,
        setBlocs: [],
      };

      return {
        ...prev,
        sessions: [...prev.sessions, newSession],
      };
    });
  };

  const removeSession = (sessionId: number | string | null) => {
    if (!isEditable) return;

    setWorkoutProgrammeData((prev) => ({
      ...prev,
      sessions: prev.sessions.filter(
        (session) => getEntityId(session) !== sessionId,
      ),
    }));

    setSelectedSessionId((currentSessionId) => {
      if (currentSessionId === sessionId) {
        return null;
      }

      return currentSessionId;
    });
  };

  const createProgramme = async () => {
    if (!isCreateMode) return;

    setSaving(true);
    setError(null);

    try {
      const response = await api.post<WorkoutProgramme>(
        "/api/WorkoutProgramme/newWorkoutProgramme",
        workoutProgrammeData,
      );

      setWorkoutProgrammeData(response.data);
    } catch {
      setError("Failed to create workout programme.");
    } finally {
      setSaving(false);
    }
  };

  const updateProgramme = async () => {
    if (!isEditMode) return;

    setSaving(true);
    setError(null);

    try {
      const response = await api.post<WorkoutProgramme>(
        "/api/WorkoutProgramme/updateWorkoutProgramme",
        workoutProgrammeData,
      );

      setWorkoutProgrammeData(response.data);
    } catch {
      setError("Failed to update workout programme.");
    } finally {
      setSaving(false);
    }
  };

  const getExerciseOptions = async () => {
    if (!isEditable) return;

    try {
      const response = await api.get<SelectOption[]>(
        "/api/Exercise/GetExercises",
      );
      setExerciseOptions(response.data);
    } catch (error) {
      console.log(error);
    }
  };

  const getExerciseTypeOptions = async () => {
    if (!isEditable) return;

    try {
      const response = await api.get<SelectOption[]>(
        "/api/ExerciseType/GetExerciseTypes",
      );
      setExerciseTypeOptions(response.data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    if (!isEditable) return;

    getExerciseOptions();
    getExerciseTypeOptions();
  }, [isEditable]);

  const value = useMemo<WorkoutProgrammeContextValue>(
    () => ({
      mode,
      isCreateMode,
      isEditMode,
      isViewMode,
      isEditable,

      exerciseTypeOptions,
      exerciseOptions,
      getExerciseOptions,
      getExerciseTypeOptions,

      workoutProgrammeData,
      setWorkoutProgrammeData,

      selectedSessionId,
      setSelectedSessionId,
      selectedSession,

      updateProgrammeField,
      updateSession,
      updateSetBloc,
      updateExerciseSet,

      loading,
      saving,
      error,

      addSession,
      removeSession,
      updateProgrammeName,

      createProgramme,
      updateProgramme,
      reloadProgramme,
    }),
    [
      mode,
      isCreateMode,
      isEditMode,
      isViewMode,
      isEditable,
      exerciseOptions,
      exerciseTypeOptions,
      workoutProgrammeData,
      selectedSessionId,
      selectedSession,
      loading,
      saving,
      error,
    ],
  );

  return (
    <WorkoutProgrammeContext.Provider value={value}>
      {children}
    </WorkoutProgrammeContext.Provider>
  );
}
