import axios from "axios";
import React, {useState, useEffect} from "react";
import {Select, SelectOption} from "../CustomElements/Select";
import styles from './AddNewExercise.module.css'
import {useNavigate} from "react-router";
import {AddNewExerciseDto} from "./Gym Types/Exercise/AddNewExerciseDtoType";

interface FormData{
    ExerciseName:string;
    ExerciseDescription:string;
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
        ExerciseName:"",
        ExerciseDescription: "",
    });
    
    const navigate = useNavigate();
    
    const [equipment, setEquipment] = useState<SelectOption[]>([]);
    const [muscles, setMuscles] = useState<SelectOption[]>([]);

    const [selectedEquipment, setSelectedEquipment] = useState<SelectOption[]>([]);
    const [selectedMuscles, setSelectedMuscles] = useState<SelectOption[]>([]);
    
    
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

            await axios.get<DropdownResponse>(process.env.REACT_APP_DEV_API_HOST +"equipment/getOptionData")
                .then((response) => {
                    const options: SelectOption[] = response.data.data.map((item) => ({
                        label: item.label,
                        value: String(item.value)
                    }));
                    setEquipment(options);
                }).catch((err) => console.log(err));
        }
        
        fetchData().then(r => console.log(r));
    }, []);

    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: value,
        }));
    };
    
    const handleFormSubmit = async (e: React.FormEvent) =>{
        e.preventDefault();
        const newExercise: AddNewExerciseDto = {
            exerciseName: formData.ExerciseName,
            description: formData.ExerciseDescription,
            muscleIds: selectedMuscles.map(m => Number(m.value)),
            equipmentIds: selectedEquipment.map(e => Number(e.value)),
            
        }
        
        console.log(newExercise)
        
        await axios.post(process.env.REACT_APP_DEV_API_HOST + "exercise/AddExercise", newExercise, {withCredentials: true})
            .then((response) => { console.log(response); }).catch((error) => console.log(error));
        
        
    }
    return(
        <div className={styles.wrapper}>
            <h2>Add new exercise</h2>
            <form onSubmit={(e) => handleFormSubmit(e)}>
                <div>
                    <label htmlFor="ExerciseName">ExerciseName:</label>
                    <input type={"text"} name={"ExerciseName"} onChange={handleChange}></input>
                </div>
                
                <div>
                    <label htmlFor="ExerciseDescription">ExerciseDescription:</label>
                    <input type={"textarea"} name={"ExerciseDescription"} onChange={handleChange}></input>
                </div>
                <Select multiple options={equipment} value={selectedEquipment} onChange={o => setSelectedEquipment(o)} index={0}/>
                <Select multiple options={muscles} value={selectedMuscles} onChange={o => setSelectedMuscles(o)} index={1}/>
                <button className={styles.cancelButton} onClick={() => navigate("/loginSignup")}>Cancel</button>
                <button className={styles.addButton} type="submit">Add</button>
            </form>
        </div>
    )    
}