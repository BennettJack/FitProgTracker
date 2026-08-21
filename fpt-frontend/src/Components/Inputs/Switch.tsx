import {
  FormControlLabel,
  FormGroup,
  SwitchProps as MuiSwitchProps,
  Switch as MuiSwitch,
} from "@mui/material";
import { InputControlProps } from "./MuiBaseTypes";
import { FieldValues, useController, useFormContext } from "react-hook-form";

export type SwitchProps = MuiSwitchProps & {
  label?: string;
};

export type RHFSwitchProps<T extends FieldValues> = SwitchProps &
  InputControlProps<T>;

export const Switch = ({ ref, label, ...rest }: SwitchProps) => {
  return (
    <FormGroup>
      <FormControlLabel
        sx={{ display: "flex", alignItems: "center" }}
        control={<MuiSwitch {...rest} ref={ref} />}
        label={label}
      />
    </FormGroup>
  );
};

export const RhfSwitch = <T extends FieldValues>({
  control,
  name,
  ...rest
}: RHFSwitchProps<T>) => {
  const methods = useFormContext<T>();
  const { field } = useController({
    control: control ?? methods.control,
    name,
  });

  return <Switch {...rest} {...field} checked={field.value} />;
};
