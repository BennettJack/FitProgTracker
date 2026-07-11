import {
  TextField as MuiTextField,
  TextFieldProps as MuiTextFieldProps,
  InputAdornment,
  Tooltip,
  TooltipProps,
} from "@mui/material";
import InfoOutlinedIcon from "@mui/icons-material/InfoOutlined";
import {
  Control,
  FieldPath,
  FieldValues,
  useController,
} from "react-hook-form";

type TextFieldProps = MuiTextFieldProps & {
  tooltip?: TooltipProps;
};

type RHFTextFieldProps<T extends FieldValues> = TextFieldProps & {
  control: Control<T>;
  name: FieldPath<T>;
};

export const TextField = ({
  tooltip,
  slotProps,
  select,
  ...rest
}: TextFieldProps) => {
  const mergedSlotProps = {
    ...slotProps,
    input: {
      ...slotProps?.input,
      ...(tooltip && {
        endAdornment: (
          <InputAdornment position="end" sx={select ? { mr: 2 } : undefined}>
            <Tooltip {...tooltip}>
              <InfoOutlinedIcon fontSize="small" />
            </Tooltip>
          </InputAdornment>
        ),
      }),
    },
  };

  return (
    <MuiTextField
      fullWidth
      select={select}
      slotProps={mergedSlotProps}
      {...rest}
    />
  );
};

export const RhfTextField = <T extends FieldValues>({
  control,
  name,
  ...rest
}: RHFTextFieldProps<T>) => {
  const { field, fieldState } = useController({
    control,
    name,
  });

  return (
    <TextField
      {...field}
      {...rest}
      error={!!fieldState.error}
      helperText={fieldState.error?.message}
    />
  );
};
