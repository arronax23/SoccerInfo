import React from "react";
import { handleMarketValueDisplay } from "../../utils/utils";

const PlayerGeneralInfo = ({ playerGeneralInfo }) => {
  return (
    <div className="player-general-info">
      <div className="header">General information</div>
      <div className="portrait">
        <img
          src={`data:image/jpeg;base64,${playerGeneralInfo.faceImageBase64}`}
        />
      </div>
      <div className="nationalities">
        {playerGeneralInfo.nationalities.map((x) => (
          <img
            key={x.country}
            className="nationality-img"
            title={x.country}
            src={`data:image/svg+xml; base64, ${x.countryFlagBase64Image}`}
          />
        ))}
      </div>
      <div className="name">
        <div className="label">Name</div>
        <div className="content">{playerGeneralInfo.name}</div>
      </div>
      <div className="age">
        <div className="label">Age</div>
        <div className="content">{playerGeneralInfo.age}</div>
      </div>
      <div className="birth-date">
        <div className="label">Birth Date</div>
        <div className="content">
          {new Date(playerGeneralInfo.dateOfBirth).toLocaleDateString()}
        </div>
      </div>
      <div className="position">
        <div className="label">Position</div>
        <div className="content">{playerGeneralInfo.position}</div>
      </div>
      <div className="market-value">
        <div className="label">Market Value</div>
        <div className="content">
          {handleMarketValueDisplay(
            playerGeneralInfo.marketValue,
            playerGeneralInfo.marketValueUnit
          )}
        </div>
      </div>
    </div>
  );
};

export default PlayerGeneralInfo;
