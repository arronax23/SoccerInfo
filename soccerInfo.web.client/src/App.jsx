import { BrowserRouter, Routes, Route } from "react-router";
import "./App.css";
import Navbar from "./nav/Navbar";
import LeaguesView from "./views/LeaguesView/LeaguesView";
import LeagueView from "./views/LeagueView/LeagueView";
import TeamView from "./views/TeamView/TeamView";
import BackButton from "./nav/BackButton";
import PlayersView from "./views/PlayersTab/PlayersView"

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <BackButton />
      <Routes>
        <Route path="/" element={<LeaguesView />} />
        <Route path="/league/:leagueId" element={<LeagueView />} />
        <Route path="/team/:teamId" element={<TeamView />} />
        <Route path="/players" element={<PlayersView />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
