
import { useState, useEffect } from "react";

const League = ({ leagueId }) => {
  const [league, setLeague] = useState();
  useEffect(() => {
    getLeague();
  }, []);

  return (
    (league && (
        <div className="l">
        <h2 className="header">{league.name}</h2>
        <img
          className="l-img"
          src={`data:${league.logoMimeType};base64,${league.logoBase64}`}
        />
      </div>
    ))
  );

  async function getLeague() {
    const response = await fetch(`/api/GetLeague/${leagueId}`);
    const data = await response.json();
    console.log(data)
    setLeague(data);
  }
};

export default League;
