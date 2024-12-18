import React from 'react'
import Team from './Team';
import { useEffect, useState } from "react";

const Teams = ({ leagueId }) => {
  const [teams, setTeams] = useState();

  useEffect(() => {
    getTeams();
  }, []);

  return (
    <div className="teams">
    {teams &&
      teams.map((team) => (
        <Team id={team.id} name={team.name} teamImageBase64={team.teamImageBase64} />
      ))}
      </div>
  )

  async function getTeams() {
    const response = await fetch(`/api/GetTeams/${leagueId}`);
    const data = await response.json();
    console.log('dsdsdsd')
    console.log(data)
    setTeams(data);
  }
}

export default Teams