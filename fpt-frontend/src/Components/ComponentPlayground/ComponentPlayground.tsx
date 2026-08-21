import { z } from "zod";
import { FormProvider, useForm, useFormContext } from "react-hook-form";
import { TextField, ThemeProvider } from "@mui/material";
import { RhfTextField } from "../../Global styles/mui/ControlledComponents/TextField";
import { useState } from "react";
import { zodResolver } from "@hookform/resolvers/zod";
import { RhfRadioGroup } from "../../Global styles/mui/ControlledComponents/RadioGroup";

const schema = z.object({
  name: z.string().min(3, "should be more than 3"),
  optional: z.string().min(1, "please select an option"),
});

type FormValues = z.infer<typeof schema>;
export default function ComponentPlayground() {
  const methods = useForm<FormValues>({
    resolver: zodResolver(schema),
    defaultValues: {
      name: "",
      optional: "",
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
  });

  const options = [
    { label: "Option 1", value: "1" },
    { label: "Option 2", value: "2" },
    { label: "Option 3", value: "3" },
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
        <input type={"submit"} />
      </form>

      <h2> values</h2>
      <p>Name: {data.name}</p>
      <p>Optional: {data.optional}</p>
    </>
  );
}
