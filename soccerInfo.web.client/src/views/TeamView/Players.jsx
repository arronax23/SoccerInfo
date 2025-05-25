import { useEffect, useState } from "react";
import PlayersGroup from "./PlayersGroup";

const Players = ({ teamId }) => {
  const [playersGroups, setPlayersGroups] = useState();
  useEffect(() => {
    getPlayers();
  }, []);


  async function getPlayers() {
    const response = await fetch(`/api/GetGroupedPlayers/${teamId}`);
    const data = await response.json();
    console.log(data);
    setPlayersGroups(data);
  }



  return (
    <div className="players">

      {playersGroups && playersGroups.map(pg => (
        <PlayersGroup
          key={pg.displayName}
          generalPositionDisplay={pg.displayName}
          players = {pg.players}
        />
      ))}
    </div>
  );
};

export default Players;
