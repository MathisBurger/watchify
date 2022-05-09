import type {NextPage} from 'next'

import styles from '../styles/LoginPage.module.scss';
import TextInput from "../components/form/TextInput";
import {useState} from "react";
import Button from "../components/Button";
import useApiService from "../hooks/useApiService";

const Login: NextPage = () => {
    
    const api = useApiService();
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    
    const login = async () => {
        const response = await api.login(username, password);
        console.log(response);
    }
    
    return (
        <div className={styles.loginContainer}>
            <div className={styles.loginBox}>
                <h1>Login</h1>
                <TextInput 
                    value={username}
                    change={(value) => setUsername(value)}
                    label="Username:"
                />
                <TextInput
                    value={password}
                    change={(value) => setPassword(value)} 
                    label="Password:" 
                    type="password" 
                />
                <Button label="Login" onClick={login} />
            </div>
        </div>
    );
    
}

export default Login;