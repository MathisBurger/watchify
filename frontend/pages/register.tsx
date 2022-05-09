import type {NextPage} from "next";
import styles from "../styles/RegisterPage.module.scss";
import TextInput from "../components/form/TextInput";
import Button from "../components/Button";
import useApiService from "../hooks/useApiService";
import {useState} from "react";


const Register: NextPage = () => {

    const api = useApiService();
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    
    const register = async () => {
        try {
            await api.register(username, password);
        } catch (e) {
            
        }
    }
    
    return (
        <div className={styles.registerContainer}>
            <div className={styles.registerBox}>
                <h1>Register</h1>
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
                <Button label="Login" onClick={register} />
            </div>
        </div>
    );
}

export default Register;