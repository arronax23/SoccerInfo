import React from "react";
import { useNavigate } from "react-router";

const League = ({ id, name, leagueImageBase64, countryFlagBase64 }) => {
  const navigate = useNavigate();

  const leagueClick = (e) => {
    navigate(`/league/${id}`)
  };

  return (
    <div className="league" key={id} onClick={leagueClick}>
      <p className="header">{name}</p>
      <img
        className="league-logo"
        src={`data:image/jpeg;base64,${leagueImageBase64}`}
        alt="league img"
      />
      <img
        className="country-flag"
        src={`data:image/jpeg;base64,${countryFlagBase64}`}
        alt="country img"
      />
    </div>
  );
};

export default League;
