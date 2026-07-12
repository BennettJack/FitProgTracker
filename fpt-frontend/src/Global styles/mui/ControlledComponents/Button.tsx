import { ButtonProps as MuiButtonProps } from "@mui/material";
import { FieldValues, Control, useFormContext } from "react-hook-form";
import MuiButton from "@mui/material/Button";

type RHFButtonProps<T extends FieldValues> = MuiButtonProps & {
  control?: Control<T>;
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
    <MuiButton
      variant={variant}
      color={color}
      sx={{ ...sx, ...disabledSx }}
      {...rest}
    >
      {children}
    </MuiButton>
  );
};
