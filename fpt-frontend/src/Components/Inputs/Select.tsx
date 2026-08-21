import { TextField, TextFieldProps } from "./TextField";
import { SelectOption } from "../CustomElements/MultiSelect/Select";
import { MenuItem } from "@mui/material";
import {
  Control,
  FieldPath,
  FieldValues,
  useController,
  useFormContext,
  useWatch,
} from "react-hook-form";
import { InputControlProps } from "./MuiBaseTypes";
import { useEffect } from "react";

export type SelectProps = TextFieldProps & {
  options: SelectOption[];
  value: string;
};

type RHFSelectProps<T extends FieldValues> = SelectProps & InputControlProps<T>;
export const Select = ({
  ref,
  value,
  onChange,
  tooltip,
  options,
  ...rest
}: SelectProps) => (
  <TextField
    {...rest}
    value={value}
    onChange={onChange}
    tooltip={tooltip}
    select
  >
    {options.map((option) => (
      <MenuItem
        key={option.value}
        value={option.value}
        disabled={option.disabled}
      >
        {option.label}
      </MenuItem>
    ))}
  </TextField>
);

export const RhfSelect = <T extends FieldValues>({
  control,
  name,
  onValueChange,
  ...rest
}: RHFSelectProps<T>) => {
  const methods = useFormContext<T>();
  const { field, fieldState } = useController({
    control: control ?? methods.control,
    name,
  });
  // Watch the field value to trigger side effects
  const watchedValue = useWatch({
    control: control ?? methods.control,
    name,
  });

  // Call the callback when value changes
  useEffect(() => {
    onValueChange?.(watchedValue);
  }, [watchedValue, onValueChange]);

  return (
    <Select
      {...rest}
      {...field}
      error={!!fieldState.error}
      helperText={fieldState.error?.message}
    />
  );
};
