import { Control, FieldPath, FieldValues } from "react-hook-form";

export type InputControlProps<T extends FieldValues> = {
  control?: Control<T>;
  name: FieldPath<T>;
  onValueChange?: (value: string) => void;
};
