import StatsPanelContent from "./StatsPanelContent";
import StatsPanelHeader from "./StatsPanelHeader" 

const StatsPanelContainer = ({
  currentCriteria,
  isPanelActive,
  closePanel,
}) => {
  return (
    isPanelActive && (
      <div className="stats-panel-container">
        <div className="stats-panel">
          <StatsPanelHeader closePanel={closePanel} headerText={currentCriteria.Text} />
          <StatsPanelContent criteria={currentCriteria} />
        </div>
      </div>
    )
  );
};

export default StatsPanelContainer;