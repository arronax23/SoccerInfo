import { useEffect, useState } from "react";
import Counter from "./Counter";

const HomeView = () => {
  const [soccerDataAmount, setSoccerDataAmount] = useState();
  useEffect(() => {
    GetSoccerDataAmount();
  }, []);

  return (
    <div className="home-container">
      <div className="introduction-container">
        <div className="header">
          <img className="logo" src={"/soccer-favicon.svg"} />
          <h1 className="app-name">Soccer Info</h1>
        </div>
        <div className="introduction-text">
          Welcome to SoccerInfo – your ultimate destination for everything
          soccer. Dive into the world of soccer with our comprehensive database.
          At SoccerInfo, we believe that soccer is more than a game—it’s a
          global phenomenon that connects communities and cultures. Our platform
          brings together detailed player profiles, historical data, club
          statistics, and transfer market insights to give you a complete
          picture of this beautiful game.
        </div>
      </div>

      {soccerDataAmount && (
        <div className="data-amount-container">
          <div className="count-boxes">
            <div className="count-box">
              <div className="amount">
                <Counter end={soccerDataAmount.leaguesCount} />
              </div>
              <div className="data-type">Leagues</div>
            </div>
            <div className="count-box">
              <div className="amount">
                <Counter end={soccerDataAmount.teamsCount} />
              </div>
              <div className="data-type">Teams</div>
            </div>
            <div className="count-box">
              <div className="amount">
                <Counter end={soccerDataAmount.playersCount} />
              </div>
              <div className="data-type">Players</div>
            </div>
          </div>
        </div>
      )}
    </div>
  );

  async function GetSoccerDataAmount() {
    const response = await fetch("/api/GetSoccerDataAmount");
    const data = await response.json();
    setSoccerDataAmount(data);
    console.log(data);
  }
};

export default HomeView;
