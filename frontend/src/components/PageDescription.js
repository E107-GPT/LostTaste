import { Box, Stack } from "@mui/material";

const PageDescription = ({ children, title, ...rest }) => {
    return (
        <>
            <Box
                bgcolor={"#3D3D3D"}
                borderRadius={"10px"}
                color={"white"}
                padding={"1%"}
                boxShadow={"0px 10px 3px black"}
            >
                <Stack direction={"row"} height={"10vh"}>
                    <Stack
                        flex={1}
                        justifyContent={"center"}
                        alignItems={"center"}
                        borderRight={"5px solid #FFD257"}
                        fontWeight={700}
                        fontSize={"35px"}
                    >
                        <Box>{title}</Box>
                    </Stack>
                    <Stack
                        direction={"row"}
                        marginLeft={"1rem"}
                        flex={5}
                        justifyContent={"start"}
                        alignItems={"center"}
                        fontSize={"20px"}
                    >
                        <Box>{children}</Box>
                    </Stack>
                </Stack>
            </Box>
        </>
    );
};

export default PageDescription;
