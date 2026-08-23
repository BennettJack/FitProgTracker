import { ButtonProps as MuiButtonProps } from "@mui/material";
import MuiButton from "@mui/material/Button";

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

export const NumberedButton = ({ value, onClick, ...rest }: MuiButtonProps) => {
  return <Button {...rest}>{value}</Button>;
};
