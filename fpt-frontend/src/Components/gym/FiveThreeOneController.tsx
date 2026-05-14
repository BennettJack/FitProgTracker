import { FormEvent, useEffect, useState } from "react";
import { UnitOfWeight } from "../../Types/WorkoutTypes";
import styles from "../../Pages/Gym/WorkoutProgrammeBuilder/WorkoutProgrammeController.module.css";
import React from "react";
import axios from "axios";

//use epley formula for x by y if 1RM > 1 ... weight x (1 + reps/ 30)
type fiveThreeOne = {
  unitOfWeight: UnitOfWeight;
  overheadPressValue: number;
  barbellSquatValue: number;
  benchPressValue: number;
  deadliftValue: number;
};

const conversion: number = 2.205;
function FiveThreeOneController() {
  const [formData, setFormData] = useState<fiveThreeOne>({
    unitOfWeight: "kg",
    overheadPressValue: 0,
    barbellSquatValue: 0,
    benchPressValue: 0,
    deadliftValue: 0,
  });
  const [waitingResponse, setWaitingResponse] = React.useState<boolean>(false);
  const submitForm = async (
    e: FormEvent<HTMLFormElement>,
    data: fiveThreeOne,
  ) => {
    e.preventDefault();
    setWaitingResponse(true);
    await axios
      .post(
        "https://localhost:7206/api/WorkoutProgramme/createFiveThreeOneProgramme",
        formData,
        {
          headers: {
            Authorization: `Bearer`,
          },
        },
      )
      .then((res) => {
        res.status === 201 ? console.log("success") : console.log("fail");
      });
    setWaitingResponse(false);
  };
  const changeUnitOfWeight = (): void => {
    formData.unitOfWeight === "kg"
      ? setFormData((prev) => ({
          ...prev,
          unitOfWeight: "lbs",
          overheadPressValue: prev.overheadPressValue * conversion,
          barbellSquatValue: prev.barbellSquatValue * conversion,
          benchPressValue: prev.benchPressValue * conversion,
          deadliftValue: prev.deadliftValue * conversion,
        }))
      : setFormData((prev) => ({
          ...prev,
          unitOfWeight: "kg",
          overheadPressValue: prev.overheadPressValue / conversion,
          barbellSquatValue: prev.barbellSquatValue / conversion,
          benchPressValue: prev.benchPressValue / conversion,
          deadliftValue: prev.deadliftValue / conversion,
        }));
  };

  useEffect(() => {
    console.log(formData);
  }, [formData]);

  const updateFormData = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const { name, value } = e.target;

    setFormData((prev) => ({
      ...prev,
      [name]: Number(value),
    }));
  };

  return waitingResponse ? (
    <div>
      <p>waiting :)</p>
    </div>
  ) : (
    <div className={styles.wrapper}>
      <form name={`fiveThreeOneForm`} onSubmit={(e) => submitForm(e, formData)}>
        <label htmlFor={`overheadPressValue`}>Overhead Press</label>
        <input
          name={`overheadPressValue`}
          type={`number`}
          onChange={updateFormData}
          value={formData.overheadPressValue}
        />
        <label htmlFor={`barbellSquatValue`}>Barbell Squat</label>
        <input
          name={`barbellSquatValue`}
          type={`number`}
          onChange={updateFormData}
          value={formData.barbellSquatValue}
        />
        <label htmlFor={`benchPressValue`}>Bench Press</label>
        <input
          name={`benchPressValue`}
          type={`number`}
          onChange={updateFormData}
          value={formData.benchPressValue}
        />
        <label htmlFor={`deadliftValue`}>Deadlift</label>
        <input
          name={`deadliftValue`}
          type={`number`}
          onChange={updateFormData}
          value={formData.deadliftValue}
        />
        <button type="submit">Submit</button>
      </form>
      <button onClick={changeUnitOfWeight}>
        set to {formData.unitOfWeight === "kg" ? "lbs" : "Kg"}
      </button>
    </div>
  );
}

export default FiveThreeOneController;
