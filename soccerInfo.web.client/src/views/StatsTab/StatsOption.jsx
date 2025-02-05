import React from "react";

const StatsOption = ({ headerText }) => {
  return (
    <div onClick={openPanel} className="stats-option">{headerText}</div>
  );

  function openPanel(){
    document.querySelector('.stats-panel-container').classList.toggle('active')
  }

};

export default StatsOption;
