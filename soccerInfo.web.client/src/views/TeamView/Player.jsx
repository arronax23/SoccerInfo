import React from "react";
import { useNavigate } from "react-router";

const Player = ({
  id,
  name,
  position,
  faceImageBase64,
  age,
  dateOfBirth,
  marketValue,
  marketValueUnit,
  nationalityImages,
}) => {
  const navigate = useNavigate();

  return (
    <div id={id} onClick={() => navigate(`/player/${id}`)} className="player">
      <h1 className="player-header">{name}</h1>
      <img
        className="player-image"
        src={`data:image/jpeg;base64,${faceImageBase64}`}
      />
      <p className="position">{position}</p>
      <p className="age">Age: {age}</p>
      <p className="dateOfBirth">
        {new Date(dateOfBirth).toLocaleDateString("pl-PL")}
      </p>
      <p className="marketValue">
        {marketValue}{marketValueUnit} €
      </p>
      <div className="nationalities">
        {nationalityImages &&
          nationalityImages.map((image) => (
            <img
              className="nationality-image"
              src={`data:image/svg+xml; base64, ${image}`}
            />
          ))}
      </div>
    </div>
  );
};

export default Player;
