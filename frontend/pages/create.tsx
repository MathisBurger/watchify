import {NextPage} from "next";
import {Button, Grid, TextField} from "@mui/material";
import FormTextField from "../components/form/FormTextField";
import {useState} from "react";
import {CreateVideo} from "../types/requests/CreateVideo";
import styles from '../styles/CreatePage.module.scss';
import TagSelectInput from "../components/form/TagSelectInput";
import CategorySelect from "../components/form/CategorySelect";

const defaultVideo: CreateVideo = {
    title: '',
    description: '',
    tags: [],
    category: ''
};

const CreatePage: NextPage = () => {
    
    const [video, setVideo] = useState<CreateVideo>(defaultVideo);
    const [chipData, setChipData] = useState<string[]>([]);
    const [files, setFiles] = useState<any[]>([]);
    
    const upload = async () => {
        const vid = Object.assign({}, video);
        vid.tags = chipData;
    }
    
    return (
        <div className={styles.wrapper}>
            <div className={styles.container}>
                <h1 className={styles.header}>Create Video</h1>
                <FormTextField
                    value={video.title}
                    variant="standard"
                    onChange={(e) =>
                        setVideo({...video, title: e.target.value})}
                    label="Title"
                />
                <FormTextField
                    value={video.description}
                    variant="standard"
                    onChange={(e) =>
                        setVideo({...video, description: e.target.value})}
                    label="Description"
                />
                <div className={styles.inputField}>
                    <div className={styles.halfInputField}>
                        <TagSelectInput  chipData={chipData} setChipData={setChipData} />
                    </div>
                    <div className={styles.halfInputField}>
                        <CategorySelect value={video.category} onChange={(e) =>
                            setVideo({...video, category: '' + e.target.value})} />
                        <TextField 
                            type="file"
                            fullWidth
                            onChange={(e) => setFiles(e.target.files)} />
                    </div>
                </div>
                <Button style={{width: "25%"}} variant="contained" color="primary" onClick={upload}>
                    Upload
                </Button>
            </div>
        </div>
    );
}

export default CreatePage;