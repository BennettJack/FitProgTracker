export type WorkoutProgramme = {
    id?: number
    name: string
    workoutSessions: WorkoutSession[]
}

export type WorkoutSession = {
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

export type ProgrammeMode = "create" | "edit" | "input"

export type WorkoutProgrammeBuilderProps = {
    workoutProgrammeId? : number
    workoutProgrammeName?: string
    mode: ProgrammeMode
}

export type sessionBuilderProps = {
    sessionId? : number
    sessionName? : string
    
}