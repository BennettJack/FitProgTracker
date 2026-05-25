import React, { FormEvent, use, useEffect, useState } from "react";
import { Select, SelectOption } from "../CustomElements/Select";
import axios from "axios";
import { api } from "../../api/apiClient";

interface formData {
  name: string;
  description: string;
  equipmentIds: number[];
  muscleIds: number[];
}

interface EquipmentOptionData {
  equipmentOptions: SelectOption[];
  muscleOptions: SelectOption[];
  exerciseTypeOptions: SelectOption[];
}
export default function ExerciseController({}): React.ReactElement {
  const [equipment, setEquipment] = useState<SelectOption[]>([]);
  const [muscles, setMuscles] = useState<SelectOption[]>([]);

  const [selectedEquipment, setSelectedEquipment] = useState<SelectOption[]>(
    [],
  );
  const [selectedMuscles, setSelectedMuscles] = useState<SelectOption[]>([]);
  useState<SelectOption>();

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
    getOptionData().then((data) => {
      if (!data) {
        console.log("error");
      } else {
        setEquipment(data.equipmentOptions);
        setMuscles(data.muscleOptions);
      }
    });
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
  const getOptionData = async () => {
    try {
      return await api
        .get<EquipmentOptionData>("/api/Exercise/ExerciseOptionData")
        .then((res) => res.data);
    } catch (error) {
      console.log(error);
    }
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
