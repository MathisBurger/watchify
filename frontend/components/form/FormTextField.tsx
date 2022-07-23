import {Grid, GridSize, TextField, TextFieldProps} from "@mui/material";
import React from "react";


interface FormTextFieldProps {
    value: TextFieldProps['value'];
    variant: TextFieldProps['variant'];
    onChange: TextFieldProps['onChange'];
    label: TextFieldProps['label'];
}

const FormTextField: React.FC<FormTextFieldProps> = ({value, variant, onChange, label}) => {
    
    return (
            <TextField
                variant={variant}
                value={value}
                onChange={onChange}
                label={label}
                fullWidth
            />
    );
}

export default FormTextField;