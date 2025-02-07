import { useEffect, useState } from "react";
import { useParams } from "react-router";
import PlayerGeneralInfo from "./PlayerGeneralInfo";
import PlayerAdditionalInfo from "./PlayerAdditionalInfo";
import MarketValueProgress from "./MarketValueProgress";
import PlayerStats from "./PlayerStats";

const PlayerDetailsView = () => {
  const { playerId } = useParams();
  const [playerGeneralInfo, setPlayerGeneralInfo] = useState();
  const [marketValueChanges, setMarketValueChanges] = useState();
  const [playerAdditionalInfo, setPlayerAdditionalInfo] = useState();
  const [isGoalKeeper, setIsGoalKeeper] = useState(false);
  const [stats, setStats] = useState();

  useEffect(() => {
    getPlayerDetails();
    getPlayerCharacteristics();
  }, []);

  return (
    <div className="player-details-container">
      {playerGeneralInfo && (
        <PlayerGeneralInfo playerGeneralInfo={playerGeneralInfo} />
      )}
      {playerAdditionalInfo && (
        <PlayerAdditionalInfo playerAdditionalInfo={playerAdditionalInfo} />
      )} 
      {stats && (
        <PlayerStats isGoalkeeper={isGoalKeeper} stats={stats} />
      )}
      {marketValueChanges && (
        <MarketValueProgress marketValueChanges={marketValueChanges} />
      )}
    </div>
  );

  async function getPlayerDetails() {
    try {
      const response = await fetch(`/api/GetPlayerDetails/${playerId}`);
      const data = await response.json();

      if (data.marketValueChanges != null) {
          setMarketValueChanges(data.marketValueChanges);
      }

      delete data.marketValueChanges;
      setPlayerGeneralInfo(data);
    } catch (error) {
      console.error("Failed to fetch player details:", error);
    }
  }

  async function getPlayerCharacteristics() {
    try {
      const response = await fetch(`/api/GetPlayerCharacteristics/${playerId}`);
      const data = await response.json();
      console.log(data);
      console.log(data.isGoalkeeper)

      setIsGoalKeeper(data.isGoalkeeper);

      if (data.isGoalkeeper){
        setStats(data.goalKeeperStats);
      }
      else {
        setStats(data.outfieldPlayerStats);
      }

      preparePlayerAdditionalInfo(data);
    } catch (error) {
      console.error("Failed to fetch player characteristics:", error);
    }
  }

  function preparePlayerAdditionalInfo(data){
    delete data.isGoalkeeper;
    delete data.outfieldPlayerStats;
    delete data.goalKeeperStats;
    setPlayerAdditionalInfo(data)
  }
};

export default PlayerDetailsView;
