import { z } from "zod";
import { FormProvider, useForm, useFormContext } from "react-hook-form";
import { RhfTextField } from "../Inputs/TextField";
import { useState } from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import { RhfRadioGroup } from "../Inputs/RadioGroup";
import { SelectOption } from "../CustomElements/MultiSelect/Select";
import { RhfSelect } from "../Inputs/Select";
import { RhfSwitch } from "../Inputs/Switch";
import { NumberedButton } from "../Inputs/Button";
import { NumberField, RhfNumberField } from "../Inputs/NumberField";

const schema = z.object({
  name: z.string().min(3, "should be more than 3"),
  optional: z.string().min(1, "please select an option"),
  selectElement: z.string().min(1, "please select an option"),
  switchReq: z.boolean().refine((val) => val, { message: "required" }),
  numberField: z
    .number()
    .min(-50, "Value must be greater than -50")
    .max(50, "Value must be less than 50"),
});

type FormValues = z.infer<typeof schema>;
export default function ComponentPlayground() {
  const methods = useForm<FormValues>({
    resolver: zodResolver(schema),
    defaultValues: {
      name: "",
      optional: "",
      selectElement: "",
      switchReq: false,
      numberField: 0,
    },
  });
  return (
    <FormProvider {...methods}>
      <ThisHasToBeHere />
    </FormProvider>
  );
}

function ThisHasToBeHere() {
  const { control, handleSubmit } = useFormContext<FormValues>();

  const [data, setData] = useState<FormValues>({
    name: "",
    optional: "",
    selectElement: "",
    switchReq: false,
    numberField: 0,
  });

  const options = [
    { label: "Option 1", value: "1" },
    { label: "Option 2", value: "2" },
    { label: "Option 3", value: "3" },
  ];

  const selectOptions: SelectOption[] = [
    {
      label: "Option 1",
      value: "1",
    },
    {
      label: "Option 2",
      value: "2",
    },
    {
      label: "Option 3",
      value: "3",
    },
  ];
  return (
    <>
      <h1>Component Playground</h1>

      <form onSubmit={handleSubmit((a) => setData(a))}>
        <RhfTextField
          variant={"outlined"}
          control={control}
          name={"name"}
          label={"Name"}
        />
        <RhfRadioGroup options={options} control={control} name={"optional"} />
        <RhfSelect
          variant={"outlined"}
          options={selectOptions}
          control={control}
          name={"selectElement"}
          value={data.selectElement}
        />
        <RhfSwitch control={control} name={"switchReq"} label={"Switch"} />
        <RhfNumberField
          control={control}
          name={"numberField"}
          label={"Number Field"}
          min={-10}
          max={10}
          size={"small"}
        />
        <NumberedButton
          control={control}
          name={"numberField"}
          value={1}
          min={-10}
          max={10}
        />
        <NumberedButton
          control={control}
          name={"numberField"}
          value={5}
          min={-10}
          max={10}
        />
        <NumberedButton
          control={control}
          name={"numberField"}
          value={-1}
          min={-10}
          max={10}
        />
        <NumberedButton
          control={control}
          name={"numberField"}
          value={-5}
          min={-10}
          max={10}
        />
        <input type={"submit"} />
      </form>

      <h2> values</h2>
      <p>Name: {data.name}</p>
      <p>Optional: {data.optional}</p>
      <p>Select Element: {data.selectElement}</p>
      <p>Switch on: {data.switchReq.toString()}</p>
    </>
  );
}
