import { useEffect, useState } from "react";
import "./App.css";

function App() {
  const [teams, setTeams] = useState();

  useEffect(() => {
    getTeams();
  }, []);
  return (
    teams &&
    teams.map((team) => (
      <div>
        <h3 className="team-header" onClick={showSquads}>{team.name}</h3>
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
            {team.players.map((player) => (
              <tr key={player.id}>
                <td>{player.name}</td>
                <td      className="face-image-p">
                  {" "}
                  <img
                    className="face-image"
                    src={`data:image/jpeg;base64,${player.faceImageBase64}`}
                  />
                </td>
                <td>{player.position}</td>
                {player.nationalityImageBase64Collection.map(
                  (nationalities) => (
                    <td    className="nationality-image-p">
                      <img
                        className="nationality-image"
                        src={`data:image/jpeg;base64,${nationalities.base64Image}`}
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
    console.log(data);
    setTeams(data);
  }

  async function showSquads(e){
    console.log(e.target.nextSibling.classList.toggle('show'))
  }
}

export default App;
