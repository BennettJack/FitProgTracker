export type WorkoutProgramme = {
    id?: number
    name: string
    workoutSessions: ExerciseSession[]
}

export type ExerciseSession = {
    id?: number
    tempId?: string
    name: string
    exerciseSetBlocs: ExerciseSetBloc[]
}

export type ExerciseSetBloc = {
    id?: number
    tempId?: string
    name: string
    exerciseSets: ExerciseSet[]
}

export type ExerciseSet = {
    id?: number
    tempId?: string
    name: string
    description: string
    repCeiling: number
    repFloor: number
    
}

export type ExerciseSetRecord = {
    exerciseSetId: number
    repsCompleted: number
    weight: number
}

export type ControllerMode = "create" | "edit" | "input"

export type WorkoutProgrammeControllerProps = {
    workoutProgrammeId? : number
    workoutProgrammeName?: string
    mode: ControllerMode
}

export type ExerciseSessionControllerProps = {
    exerciseSession: ExerciseSession
    mode: ControllerMode
    updateProgramme?: (
        updater: (prev: WorkoutProgramme) => WorkoutProgramme
    ) => void;
}

export type ExerciseSetBlocControllerProps = {
    exerciseSetBloc: ExerciseSetBloc
    mode: ControllerMode
    updateSession?: (
        updater: (prev: ExerciseSession) => ExerciseSession
    ) => void
}

export type ExerciseSetControllerProps = {
    exerciseSet: ExerciseSet
    mode: ControllerMode
    updateExerciseSetBloc: (
        updater:(prev: ExerciseSetBloc) => ExerciseSetBloc
    ) => void
}

export type UpdateProgrammeData = (
    updater: (prev: WorkoutProgramme) => WorkoutProgramme) => void