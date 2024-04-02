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
                    <Box width={"33.3%"}></Box>
                    <Box width={"33.3%"}>
                        <Stack
                            direction={"row"}
                            justifyContent={"center"}
                            alignItems={"center"}
                        >
                            <img src={LogoImg} height={"50vh"}></img>
                        </Stack>
                    </Box>
                    <Box width={"33.3%"}></Box>
                </Stack>
            </Box>
        </>
    );
};

export default NavBar;
