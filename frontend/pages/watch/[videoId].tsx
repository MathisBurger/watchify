import type {NextPage} from "next";
import {useRouter} from "next/router";
import {useEffect, useState} from "react";
import {ORIGIN} from "../../services/ApiService";


const Watch: NextPage = () => {
    const router = useRouter();
    
    const {videoId} = router.query;
    const [videoUrl, setVideoUrl] = useState<string>('');
    
    useEffect(() => {
       if (videoId && videoUrl === '') {
           fetch(`${ORIGIN}/Player/Watch?videoId=${videoId}`)
               .then(res => res.blob())
               .then(data => {
                   const url = URL.createObjectURL(data);
                   setVideoUrl(url);
               });
       } 
    });
    return (
        <video>
            <source src={videoUrl}/>
        </video>
    )
}

export default Watch;