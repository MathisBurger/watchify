import RestService from "./RestService";
import {RegisterResponse} from "../types/responses/RegisterResponse";
import {CreateVideo} from "../types/requests/CreateVideo";

const ORIGIN = 'https://localhost:7220'

class ApiService extends RestService {
    
    public async login(username: string, password: string): Promise<void> {
        return await this.post<any>(`${ORIGIN}/Auth/Login`, JSON.stringify({
            username,
            password
        }), true);
    }
    
    public async register(username: string, password: string): Promise<RegisterResponse> {
        return await this.post<RegisterResponse>(`${ORIGIN}/Auth/Register`, JSON.stringify({
            username, 
            password,
        }))
    }
    
    public async me(): Promise<any> {
        return await this.get<any>(`${ORIGIN}/User/Me`);
    }
    
    public async createVideo(input: CreateVideo): Promise<any> {
        return await this.post<any>(`${ORIGIN}/Video/CreateVideo`, JSON.stringify(input));
    }
    
    public async uploadVideo(formData: FormData, videoID: string): Promise<any> {
        return await this.post<any>(`${ORIGIN}/Video/UploadVideo?videoId=${videoID}`, formData, false, 'image/png');
    }
}

export default ApiService;