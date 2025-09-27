import React, {useState} from "react";


interface SelectOption {
    label: string;
    value: string;
}

type SelectProps = {
    options: SelectOption[];
    value?: SelectOption[];
    onChange?: (value: SelectOption | undefined) => void;
}

export function Select({value, onChange, options}: SelectProps) : React.ReactElement {
    const [optionsShown, setOptionsShown] = useState<boolean>(false);
    
    
    return(
        <div
            onBlur={() => setOptionsShown(false)}
            onClick={() => setOptionsShown(prevState => !prevState)}
        >
            <span>Value</span>
            <button>&times;</button>
            <ul>
                {options.map((option) => (
                    <li key={option.value}>{option.label}</li>
                ))} 
            </ul>
        </div>
    )
}