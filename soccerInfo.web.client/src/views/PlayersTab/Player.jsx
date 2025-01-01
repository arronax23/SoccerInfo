import React from "react";
import { Link, useNavigate } from "react-router";

const Player = ({
  id,
  name,
  faceImageBase64,
  teamId,
  teamImageBase64,
  leagueId,
  leagueImageBase64,
}) => {
  const navigate = useNavigate();

  return (
    <div className="player-overview">
      <img
        className="face-image"
        src={`data:image/jpeg;base64,${faceImageBase64}`}
      />
      <p className="name">{name}</p>
      <img
        onClick={() => navigate(`/team/${teamId}`)}
        className="team-image"
        src={`data:image/jpeg;base64,${teamImageBase64}`}
      />
      <img
        onClick={() => navigate(`/league/${leagueId}`)}
        className="league-image"
        src={`data:image/jpeg;base64,${leagueImageBase64}`}
      />
    </div>
  );
};

export default Player;
