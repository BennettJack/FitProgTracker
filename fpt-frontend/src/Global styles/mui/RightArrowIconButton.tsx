import ArrowForwardIosIcon from "@mui/icons-material/ArrowForwardIos";
import { Theme } from "@mui/material/styles";
import { IconButton, IconButtonProps } from "@mui/material";

type IconButtonColour = "primary" | "secondary" | "success" | "cancel";

type IconButtonColourSet = {
  default: string;
  hover: string;
  active: string;
};

const getIconButtonColours = (
  theme: Theme,
  colour: IconButtonColour,
): IconButtonColourSet => {
  switch (colour) {
    case "primary":
      return {
        default: theme.palette.text.secondary,
        hover: theme.palette.primary.main,
        active: theme.palette.primary.dark,
      };

    case "secondary":
      return {
        default: theme.palette.text.secondary,
        hover: theme.palette.secondary.main,
        active: theme.palette.secondary.dark,
      };

    case "success":
      return {
        default: theme.palette.success.main,
        hover: theme.palette.success.dark,
        active: theme.palette.success.light,
      };

    case "cancel":
      return {
        default: theme.palette.text.secondary,
        hover: theme.palette.error.main,
        active: theme.palette.error.dark,
      };
  }
};

interface RightArrowIconButtonProps extends IconButtonProps {
  colour?: IconButtonColour;
  label: string;
}

export const RightArrowIconButton = ({
  colour = "primary",
  sx,
  ...props
}: RightArrowIconButtonProps) => {
  return (
    <IconButton
      aria-label={props.label}
      {...props}
      sx={[
        (theme) => {
          const colours = getIconButtonColours(theme, colour);

          return {
            color: colours.default,
            transition: "color 0.2s ease",
            cursor: "pointer",
            "&:hover": {
              color: colours.hover,
            },
            "&:active": {
              color: colours.active,
            },
          };
        },
        ...(sx ? (Array.isArray(sx) ? sx : [sx]) : []),
      ]}
    >
      <ArrowForwardIosIcon />
    </IconButton>
  );
};
