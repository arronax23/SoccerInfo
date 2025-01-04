import React, { useEffect, useState } from "react";
import MarketValueProgress from "./MarketValueProgress";
import { useParams } from "react-router";
import PlayerGeneralInfo from "./PlayerGeneralInfo";

const PlayerDetailsView = () => {
  const { playerId } = useParams();
  const [playerGeneralInfo, setPlayerGeneralInfo] = useState();
  const [marketValueChanges, setMarketValueChanges] = useState();

  useEffect(() => {
    getPlayerDetails();
  }, []);

  return (
    <div className="player-details-container">
      {playerGeneralInfo && (
        <PlayerGeneralInfo playerGeneralInfo={playerGeneralInfo} />
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
      console.log(data);

      if (data.marketValueChanges != null) {
          setMarketValueChanges(data.marketValueChanges);
      }

      delete data.marketValueChanges;
      setPlayerGeneralInfo(data);
    } catch (error) {
      console.error("Failed to fetch player details:", error);
    }
  }
};

export default PlayerDetailsView;
