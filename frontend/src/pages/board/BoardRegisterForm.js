import {
    Box,
    Button,
    Radio,
    RadioGroup,
    Stack,
    TextField,
} from "@mui/material";

const BoardRegisterForm = () => {
    const typeList = [
        { name: "건의", color: "#0EB4FC", value: "question" },
        { name: "버그제보", color: "#F05650", value: "report" },
    ];
    return (
        <>
            <Box
                bgcolor={"#3D3D3D"}
                color={"white"}
                borderRadius={"10px"}
                padding={"10px"}
                boxShadow={"0px 10px 3px black"}
            >
                <Stack direction={"column"} margin={"0.5rem"}>
                    <Box
                        marginY={"0.5rem"}
                        bgcolor={"white"}
                        borderRadius={"5px 5px 0px 0px"}
                    >
                        <TextField
                            label={"제목"}
                            fullWidth
                            variant="filled"
                        ></TextField>
                    </Box>
                    <Box marginY={"0.5rem"}>
                        <TextField
                            fullWidth
                            multiline
                            minRows={3}
                            placeholder="내용을 입력해주세요."
                            variant="outlined"
                            sx={{
                                backgroundColor: "white",
                                borderRadius: "5px",
                            }}
                        ></TextField>
                    </Box>
                    <Stack
                        direction={"row"}
                        justifyContent={"end"}
                        alignItems={"center"}
                        sx={{ fontWeight: 700 }}
                    >
                        <Box color={"black"} marginX={"1rem"}>
                            <RadioGroup>
                                <Stack direction={"row"}>
                                    {typeList.map((type, index) => (
                                        <Stack direction={"row"} key={index}>
                                            <Radio
                                                value={type.value}
                                                color="primary"
                                            ></Radio>
                                            <Stack
                                                justifyContent={"center"}
                                                alignItems={"center"}
                                            >
                                                <Box
                                                    borderRadius={"20px"}
                                                    minWidth={"5rem"}
                                                    bgcolor={type.color}
                                                    textAlign={"center"}
                                                >
                                                    {type.name}
                                                </Box>
                                            </Stack>
                                        </Stack>
                                    ))}
                                </Stack>
                            </RadioGroup>
                        </Box>
                        <Stack>
                            <Box
                                bgcolor={"#FFD257"}
                                borderRadius={"10px"}
                                color={"black"}
                            >
                                <Button
                                    color="inherit"
                                    fullWidth
                                    sx={{ fontWeight: 700 }}
                                >
                                    등록
                                </Button>
                            </Box>
                        </Stack>
                    </Stack>
                </Stack>
            </Box>
        </>
    );
};

export default BoardRegisterForm;
