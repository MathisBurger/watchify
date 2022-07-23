import {MenuItem, Select, SelectProps} from "@mui/material";
import React from "react";

interface CategorySelectProps {
    value: SelectProps['value'];
    onChange: SelectProps['onChange'];
}

const CategorySelect: React.FC<CategorySelectProps> = ({value, onChange}) => {
    
    const categories = [
        "tech",
        "music",
        "science"
    ]
    
    return (
      <Select label="Category" placeholder="Category" value={value} onChange={onChange} fullWidth>
          {categories.map(category => <MenuItem value={category} key={category}>{category}</MenuItem> )}
      </Select>
    );
}

export default CategorySelect;