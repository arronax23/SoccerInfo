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
            key={league.id}
            id={league.id}
            name={league.name}
            logoBase64={league.logoBase64}
            logoMimeType={league.logoMimeType}
            countryFlagBase64={league.countryFlagBase64}
            countryFlagMimeType={league.countryFlagMimeType}
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
