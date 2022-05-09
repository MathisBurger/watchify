import type {NextPage} from 'next'

import styles from '../styles/LoginPage.module.scss';
import TextInput from "../components/form/TextInput";
import {useState} from "react";
import Button from "../components/Button";
import useApiService from "../hooks/useApiService";
import {useRouter} from "next/router";
import {useSnackbar} from "notistack";

const Login: NextPage = () => {
    
    const api = useApiService();
    const router = useRouter();
    const {enqueueSnackbar} = useSnackbar();
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    
    const login = async () => {
        try {
            await api.login(username, password);
            await router.push('/dashboard');
        } catch (e) {
            enqueueSnackbar("Login failed", {variant: "error"});
        }
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
                <h5 className={styles.loginLink} onClick={() => router.push("/register")}>Register</h5>
            </div>
        </div>
    );
    
}

export default Login;