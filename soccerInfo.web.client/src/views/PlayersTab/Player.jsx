import { useRef } from "react";
import { useNavigate } from "react-router";

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
  const playerOverview = useRef();
  const teamImage = useRef();
  const leagueImage = useRef();

  return (
    <div
      ref={playerOverview}
      onMouseMove={playerHover}
      onMouseLeave={playerLeave}
      onClick={() => navigate(`/player/${id}`)}
      className="player-overview"
    >
      <img
        className="face-image"
        src={`data:image/jpeg;base64,${faceImageBase64}`}
      />
      <p className="name">{name}</p>
      <img
        ref={teamImage}
        onMouseOver={imageHover}
        onClick={(e) => {
          e.stopPropagation();
          navigate(`/team/${teamId}`);
        }}
        className="team-image"
        src={`data:image/jpeg;base64,${teamImageBase64}`}
      />
      <img
        ref={leagueImage}
        onMouseOver={imageHover}
        onClick={(e) => {
          e.stopPropagation();
          navigate(`/league/${leagueId}`);
        }}
        className="league-image"
        src={`data:image/jpeg;base64,${leagueImageBase64}`}
      />
    </div>
  );

  function playerHover() {
    if (
      !teamImage.current.matches(":hover") &&
      !leagueImage.current.matches(":hover")
    ) {
      playerOverview.current.classList.add("active");
    }
  }

  function playerLeave() {
    playerOverview.current.classList.remove("active");
  }
  function imageHover(e) {
    playerOverview.current.classList.remove("active");
    e.stopPropagation();
  }
};

export default Player;
