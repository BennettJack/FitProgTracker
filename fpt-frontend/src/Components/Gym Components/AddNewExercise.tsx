import axios from "axios";
import React, {useState, useEffect} from "react";
import {Select, SelectOption} from "../CustomElements/Select";

interface FormData{
    ExerciseName:string;
    ExerciseDescription:string;
    EquipmentIds:string[];
    MuscleIds:string[];
}
//TODO -  Set this as a global variable so we can use it elsewhere
type DropdownResponse = {
    isSuccess: boolean,
    status: number,
    message: string,
    data: {value: number, label: string}[];
}

export function AddNewExercise(){
    
    const [formData, setFormData] = useState<FormData>({
        ExerciseName: "",
        ExerciseDescription: "",
        EquipmentIds: [],
        MuscleIds: [],
    });
    
    const [equipment, setEquipment] = useState<SelectOption[]>([]);
    const [muscles, setMuscles] = useState<SelectOption[]>([]);
    
    
    useEffect(()=>{
        const fetchData = async () => {
            
            await axios.get<DropdownResponse>(process.env.REACT_APP_DEV_API_HOST +"muscle/getOptionData")
                .then((response) => {
                    const options: SelectOption[] = response.data.data.map((item) => ({
                        label: item.label,
                        value: String(item.value)
                    }));
                    setMuscles(options);
                }).catch((err) => console.log(err));
        }
        
        fetchData();
    }, []);
    
    
    useEffect(() => {
        console.log(value1)
        console.log(muscles[1]);
        if (muscles.length > 0) {
            setValue1([muscles[0]]);
        }
    }, [muscles]);
    
    const [value1, setValue1] = useState<SelectOption[]>([]);
    
    return(
        <>
            <form onSubmit={(e) => e.preventDefault()}>
                <div>
                    <label htmlFor="ExerciseName">ExerciseName:</label>
                    <input type={"text"} name={"ExerciseName"}></input>
                </div>
                
                <div>
                    <label htmlFor="ExerciseDescription">ExerciseDescription:</label>
                    <input type={"textarea"} name={"ExerciseDescription"}></input>
                </div>
                <Select multiple options={muscles} value={value1} onChange={o => setValue1(o)} />
            </form>
        </>
    )    
}