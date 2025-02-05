import React from "react";
import StatsOption from "./StatsOption"
import StatsPanel from "./StatsPanel"

const StatsView = () => {
  return (
    <div className="stats-view">
        <StatsPanel />
        <StatsOption headerText="Most valuable" />
        <StatsOption headerText="Most goals" />
        <StatsOption headerText="Most assists" />
        <StatsOption headerText="Most goals + assists" />
    </div>
  );

};

export default StatsView;
