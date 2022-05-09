import React, {ChangeEvent, ChangeEventHandler, HTMLInputTypeAttribute} from 'react';
import styles from '../../styles/components/form/TextInput.module.scss';

interface TextInputProps {
    value: string;
    change: (value: string) => void;
    label?: string;
    type?: HTMLInputTypeAttribute
}

const TextInput: React.FC<TextInputProps> = ({
     value, 
     change, label,
     type = "text"
}) => {
    
    const onChange: ChangeEventHandler = (e: ChangeEvent<HTMLInputElement>) => {
        change(e.target.value);
    }
    
    return (
      <div className={styles.inputContainer}>
          {label ? (
              <div className={styles.label}>{label}</div>
          ) : null}
          <input type={type} value={value} onChange={onChange} />
      </div>  
    );
}

export default TextInput;