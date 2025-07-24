import { useRef } from "react";
import { useNavigate } from "react-router";

const Team = ({
  id,
  name,
  teamImageBase64,
  teamId,
  leagueId,
  leagueImageBase64,
}) => {
  const navigate = useNavigate();
  const teamOverview = useRef();
  const teamImage = useRef();
  const leagueImage = useRef();

  return (
    <div
      ref={teamOverview}
      onMouseMove={playerHover}
      onMouseLeave={playerLeave}
      onClick={() => navigate(`/team/${id}`)}
      className="team-overview"
    >
      <img
        ref={teamImage}
        className="team-image"
        src={`data:image/jpeg;base64,${teamImageBase64}`}
      />        
      <p className="name">{name}</p>
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
    if (!leagueImage.current.matches(":hover")
    ) {
      teamOverview.current.classList.add("active");
    }
  }

  function playerLeave() {
    teamOverview.current.classList.remove("active");
  }
  function imageHover(e) {
    teamOverview.current.classList.remove("active");
    e.stopPropagation();
  }
};

export default Team;
