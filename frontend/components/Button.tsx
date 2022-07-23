import React from "react";
import styles from '../styles/components/Button.module.scss';

interface ButtonProps {
    label: string;
    onClick: () => void;
}

const Button: React.FC<ButtonProps> = ({label, onClick}) => {
    
    return (
        <button onClick={() => onClick()} className={styles.customButton}>
            {label}
        </button>
    );
}

export default Button;