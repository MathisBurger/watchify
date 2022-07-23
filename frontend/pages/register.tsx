import type {NextPage} from "next";
import styles from "../styles/RegisterPage.module.scss";
import TextInput from "../components/form/TextInput";
import Button from "../components/Button";
import useApiService from "../hooks/useApiService";
import {useState} from "react";
import {useRouter} from "next/router";
import {useSnackbar} from "notistack";


const Register: NextPage = () => {

    const api = useApiService();
    const router = useRouter();
    const {enqueueSnackbar} = useSnackbar();
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    
    const register = async () => {
        try {
            await api.register(username, password);
            await router.push("/login");
        } catch (e) {
            enqueueSnackbar("Registration failed", {variant: "error"});
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
                <Button label="Register" onClick={register} />
                <h5 className={styles.registerLink} onClick={() => router.push("/login")}>Login</h5>
            </div>
        </div>
    );
}

export default Register;