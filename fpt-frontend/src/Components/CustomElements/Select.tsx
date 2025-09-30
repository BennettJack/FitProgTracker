import React, {useEffect, useState, useRef} from "react";
import styles from "./Select.module.css"


export type SelectOption = {
    label: string;
    value: string;
}
type SingleSelectProps = {
    multiple?: false;
    value?: SelectOption;
    onChange: (value: SelectOption | undefined) => void;
}

type MultiSelectProps = {
    multiple: true
    value: SelectOption[];
    onChange: (value: SelectOption []) => void;
}
type SelectProps = {
    options: SelectOption[];
    index: number;
} & (MultiSelectProps | SingleSelectProps);

export function Select({multiple, value, onChange, options, index}: SelectProps) : React.ReactElement {
    const [optionsShown, setOptionsShown] = useState<boolean>(false);
    const [availableOptions, setAvailableOptions] = useState<SelectOption[]>(options);
    const [optionsToDisplay, setOptionsToDisplay] = useState<SelectOption[]>(options);
    const [search, setSearch] = useState<string>('');

    const searchRef = useRef<HTMLInputElement>(null);
    
    useEffect(() => {
        if(optionsShown){
            searchRef.current?.focus();
        }
    }, [optionsShown])

    useEffect(() => {
        
        if (multiple) {
            setAvailableOptions(options.filter(o => !value.some(v => v.value === o.value)));
            
        } else {
            setAvailableOptions(options);
        }
    }, [options, value, multiple]);
    
    useEffect(() =>{
        setOptionsToDisplay(availableOptions);
    }, [availableOptions])
    function clearOptions() {
        multiple ? onChange([]) : onChange(undefined);
        setAvailableOptions(options);
    }

    function selectOption(option: SelectOption) {
        if (!multiple) {
            if (option !== value) {
                onChange(option);
            }
            setOptionsShown(false);
        } else {
            if (value.some(o => o.value === option.value)) {
                console.log("add")
                onChange(value.filter(o => o.value !== option.value));
            } else {
                console.log("remove")
                onChange([...value, option]);
            }
        }
        setSearch("");
    }
    
    function isOptionSelected(option: SelectOption) {
        return multiple ? value.includes(option) : option === value;
    }
    
    function filterOptions(query: string) {
        const filteredOptions = availableOptions.filter(option =>
            option.label.toLowerCase().includes(query.toLowerCase()));
        setOptionsToDisplay(filteredOptions);
    }
    
    return(
        <div className={styles.wrapper}
            onBlur={(e) => {
                if(!e.currentTarget.contains(e.relatedTarget)){
                    setOptionsShown(false)}
                }
            }
            onClick={() => setOptionsShown(prevState => !prevState)} 
             tabIndex={index}
        >
            <span 
                className={styles.value}
            >
                {multiple ? (value.map(
                val => (
                    <button 
                        key={val.value} 
                        onClick={event =>
                        {
                            event.stopPropagation()
                            selectOption(val)
                        }}
                    >
                        {val.label}
                        <span 
                            className={styles.removeButton}
                        >
                            &times;
                        </span>
                        
                    </button>
                ))) : value?.label}
                <input
                    type={"text"}
                    ref={searchRef}
                    className={styles.search}
                    value={search}
                    onChange={(e) => {
                        filterOptions(e.target.value);
                        setSearch(e.target.value);
                    }}
                />
            </span>
            
            
            
            <button 
                className={styles.clearButton} 
                onClick={() => clearOptions()}
            >
                &times;
            </button>

            <div className={styles.divider}></div>
            <div className={styles.caret}></div>
            
            <ul 
                className={`${styles.options} ${optionsShown ? styles.show : ""}`}
            >
                {optionsToDisplay.map((option) => (
                    <li 
                        key={option.value} 
                        onClick={() => selectOption(option)}
                        className={styles.option}
                    >
                        {option.label}
                    </li>
                ))} 
            </ul>
            
        </div>
    )
}