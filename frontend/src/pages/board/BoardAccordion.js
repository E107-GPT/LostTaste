import { Box, Fade, Stack, Typography } from "@mui/material";
import Accordion from "@mui/material/Accordion";
import AccordionDetails from "@mui/material/AccordionDetails";
import AccordionSummary from "@mui/material/AccordionSummary";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import { useEffect, useState } from "react";
import axios from "axios";

const BoardAccordion = () => {
    const typeList = [
        { name: "건의", color: "#0EB4FC", value: "question" },
        { name: "버그제보", color: "#F05650", value: "report" },
    ];

    const getBoard = async () => {
        const response = await axios.get("https://j10e107.p.ssafy.io/api/board?limit=10");
        const data = response.data;

        const newList = [];
        data.map((e) => {
            const obj = {
                num: e.id,
                type: e.categoryCode === "BCT_0001" ? "question" : "report",
                title: e.title,
                content: "",
            };
            newList.push();
        });
    };

    useEffect(() => {
        getBoard();
    }, []);

    const [boardList, setBoardList] = useState([
        {
            num: 1,
            type: "question",
            title: "123",
            content: "Content!!!",
            date: "2024-03-07",
            status: "solved",
        },
        {
            num: 2,
            type: "report",
            title: "123",
            content: "Content!!!",
            date: "2024-03-07",
            status: "solved",
        },
        {
            num: 3,
            type: "question",
            title: "123",
            content: "Content!!!",
            date: "2024-03-07",
            status: "solved",
        },
        {
            num: 4,
            type: "question",
            title: "123",
            content: "Content!!!",
            date: "2024-03-07",
            status: "unsolved",
        },
    ]);
    return (
        <>
            <Box bgcolor={"#F4F4F4"} width={"100%"} paddingY={"0.5rem"} borderRadius={"10px 10px 0px 0px "} borderBottom={"3px solid black"}>
                <Stack direction={"row"} alignItems={"center"} textAlign={"center"} fontWeight={700}>
                    <Box flex={1}>번호</Box>
                    <Box flex={1}>종류</Box>
                    <Box flex={3}>제목</Box>
                    <Box flex={1}>날짜</Box>
                    <Box flex={1}>상태</Box>
                </Stack>
            </Box>
            {boardList.map((board, index) => (
                <Accordion key={index} sx={{ padding: 0, fontWeight: 700 }}>
                    <AccordionSummary aria-controls="panel2-content" id="panel2-header" sx={{ padding: 0 }}>
                        <Stack direction={"row"} alignItems={"center"} textAlign={"center"} width={"100%"}>
                            <Typography flex={1}>{board.num}</Typography>
                            <Box flex={1}>
                                <Stack direction={"row"} justifyContent={"center"}>
                                    <Box borderRadius={"20px"} width={"7rem"} bgcolor={board.type === "question" ? "#0EB4FC" : "#F05650"} textAlign={"center"}>
                                        {board.type === "question" ? "건의" : "버그제보"}
                                    </Box>
                                </Stack>
                            </Box>
                            <Typography flex={3}>{board.title}</Typography>
                            <Typography flex={1}>{board.date}</Typography>
                            <Box flex={1}>
                                <Stack direction={"row"} justifyContent={"center"}>
                                    <Box borderRadius={"20px"} width={"7rem"} bgcolor={board.status === "solved" ? "#3FE87F" : "#B5B5B5"} textAlign={"center"}>
                                        {board.status === "solved" ? "해결" : "미해결"}
                                    </Box>
                                </Stack>
                            </Box>
                        </Stack>
                    </AccordionSummary>
                    <AccordionDetails>
                        <Typography>{board.content}</Typography>
                    </AccordionDetails>
                </Accordion>
            ))}
        </>
    );
};

export default BoardAccordion;
