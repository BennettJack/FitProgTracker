import { ButtonProps as MuiButtonProps } from "@mui/material";
import MuiButton from "@mui/material/Button";
import { InputControlProps } from "./MuiBaseTypes";
import { FieldValues, useController, useFormContext } from "react-hook-form";

export type RHFButtonProps<T extends FieldValues> = MuiButtonProps &
  InputControlProps<T> & {
    min: number;
    max: number;
  };
export const Button = ({
  variant,
  color,
  children,
  sx = {},
  ...rest
}: MuiButtonProps) => {
  const disabledSx = rest.disabled
    ? {
        "&.Mui-disabled": {
          cursor: "not-allowed",
          pointerEvents: "auto",
          borderColor: "#0000001f",
        },
      }
    : {};

  return (
    <MuiButton variant={variant} color={color} {...rest}>
      {children}
    </MuiButton>
  );
};

export const NumberedButton = <T extends FieldValues>({
  value,
  onClick,
  control,
  name,
  max,
  min,
  onValueChange,
  ...rest
}: RHFButtonProps<T>) => {
  const methods = useFormContext<T>();
  const { field } = useController({
    control: control ?? methods.control,
    name,
  });

  return (
    <Button
      {...rest}
      value={value}
      onClick={(event) => {
        const increment = Number(value);
        const currentValue = Number(field.value) || 0;
        let newValue = currentValue + increment;
        newValue > max
          ? (newValue = max)
          : newValue < min
            ? (newValue = min)
            : newValue;
        field.onChange(newValue);
        onValueChange?.(String(newValue));
        onClick?.(event);
      }}
    >
      {Number(value) > 0 && "+"}
      {value}
    </Button>
  );
};
