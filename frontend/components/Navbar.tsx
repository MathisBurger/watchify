import React from "react";
import styles from "../styles/components/Navbar.module.scss";
import Image from "next/image";

const Navbar: React.FC = () => {
    
    return (
        <div className={styles.navbar}>
            <img src="/favicon.ico" className={styles.logo}  alt={"logo"}/>
        </div>
    );
}

export default Navbar;