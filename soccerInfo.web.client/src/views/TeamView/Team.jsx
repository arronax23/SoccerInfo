import {useState, useEffect} from 'react'

const Team = ({ id }) => {
  const [team, setTeam] = useState();
  useEffect(() => {
    getTeam();
  }, []);

  return (
    (team && (
        <div className="t">
        <h2 className="header">{team.name}</h2>
        <img
          className="t-img"
          src={`data:${team.logoMimeType};base64,${team.logoBase64}`}
        />
      </div>
    ))
  );

  async function getTeam() {
    const response = await fetch(`/api/GetTeam/${id}`);
    const data = await response.json();
    console.log(data)
    setTeam(data);
  }
}

export default Team