import { Box, Button, Container, Stack } from "@mui/material";
import MainPageBackGround from "../../assets/images/mainpage_background.png";
import LogoImg from "../../assets/images/Lost Taste.png";
const MainPage = () => {
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
                <Stack direction={"column"} alignItems={"center"} justifyContent={"center"}>
                    <Box marginY={15} bgcolor={"rgba(0,0,0,0.5)"} padding={10} borderRadius={"20px"}>
                        <img src={LogoImg}></img>
                    </Box>
                    <Box
                        color={"black"}
                        bgcolor={"#FFD257"}
                        boxShadow={0}
                        sx={{
                            borderRadius: "50px",
                            boxShadow: `0px 7px #121212`,
                        }}
                    >
                        <Stack minWidth={"15vw"} direction={"column"} alignItems={"center"} justifyContent={"center"}>
                            <Button
                                color="inherit"
                                fullWidth
                                sx={{
                                    padding: "10px",
                                    borderRadius: "50px",
                                    fontSize: "20px",
                                    fontWeight: 700,
                                }}
                            >
                                <a href="https://j10e107.p.ssafy.io/file/download/LostTaste.zip" download={"LostTaste.zip"}>
                                    게임 다운로드
                                </a>
                            </Button>
                        </Stack>
                    </Box>
                </Stack>
            </Box>
        </>
    );
};

export default MainPage;
