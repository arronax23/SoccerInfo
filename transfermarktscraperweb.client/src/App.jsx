import { useEffect, useState } from "react";
import "./App.css";

function App() {
  const [teams, setTeams] = useState();
  const [players, setPlayers] = useState();

  useEffect(() => {
    getTeams();
  }, []);
  return (
    teams &&
    teams.map((team) => (
      <div>
        <h3 id={team.id} className="team-header" onClick={showSquads}>
          {team.name}
        </h3>
        <table
          key={team.id}
          className="table table-striped"
          aria-labelledby="tabelLabel"
        >
          <thead>
            <tr>
              <th>Naza</th>
              <th>Zdjęcie</th>
              <th>Pozycja</th>
              <th>Narodowość</th>
            </tr>
          </thead>
          <tbody>
            {players && players.map((player) => (
              <tr key={player.id}>
                <td>{player.name}</td>
                <td className="face-image-p">
                  <img
                    className="face-image"
                    src={`data:image/jpeg;base64,${player.faceImageBase64}`}
                  />
                </td>
                <td>{player.position}</td>
                {player.nationalityImageBase64Collection.map(
                  (nationality) => (
                    <td className="nationality-image-p">
                      <img
                        className="nationality-image"
                        src={`data:image/jpeg;base64,${nationality}`}
                      />
                    </td>
                  )
                )}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    ))
  );

  async function getTeams() {
    const response = await fetch("api/GetTeams");
    const data = await response.json();
    setTeams(data);
  }

  async function GetPlayers(teamId) {
    const response = await fetch(`api/GetPlayers/${teamId}`);
    const data = await response.json();
    console.log(data);
    setPlayers(data);
  } 

  async function showSquads(e) {
    await GetPlayers(e.target.id);
    console.log(e.target.nextSibling.classList.toggle("show"));
  }
}

export default App;
