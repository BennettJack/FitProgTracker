import React, {useState, useEffect} from 'react';



type ExerciseSessionProps = {
    edit: boolean,
}



export function ExerciseSession(edit : ExerciseSessionProps): React.ReactElement {
    
    return(
        <>

            {edit ? true: (
                <div>
                    
                </div>
            )}
        </>
    )
}