import React from 'react'
import { useParams } from 'react-router'
import Players from './Players'
import Team from './Team';

const TeamView = () => {
  const { teamId } = useParams();
  return (
    <div>
      <Team id={teamId} />
      <Players teamId={teamId} />
    </div>
  )
}

export default TeamView