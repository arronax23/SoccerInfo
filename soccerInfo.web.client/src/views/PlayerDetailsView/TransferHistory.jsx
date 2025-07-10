import { handleDateDisplay, handleMarketValueDisplay } from "../../utils/formatter";

const TransferHistory = ({ transferHistory }) => {
  return (
    <div className="transfer-history-container">
      <div className="header">Transfer history</div>
      <div className="table-header">
        <div className="column-header">Season</div>
        <div className="column-header">Age</div>
        <div className="column-header">Date</div>
        <div className="column-header">From</div>
        <div className="column-header"></div>
        <div className="column-header"></div>
        <div className="column-header">To</div>
        <div className="column-header"></div>
        <div className="column-header">Market value</div>
        <div className="column-header">Fee</div>
        <div className="column-header">Type</div>
      </div>
      <div className="table-content">
        {transferHistory.transfers.map((t) => (
          <div key={t.age} className="row">
            <div className="column">{t.season}</div>
            <div className="column">{t.age}</div>
            <div className="column">{handleDateDisplay(t.date)}</div>
            <div className="column">{t.clubFromName}</div>
            <div className="column">
              <img className="club-image" src={`data:image/png;base64,${t.cLubFromBase64Image}`} />
            </div>
            <div className="column"> ➜ </div>
            <div className="column">{t.clubToName}</div>
            <div className="column">
              <img className="club-image" src={`data:image/png;base64,${t.cLubToBase64Image}`} />
            </div>
            <div className="column">{handleMarketValueDisplay(t.marketValue.value, t.marketValue.suffix)}</div>
            <div className="column">{handleMarketValueDisplay(t.fee.value, t.fee.suffix)}</div>
            <div className="column">{t.transferType}</div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default TransferHistory;
