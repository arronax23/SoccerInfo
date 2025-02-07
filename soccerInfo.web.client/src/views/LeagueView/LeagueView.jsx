import Teams from './Teams'
import { useParams } from 'react-router'
import League from './League'

const LeagueView = () => {
    const { leagueId } = useParams();
  return (
    <div>
        <League leagueId={leagueId} />
        <Teams leagueId={leagueId} />
    </div>
  )
}

export default LeagueView