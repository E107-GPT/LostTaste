import { Box, Stack } from "@mui/material";
import MainPageBackGround from "../../assets/images/mainpage_background.png";
const RankingPage = () => {
    //     {
    //     "partyName": string,
    //     "memberCount": number,
    //     "playTime": number, // 초 단위
    //     "rngSeed": number,
    // }
    return (
        <>
            <Box
                width={"100%"}
                sx={{
                    width: "100%", // 너비 설정
                    height: "90vh", // 높이 설정
                    backgroundImage: `url(${MainPageBackGround})`, // 배경 이미지 URL 설정
                    backgroundSize: "cover",
                    backgroundPosition: "center", // 이미지 위치 설정
                }}
            >
                <Stack
                    direction={"column"}
                    alignItems={"center"}
                    justifyContent={"center"}
                >
                    <Box
                        marginY={"3vh"}
                        bgcolor={"rgba(0,0,0,0.5)"}
                        padding={"1%"}
                        paddingTop={"3%"}
                        borderRadius={"20px"}
                    >
                        <Box>
                            <Box>1</Box>
                            <Box>파티이름</Box>
                            <Box>인원</Box>
                            <Box>시간</Box>
                        </Box>
                    </Box>
                </Stack>
            </Box>
        </>
    );
};

export default RankingPage;
