import type {NextPage} from 'next'

import styles from '../styles/LoginPage.module.scss';
import TextInput from "../components/form/TextInput";

const Login: NextPage = () => {
    
    return (
        <div className={styles.loginContainer}>
            <TextInput />
        </div>
    );
    
}

export default Login;