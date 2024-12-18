import React, {useState, useEffect} from "react";

const League = ({ leagueId }) => {
  const [league, setLeague] = useState();
  useEffect(() => {
    getLeague();
  }, []);

  return (
    (league && (
        <div className="l">
        <img
          className="l-img"
          src={`data:image/jpeg;base64,${league.leagueImageBase64}`}
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
