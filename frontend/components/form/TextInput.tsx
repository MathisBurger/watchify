import React from 'react';
import styles from '../../styles/components/form/TextInput.module.scss';

const TextInput: React.FC = () => {
    
    return (
      <div className={styles.inputContainer}>
          <div className={styles.label}>Moin:</div>
          <input type="text" />
      </div>  
    );
}

export default TextInput;