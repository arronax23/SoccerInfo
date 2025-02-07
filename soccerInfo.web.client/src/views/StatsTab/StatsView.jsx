import  { useState } from "react";
import StatsOption from "./StatsOption";
import StatsPanelContainer from "./StatsPanel/StatsPanelContainer";
import Criterias from "./Criteria";


const StatsView = () => {
  const [isPanelActive, setIsPanelActive] = useState();
  const [currentCriteria, setCurrentCriteria] = useState();

  return (
    <div className="stats-view">
      <StatsPanelContainer
        currentCriteria={currentCriteria}
        isPanelActive={isPanelActive}
        closePanel={closePanel}
      />
      {Object.values(Criterias).map((c) => (
        <StatsOption
          key={c.Type}
          optionCriteria={c}
          setCurrentCriteria={setCurrentCriteria}
          setIsPanelActive={setIsPanelActive}
        />
      ))}
    </div>
  );

  function closePanel() {
    setIsPanelActive(false);
  }  
};

export default StatsView;
