import { useNavigate } from 'react-router'

const Team = ({id, name, teamImageBase64 }) => {
    const navigate = useNavigate();
  return (
    <div className="team" onClick={teamClick}>
        <h1 className="team-header">{name}</h1>
            <img
              className="team-image"
              src={`data:image/jpeg;base64,${teamImageBase64}`}
            />
    </div>
  )

  function teamClick(){
    navigate(`/team/${id}`)
  }

}

export default Team