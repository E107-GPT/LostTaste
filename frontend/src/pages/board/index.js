import { Box, Container, Stack } from "@mui/material";
import PageDescription from "../../components/PageDescription";
import BoardRegisterForm from "./BoardRegisterForm";
import BoardAccordion from "./BoardAccordion";

const BoardPage = () => {
    return (
        <>
            <Box>
                <Stack direction={"column"} alignItems={"center"}>
                    <Stack
                        direction={"row"}
                        alignItems={"center"}
                        justifyContent={"center"}
                    >
                        <Box minWidth={"80vw"} marginY={"1rem"}>
                            <PageDescription title={"게시판"}>
                                무엇이든 적어주세요.
                            </PageDescription>
                        </Box>
                    </Stack>
                    <Box minWidth={"80vw"}>
                        <BoardRegisterForm></BoardRegisterForm>
                    </Box>
                    <Box marginTop={"1.5rem"} width={"80vw"}>
                        <BoardAccordion></BoardAccordion>
                    </Box>
                </Stack>
            </Box>
        </>
    );
};

export default BoardPage;
