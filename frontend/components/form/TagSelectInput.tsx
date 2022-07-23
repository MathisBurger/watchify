import {Chip, Paper, styled, TextField} from "@mui/material";
import React, {Dispatch, SetStateAction, useMemo, useState} from "react";

const ListItem = styled('li')(({ theme }) => ({
    margin: theme.spacing(0.5),
}));

type ChipData =  {key: number, label: string};

interface TagSelectInputProps {
    chipData: string[];
    setChipData:  Dispatch<SetStateAction<string[]>>
}


const TagSelectInput: React.FC<TagSelectInputProps> = ({chipData, setChipData}) => {

    const [input, setInput] = useState<string>('');
    
    const chips = useMemo<ChipData[]>(
        () => chipData.map((v, i) => ({key: i, label: v})),
        [ chipData]
    )

    const handleDelete = (chipToDelete: ChipData) => () => {
        setChipData( chips.filter((chip) => chip.key !== chipToDelete.key).map(v => v.label));
    };
    
    const value = useMemo(
        () => (
            <Paper
                sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    flexWrap: 'wrap',
                    listStyle: 'none',
                    p: 0.5,
                    m: 0,
                }}
                component="ul"
            >
                {chips.map((data) => {

                    return (
                        <ListItem key={data.key}>
                            <Chip
                                label={data.label}
                                onDelete={handleDelete(data)}
                            />
                        </ListItem>
                    );
                })}
            </Paper>
        ),
        [
            chipData
        ]
    );
    
    const updateChips = (e: any) => {
        if (e.key === 'Enter' || e.key === ' ') {
            setChipData([...chipData, input]);
            setInput('');
        }
    }

    return (
        <div style={{display: 'flex', flexDirection: 'column', gap: '5px'}}>
            {chipData.length > 0 ? value : null}
            <TextField 
                value={input} 
                onChange={(e) => setInput(e.target.value)}
                label="Tags"
                fullWidth
                onKeyUp={(e) => updateChips(e)}
            />
        </div>
    );
}

export default TagSelectInput;