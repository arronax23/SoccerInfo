import React, {useState, useEffect} from 'react'

const Team = ({ id }) => {
  const [team, setTeam] = useState();
  useEffect(() => {
    getTeam();
  }, []);

  return (
    (team && (
        <div className="t">
        <img
          className="t-img"
          src={`data:image/jpeg;base64,${team.teamImageBase64}`}
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