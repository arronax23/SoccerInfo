import { BrowserRouter, Routes, Route } from "react-router";
import "./App.css";
import Navbar from "./nav/Navbar";
import LeaguesView from "./views/LeaguesView/LeaguesView";
import HomeView from "./views/HomeView/HomeView";
import LeagueView from "./views/LeagueView/LeagueView";
import TeamView from "./views/TeamView/TeamView";
import BackButton from "./nav/BackButton";
import SearchView from "./views/SearchTab/SearchView"
import PlayerDetailsView from "./views/PlayerDetailsView/PlayerDetailsView";
import StatsView from "./views/StatsTab/StatsView";

function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <BackButton />
      <Routes>
        <Route path="/" element={<HomeView />} />
        <Route path="/leagues" element={<LeaguesView />} />
        <Route path="/league/:leagueId" element={<LeagueView />} />
        <Route path="/team/:teamId" element={<TeamView />} />
        <Route path="/search" element={<SearchView />} />
        <Route path="/player/:playerId" element={<PlayerDetailsView />} />
        <Route path="/stats" element={<StatsView />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
