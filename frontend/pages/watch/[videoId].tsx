import type {NextPage} from "next";
import {useRouter} from "next/router";
import {useEffect, useState} from "react";
import {ORIGIN} from "../../services/ApiService";
import styles from "../../styles/WatchPage.module.scss";
import useApiService from "../../hooks/useApiService";
import LikeBox, {LikeType} from "../../components/form/LikeBox";
import {VideoLikeStatus} from "../../types/responses/VideoLikeStatus";
import {Box} from "@mui/material";


const Watch: NextPage = () => {
    
    const router = useRouter();
    const apiService = useApiService();
    
    const {videoId} = router.query;
    const [videoUrl, setVideoUrl] = useState<string>('');
    const [metaData, setMetaData] = useState<any>(null);
    const [likedStatus, setLikedStatus] = useState<VideoLikeStatus>({liked: false, disliked: false});
    
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
    
    useEffect(() => {
        const fetcher = async () => {
            if (videoId) {
                setMetaData(await apiService.getVideoMeta(videoId as string));
                setLikedStatus(await apiService.getLikedStatus(videoId as string));
            }
        };
        fetcher();
    }, [videoId]);
    
    const likeVideo = async () => {
        const currentUser = await apiService.me();
        if (!currentUser) {
            alert("You are not logged in");
            return;
        }
        await apiService.likeVideo(videoId as string);
        setLikedStatus(await apiService.getLikedStatus(videoId as string));
    }

    const dislikeVideo = async () => {
        const currentUser = await apiService.me();
        if (!currentUser) {
            alert("You are not logged in");
            return;
        }
        await apiService.dislikeVideo(videoId as string);
        setLikedStatus(await apiService.getLikedStatus(videoId as string));
    }
    
    if (metaData == null) return <></>;
    return (
        <div className={styles.wrappedContainer}>
            <div className={styles.videoPlayerContainer}>
                <video controls autoPlay className={styles.videoPlayer}>
                    <source
                        src={`${ORIGIN}/Player/Watch?videoId=${videoId}`}
                        type="video/mp4"
                    />
                </video>
                <div className={styles.contentContainer}>
                    <h1>{metaData.title}</h1>
                    <h3>{metaData.views} Views</h3>
                    <div className={styles.likeRow}>
                        <Box onClick={likeVideo}>
                            <LikeBox likeType={LikeType.Like} checked={likedStatus.liked} count={metaData.likes} />
                        </Box>
                        <Box onClick={dislikeVideo}>
                            <LikeBox likeType={LikeType.Dislike} checked={likedStatus.disliked} count={metaData.dislikes} />
                        </Box>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Watch;