import Team from "./Team";
import { useEffect, useState } from "react";

const Teams = ({ leagueId }) => {
  const [teams, setTeams] = useState();

  useEffect(() => {
    getTeams();
  }, []);

  return (
    <div className="teams">
      {teams &&
        teams.map((team) => (
          <Team
            key={team.id}
            id={team.id}
            name={team.name}
            logoBase64={team.logoBase64}
            logoMimeType={team.logoMimeType}
          />
        ))}
    </div>
  );

  async function getTeams() {
    const response = await fetch(`/api/GetTeams/${leagueId}`);
    const data = await response.json();
    console.log(data);
    setTeams(data);
  }
};

export default Teams;
