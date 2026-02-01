export type WorkoutProgramme = {
    id?: number
    name: string
    workoutSessions: ExerciseSession[]
}

export type ExerciseSession = {
    id?: number
    name: string
    exerciseSetBlocks: ExerciseSetBloc[]
}

export type ExerciseSetBloc = {
    id?: number
    name: string
    exerciseSets: ExerciseSet[]
}

export type ExerciseSet = {
    id?: number
    name: string
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
    
}