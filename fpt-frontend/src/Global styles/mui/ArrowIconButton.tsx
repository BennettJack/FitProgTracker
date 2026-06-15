import ArrowForwardIosIcon from "@mui/icons-material/ArrowForwardIos";
import { Theme } from "@mui/material/styles";
import { IconButton, IconButtonProps } from "@mui/material";
import {
  ArrowBackIos,
  KeyboardArrowDown,
  KeyboardArrowLeft,
  KeyboardArrowRight,
  KeyboardArrowUp,
} from "@mui/icons-material";

type IconButtonColour = "primary" | "secondary" | "success" | "cancel";
type IconDirection = "up" | "down" | "left" | "right";

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

interface DirectionalIconButtonProps extends IconButtonProps {
  colour?: IconButtonColour;
  direction: IconDirection;
  label: string;
}

export const ArrowIconButton = ({
  colour = "primary",
  direction,
  sx,
  ...props
}: DirectionalIconButtonProps) => {
  const getArrowIcon = () => {
    switch (direction) {
      case "up":
        return <KeyboardArrowUp />;
      case "down":
        return <KeyboardArrowDown />;
      case "left":
        return <KeyboardArrowLeft />;
      case "right":
        return <KeyboardArrowRight />;
    }
  };

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
      {getArrowIcon()}
    </IconButton>
  );
};
