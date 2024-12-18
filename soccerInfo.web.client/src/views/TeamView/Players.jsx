import React, { useEffect, useState } from "react";
import Player from "./Player";

const Players = ({teamId}) => {
  const [players, setPlayers] = useState();
  useEffect(() => {
    getPlayers();
  }, []);

  async function getPlayers() {
    const response = await fetch(`/api/GetPlayers/${teamId}`);
    const data = await response.json();
    console.log(data);
    setPlayers(data);
  }
  return (
    <div className="players">
      {players &&
        players.map((p) => (
          <Player
            id={p.id}
            name={p.name}
            position={p.position}
            faceImageBase64 ={p.faceImageBase64}
            age={p.age}
            dateOfBirth={p.dateOfBirth}
            marketValue={p.marketValue}
            marketValueUnit={p.marketValueUnit}
            nationalityImages={p.nationalityImageBase64Collection}
          />
        ))}
    </div>
  );
};

export default Players;
