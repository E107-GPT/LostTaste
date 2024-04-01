import { Button, Fade } from "@mui/material";
import Box from "@mui/material/Box";
import Stack from "@mui/material/Stack";
import { useState } from "react";
import { Link, NavLink, useNavigate } from "react-router-dom";
import LogoImg from "../assets/images/Lost Taste.png";

const NavBar = () => {
    const currentPage = window.location.pathname.substring(1);
    const [curPage, setCurPage] = useState(
        currentPage === "" ? "home" : currentPage
    );

    const pageList = [{ pageName: "home", path: "/", content: "HOME" }];
    const navigate = useNavigate();

    return (
        <>
            <Box bgcolor={"#121212"}>
                <Stack
                    alignItems={"center"}
                    direction={"row"}
                    color={"white"}
                    minHeight={"10vh"}
                >
                    <Box width={"33.3%"}>
                        <img src={LogoImg} height={"50vh"}></img>
                    </Box>
                    <Box width={"33.3%"}>
                        <Stack
                            direction={"row"}
                            justifyContent={"center"}
                            alignItems={"center"}
                        >
                            {pageList.map((obj, index) => (
                                <Box
                                    key={index}
                                    width="30%"
                                    textAlign={"center"}
                                    borderBottom={
                                        curPage === obj.pageName
                                            ? "3px solid #FFD257"
                                            : "3px solid transparent"
                                    }
                                >
                                    <Button
                                        color="inherit"
                                        onClick={() => {
                                            navigate(obj.path);
                                            setCurPage(obj.pageName);
                                        }}
                                        size={"large"}
                                        fullWidth
                                    >
                                        <Box sx={{ fontSize: "20px" }}>
                                            {obj.content}
                                        </Box>
                                    </Button>
                                </Box>
                            ))}
                        </Stack>
                    </Box>
                    <Box width={"33.3%"}></Box>
                </Stack>
            </Box>
        </>
    );
};

export default NavBar;
