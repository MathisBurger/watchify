import React from "react";
import styles from "../../styles/components/form/LikeBox.module.scss";
import {ThumbDown, ThumbUp} from "@mui/icons-material";

export enum LikeType {
    Like,
    Dislike
}

interface LikeBox {
    likeType: LikeType; 
    checked: boolean;
    count: number;
}

const LikeBox: React.FC<LikeBox> = ({likeType, checked, count}) => {
    
    
    switch (likeType) {
        case LikeType.Dislike:
            return (
                <div className={styles.likeBox}>
                    <ThumbDown  color={checked ? 'primary' : undefined} fontSize="large" />
                    <h4>{count}</h4>
                </div>
            );
        case LikeType.Like:
            return (
                <div className={styles.likeBox}>
                    <ThumbUp color={checked ? 'primary' : undefined} fontSize="large" />
                    <h4>{count}</h4>
                </div>
            )
    }
}

export default LikeBox;