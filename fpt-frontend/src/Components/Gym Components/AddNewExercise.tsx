import axios from "axios";
import React, {useState, useEffect} from "react";
import {Select} from "../CustomElements/Select";

interface FormData{
    ExerciseName:string;
    ExerciseDescription:string;
    EquipmentIds:string[];
    MuscleIds:string[];
}

interface DropdownOption{
    id: string;
    name: string;
}


export function AddNewExercise(){
    
    const [formData, setFormData] = useState<FormData>({
        ExerciseName: "",
        ExerciseDescription: "",
        EquipmentIds: [],
        MuscleIds: [],
    });
    
    const [equipment, setEquipment] = useState<DropdownOption[]>([]);
    const [muscles, setMuscles] = useState<DropdownOption[]>([]);
    
    
    useEffect(()=>{
        const fetchData = async () => {
            try {
                const[equipmentData, muscleData] = await Promise.all([
                    axios.get<DropdownOption[]>("/equipment/getOptionData"),
                    axios.get<DropdownOption[]>("/muscle/getOptionData"),
                ]);
                
                setEquipment(equipmentData.data);
                setMuscles(muscleData.data);
            }
            catch (err){
                console.error(err);
            }
        }
        
        fetchData();
    }, []);
    function handleFormSubmit(){
        
    }
    const test = [
        {label: "First", value: "1"},
        {label: "Second", value: "2"},
        {label: "Third", value: "3"},
        {label: "Fourth", value: "4"},
        {label: "Fifth", value: "5"},
    ]
    
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
            </form>
            
            
            <Select options={test} />
        </>
    )    
}