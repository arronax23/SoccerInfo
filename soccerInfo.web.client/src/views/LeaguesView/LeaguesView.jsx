import { useState, useEffect } from "react";
import League from "./League";

function LeaguesView() {
  const [leagues, setLeagues] = useState();
  useEffect(() => {
    GetLeagues();
  }, []);

  return (
    <div className="leagues">
      {leagues &&
        leagues.map((league) => (
          <League
            id={league.id}
            name={league.name}
            leagueImageBase64={league.leagueImageBase64}
            countryFlagBase64={league.countryFlagBase64}
          />
        ))}
    </div>
  );
  async function GetLeagues() {
    const response = await fetch("/api/GetLeagues");
    const data = await response.json();
    setLeagues(data);
    console.log(data);
  }
}

export default LeaguesView;
