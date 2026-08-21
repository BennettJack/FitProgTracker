import {
  Autocomplete as MuiAutocomplete,
  AutocompleteProps as MuiAutocompleteProps,
} from "@mui/material";

import { TextField, TextFieldProps } from "./TextField";

export type AutoCompleteProps<
  T,
  Multiple extends boolean = false,
  DisableClearable extends boolean = false,
  FreeSolo extends boolean = false,
> = MuiAutocompleteProps<T, Multiple, DisableClearable, FreeSolo> & {
  textFieldProps?: TextFieldProps;
};

export function AutoComplete<
  T,
  Multiple extends boolean = false,
  DisableClearable extends boolean = false,
  FreeSolo extends boolean = false,
>({
  textFieldProps,
  ...props
}: AutoCompleteProps<T, Multiple, DisableClearable, FreeSolo>) {
  return (
    <MuiAutocomplete
      {...props}
      renderInput={(params) => <TextField {...params} {...textFieldProps} />}
    />
  );
}
