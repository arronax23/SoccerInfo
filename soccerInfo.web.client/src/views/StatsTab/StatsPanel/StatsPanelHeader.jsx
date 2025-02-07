const StatsPanelHeader = ({ closePanel, headerText }) => {
  return (
    <header className="stats-panel-header">
      <div className="empty"></div>
      <h1>{headerText}</h1>
      <button className="close-btn">
        <img onClick={closePanel} src="/close.svg" alt="" />
      </button>
    </header>
  );
};

export default StatsPanelHeader;
