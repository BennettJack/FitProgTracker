import { z } from "zod";
import { v4 as uuidv4 } from "uuid";

export const ExerciseSetRecordSchema = z.object({
  id: z.number().optional(),
  repsCompleted: z.number(),
  weight: z.number(),
  exerciseId: z.number(),
  exerciseTypeId: z.number(),
  exerciseSetId: z.number().optional(),
});
export const ExerciseSetSchema = z.object({
  id: z.number().optional(),
  tempId: z.string().optional(),
  description: z.string().optional(),
  repCeiling: z.coerce.number().min(1, "Rep ceiling cannot be less than 1"),
  repFloor: z.coerce.number().min(1, "Rep floor cannot be less than 1"),
  exerciseTypeId: z.coerce.string().min(1, "Please select an exercise type"),
  exerciseId: z.coerce.string().min(1, "Please select an exercise"),
  todayRecord: ExerciseSetRecordSchema.optional(),
});

export const ExerciseSetBlocSchema = z.object({
  id: z.number().optional(),
  tempId: z.string().optional(),
  name: z.string(),
  sets: z.array(ExerciseSetSchema),
});

export const SessionSchema = z.object({
  id: z.number().optional(),
  tempId: z.string().optional(),
  name: z.string(),
  setBlocs: z.array(ExerciseSetBlocSchema),
});

export const WorkoutProgrammeSchema = z.object({
  tempId: z.string().optional(),
  id: z.number().optional(),
  name: z.string().min(10, { message: "Name must be at least 10 characters" }),
  sessions: z.array(SessionSchema),
});

export const createEmptyWorkoutProgramme = (): WorkoutProgramme => ({
  tempId: uuidv4(),
  name: "New workout programme",
  sessions: [],
});

export const createEmptySession = (): Session => ({
  tempId: uuidv4(),
  name: "New session",
  setBlocs: [],
});

export const createEmptyExerciseSetBloc = (): ExerciseSetBloc => ({
  tempId: uuidv4(),
  name: "New set bloc",
  sets: [],
});

export const createEmptyExerciseSet = (): ExerciseSet => ({
  tempId: uuidv4(),
  description: "",
  repCeiling: 0,
  repFloor: 0,
  exerciseTypeId: "",
  exerciseId: "",
});
export type WorkoutProgramme = z.infer<typeof WorkoutProgrammeSchema>;
export type Session = z.infer<typeof SessionSchema>;
export type ExerciseSetBloc = z.infer<typeof ExerciseSetBlocSchema>;
export type ExerciseSet = z.infer<typeof ExerciseSetSchema>;
export type ExerciseSetRecord = z.infer<typeof ExerciseSetRecordSchema>;
