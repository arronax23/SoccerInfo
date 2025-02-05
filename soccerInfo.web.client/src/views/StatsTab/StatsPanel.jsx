import React, { useRef } from "react";

const StatsPanel = ({ headerText }) => {
  const panelContainer = useRef();

  return (
    <div ref={panelContainer} className="stats-panel-container">
      <button className="close-btn">
        <img onClick={close} src="/close.svg" alt="" />
      </button>

      <div className="stats-panel">{headerText}</div>
    </div>
  );

  function close() {
    panelContainer.current.classList.toggle("active");
  }
};

export default StatsPanel;
