import type {NextPage} from "next";
import {useRouter} from "next/router";
import {useEffect, useState} from "react";
import {ORIGIN} from "../../services/ApiService";
import styles from "../../styles/WatchPage.module.scss";


const Watch: NextPage = () => {
    const router = useRouter();
    
    const {videoId} = router.query;
    const [videoUrl, setVideoUrl] = useState<string>('');
    
    useEffect(() => {
       if (videoId && videoUrl === '') {
           fetch(``)
               .then(res => res.blob())
               .then(data => {
                   const url = URL.createObjectURL(data);
                   setVideoUrl(url);
               });
       } 
    });
    return (
        <video controls autoPlay className={styles.videoPlayer}>
            <source 
                src={`${ORIGIN}/Player/Watch?videoId=c9e7fd73-432b-4399-9301-631d3f60569e`}
                type="video/mp4"
            />
        </video>
    );
}

export default Watch;