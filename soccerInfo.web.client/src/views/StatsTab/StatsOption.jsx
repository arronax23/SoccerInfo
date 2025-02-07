
const StatsOption = ({ optionCriteria, setIsPanelActive, setCurrentCriteria }) => {
  return (
    <div onClick={openPanel} className="stats-option">
      {optionCriteria.Text}
    </div>
  );

  function openPanel() {
    setIsPanelActive(true);
    setCurrentCriteria(optionCriteria);
  }
};

export default StatsOption;
