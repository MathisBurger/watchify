import RestService from "./RestService";
import {RegisterResponse} from "../types/responses/RegisterResponse";

const ORIGIN = 'https://localhost:7220'

class ApiService extends RestService {
    
    public async login(username: string, password: string): Promise<void> {
        return await this.post<any>(`${ORIGIN}/Auth/Login`, {
            username,
            password
        }, true);
    }
    
    public async register(username: string, password: string): Promise<RegisterResponse> {
        return await this.post<RegisterResponse>(`${ORIGIN}/Auth/Register`, {
            username, 
            password,
        })
    }
}

export default ApiService;