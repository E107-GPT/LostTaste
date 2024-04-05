import { Box, Stack } from "@mui/material";
import MainPageBackGround from "../../assets/images/mainpage_background.png";
import { useEffect, useState } from "react";
import firstIcon from "../../assets/images/first.png";
import secondIcon from "../../assets/images/second.png";
import thirdIcon from "../../assets/images/third.png";
import axios from "axios";

const RankingPage = () => {
    //     {
    //     "partyName": string,
    //     "memberCount": number,
    //     "playTime": number, // 초 단위
    //     "rngSeed": number,
    // }
    const [partyList, setPartyList] = useState([1, 2, 3, 4, 5]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(
                    "https://j10e107.p.ssafy.io/api/ranking?limit=10"
                );
                console.log(response.data.records);
                setPartyList(response.data.records);
            } catch (e) {
                console.log(e);
            }
        };

        fetchData();
    }, []);
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
                        bgcolor={"rgba(0,0,0,0.9)"}
                        padding={"1%"}
                        borderRadius={"10px"}
                        width={"50vw"}
                    >
                        <Stack
                            borderBottom={"3px solid #FFD257"}
                            direction={"row"}
                            padding={"1rem"}
                            fontWeight={"700"}
                            color={"white"}
                        >
                            <Box width={"10%"} textAlign={"center"}>
                                등수
                            </Box>
                            <Box width={"50%"} textAlign={"center"}>
                                파티이름
                            </Box>
                            <Box width={"40%"} textAlign={"center"}>
                                시간
                            </Box>
                        </Stack>
                        {partyList.map((party, i) => (
                            <Box key={i}>
                                <Stack
                                    borderBottom={
                                        "3px solid rgba(255,210, 87, 0.6)"
                                    }
                                    direction={"row"}
                                    padding={"1rem"}
                                    fontWeight={"500"}
                                    color={"white"}
                                    height={"4vh"}
                                    alignItems={"center"}
                                >
                                    <Box width={"10%"} textAlign={"center"}>
                                        {i === 0 ? (
                                            <img src={firstIcon}></img>
                                        ) : (
                                            ""
                                        )}
                                        {i === 1 ? (
                                            <img src={secondIcon}></img>
                                        ) : (
                                            ""
                                        )}
                                        {i === 2 ? (
                                            <img src={thirdIcon}></img>
                                        ) : (
                                            ""
                                        )}
                                        {i > 2 ? i + 1 : ""}
                                    </Box>
                                    <Box width={"50%"} textAlign={"center"}>
                                        {party.partyName}
                                    </Box>
                                    <Box width={"40%"} textAlign={"center"}>
                                        {`${parseInt(
                                            party.playTimeSec / 60
                                        )}분 ${
                                            parseInt(party.playTimeSec % 60) +
                                            Number(
                                                (party.playTimeSec % 1).toFixed(
                                                    3
                                                )
                                            )
                                        }초`}
                                    </Box>
                                </Stack>
                            </Box>
                        ))}
                    </Box>
                </Stack>
            </Box>
        </>
    );
};

export default RankingPage;
