import {
  RadioGroup as MuiRadioGroup,
  RadioGroupProps as MuiRadioGroupProps,
  FormControlLabel,
  Radio,
  FormControl,
  FormHelperText,
} from "@mui/material";
import {
  Control,
  FieldPath,
  FieldValues,
  useController,
  useFormContext,
} from "react-hook-form";

export type RadioGroupProps = MuiRadioGroupProps & {
  options: Array<{ label: string; value: string }>;
};

type RHFRadioGroupProps<T extends FieldValues> = Omit<
  RadioGroupProps,
  "children"
> & {
  control?: Control<T>;
  name: FieldPath<T>;
};

export const RadioGroup = ({ options, ...rest }: RadioGroupProps) => {
  return (
    <MuiRadioGroup {...rest}>
      {options.map((option) => (
        <FormControlLabel
          key={option.value}
          value={option.value}
          control={<Radio />}
          label={option.label}
        />
      ))}
    </MuiRadioGroup>
  );
};

export const RhfRadioGroup = <T extends FieldValues>({
  control,
  name,
  options,
  ...rest
}: RHFRadioGroupProps<T>) => {
  const methods = useFormContext<T>();
  const { field, fieldState } = useController({
    control: control ?? methods.control,
    name,
  });

  return (
    <FormControl error={!!fieldState.error}>
      <RadioGroup {...field} {...rest} options={options} />
      {fieldState.error && (
        <FormHelperText>{fieldState.error.message}</FormHelperText>
      )}
    </FormControl>
  );
};
