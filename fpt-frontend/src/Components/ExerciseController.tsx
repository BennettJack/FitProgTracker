import React, { FormEvent, use, useEffect, useState } from "react";
import { Select, SelectOption } from "./CustomElements/Select";
import { Equipment, Muscle } from "../Types/ModelTypes";
import axios from "axios";

interface formData {
  name: string;
  description: string;
  equipmentIds: number[];
  muscleIds: number[];
}
export default function ExerciseController({}): React.ReactElement {
  const [equipment, setEquipment] = useState<SelectOption[]>([]);
  const [muscles, setMuscles] = useState<SelectOption[]>([]);

  const [selectedEquipment, setSelectedEquipment] = useState<SelectOption[]>(
    [],
  );
  const [selectedMuscles, setSelectedMuscles] = useState<SelectOption[]>([]);

  const [formData, setFormData] = useState<formData>({
    name: "",
    description: "",
    equipmentIds: [],
    muscleIds: [],
  });

  const submitExercise = async (e: FormEvent) => {
    e.preventDefault();
    console.log(formData);
    await axios.post<formData>(
      "https://localhost:7206/api/exercise/AddExercise",
      formData,
    );
  };
  useEffect(() => {
    getMuscleOptions().then((data) => setMuscles(data));
    getEquipmentOptions().then((data) => setEquipment(data));
  }, []);

  useEffect(() => {
    setFormData((prev) => ({
      ...prev,
      equipmentIds: selectedEquipment.map((o) => Number(o.value)),
      muscleIds: selectedMuscles.map((o) => Number(o.value)),
    }));
  }, [selectedEquipment, selectedMuscles]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };
  const getMuscleOptions = async (): Promise<SelectOption[]> => {
    return axios
      .get<SelectOption[]>("https://localhost:7206/api/Muscle/getOptionData")
      .then((response) => response.data);
  };

  const getEquipmentOptions = async (): Promise<SelectOption[]> => {
    return axios
      .get<SelectOption[]>("https://localhost:7206/api/Equipment/getOptionData")
      .then((response) => response.data);
  };

  return (
    <>
      <form>
        <label htmlFor="name">Exercise name</label>
        <input
          type={"text"}
          name={"name"}
          value={formData.name}
          onChange={handleChange}
        />

        <label htmlFor="description">Exercise description</label>
        <input
          type={"text"}
          name={"description"}
          value={formData.description}
          onChange={handleChange}
        />

        <Select
          multiple
          value={selectedEquipment}
          options={equipment}
          index={0}
          onChange={(o) => setSelectedEquipment(o)}
        />
        <Select
          multiple
          value={selectedMuscles}
          options={muscles}
          index={1}
          onChange={(o) => setSelectedMuscles(o)}
        />
        <button onClick={submitExercise}>Add</button>
      </form>
    </>
  );
}
