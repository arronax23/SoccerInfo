import React, { useEffect, useState } from "react";
import Player from "./Player";

const PlayersView = () => {
  const [players, setPlayers] = useState();
  const [keyword, setKeyword] = useState("");

  useEffect(() => {
    getPlayers();
  }, [keyword]);

  return (
    <div className="container">
      <div className="search-players">
        <input
          value={keyword}
          onChange={(e) => setKeyword(e.target.value)}
          className="search__input"
          type="text"
          placeholder="Search players"
        ></input>
      </div>
      <div className="found-players">
        {players &&
          players.map((p) => (
            <Player
              key={p.id}
              id={p.id}
              name={p.name}
              faceImageBase64={p.faceImageBase64}
              teamId={p.teamId}
              teamImageBase64={p.teamImageBase64}
              leagueImageBase64={p.leagueImageBase64}
              leagueId={p.leagueId}
            />
          ))}
      </div>
    </div>
  );

  async function getPlayers() {
    if (keyword.length < 3) {
      return;
    }

    const response = await fetch(`/api/SearchPlayers/${keyword}`);
    const data = await response.json();
    console.log(data);
    setPlayers(data);
  }
};

export default PlayersView;
