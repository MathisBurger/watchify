import React, {PropsWithChildren, useEffect} from "react";
import useApiService from "../hooks/useApiService";
import {useRouter} from "next/router";



const AuthorizationWrapper: React.FC<PropsWithChildren<any>> = ({children}) => {
    
    const api = useApiService();
    const router = useRouter();
    
    useEffect(() => {
        api.me()
            .then((data) => {
                if (!data) {
                    router.push('/login');
                }
            })
            .catch(() => router.push('/login'));
    }, []);
    
    return (
      <>
          {children}
      </>  
    );
}

export default AuthorizationWrapper;