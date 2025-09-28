import React, {useState} from "react";
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
} & (MultiSelectProps | SingleSelectProps);

export function Select({multiple, value, onChange, options}: SelectProps) : React.ReactElement {
    const [optionsShown, setOptionsShown] = useState<boolean>(false);
    
    
    function clearOptions() {
        multiple ? onChange([]) : onChange(undefined);
    }
    
    function selectOption(option: SelectOption) {
        if(!multiple) {
            if(option !== value){
                onChange(option);
            }
        }
        else {
            if(value.some(o => o.value === option.value)) {
              
                    console.log("Selected option included", option);
                

                onChange(value.filter(o => o.value !== option.value));
            }
            else{
                onChange([...value, option]);
          
                    console.log("Selected option not included", option);
                    console.log("options currently selected", value)
                
            }
        }
    }
    
    function isOptionSelected(option: SelectOption) {
        return multiple ? value.includes(option) : option === value;
    }
    
    function searchFilter(search: string){
        
    }
    
    
    return(
        <div className={styles.wrapper}
            onBlur={() => setOptionsShown(false)}
            onClick={() => setOptionsShown(prevState => !prevState)} 
             tabIndex={0}
        >
            <span className={styles.value}>{multiple ? (value.map(
                val => (
                    <button key={val.value} onClick={event =>{
                        event.stopPropagation()
                        selectOption(val)
                    }}>
                        {val.label}
                        <span className={styles.removeButton}>&times;</span>
                    </button>
                )
            )) : value?.label}</span>
            <input type={"text"} className={styles.search}/>
            <button className={styles.clearButton} onClick={() => clearOptions()}>&times;</button>
            <ul className={`${styles.options} ${optionsShown ? styles.show : ""}`}>
                {options.map((option) => (
                    <li key={option.value} onClick={() => selectOption(option)}>{option.label}</li>
                ))} 
            </ul>
        </div>
    )
}