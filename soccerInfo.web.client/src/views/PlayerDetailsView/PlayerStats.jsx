import { handleTextDisplay } from "../../utils/formatter";

const PlayerStats = ({ isGoalkeeper, stats }) => {
  return (
    <div className="stats-container">
      <div className="header">Stats</div>
      <div className="stats-labels">
        <div className="stats-label">League</div>
        {isGoalkeeper ? (<div className="stats-label">Goals conceded</div>) : (<div className="stats-label">Goals</div>) } 
        {isGoalkeeper ? (<div className="stats-label">Clean sheets</div>) : (<div className="stats-label">Assists</div>) } 
        <div className="stats-label">Matches played</div>
        <div className="stats-label">Minutes played</div>
      </div>

      {stats.map((x) => (
        <div key={x.league.name} className="stats-content">
          <div className="stats-item league-flag">
            <img
              className="league-icon"
              title={x.league.name}
              src={`data:image; base64, ${x.league.base64Image}`}
            />
          </div>
          {isGoalkeeper ? (<div className="stats-item goals-conceded">{handleTextDisplay(x.goalsConceded)}</div>) : (<div className="stats-item goals">{handleTextDisplay(x.goals)}</div>) } 
          {isGoalkeeper ? (<div className="stats-item clean-sheets">{handleTextDisplay(x.cleanSheets)}</div>) : (<div className="stats-item assists">{handleTextDisplay(x.assists)}</div>) } 
          <div className="stats-item matches-played">{handleTextDisplay(x.matchesPlayed)}</div>
          <div className="stats-item minutes-played">{handleTextDisplay(x.minutesPlayed)}</div>
        </div>
      ))}
    </div>
  );
};

export default PlayerStats;
