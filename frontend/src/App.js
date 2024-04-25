import logo from "./logo.svg";
import "./App.css";
import { Routes, Route } from "react-router-dom";

import MainPage from "./pages/main";
import BoardPage from "./pages/board";
import NavBar from "./components/NavBar";
import RankingPage from "./pages/ranking";

function App() {
    return (
        <>
            <NavBar></NavBar>
            <Routes>
                <Route path="" element={<MainPage></MainPage>}></Route>
                <Route path="/board" element={<BoardPage></BoardPage>}></Route>
                <Route
                    path="/ranking"
                    element={<RankingPage></RankingPage>}
                ></Route>
            </Routes>
        </>
    );
}

export default App;
