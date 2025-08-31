import { useNavigate } from 'react-router'

const Team = ({id, name, logoBase64, logoMimeType }) => {
    const navigate = useNavigate();
  return (
    <div className="team" onClick={teamClick}>
        <h1 className="team-header">{name}</h1>
            <img
              className="team-image"
              src={`data:${logoMimeType};base64,${logoBase64}`}
            />
    </div>
  )

  function teamClick(){
    navigate(`/team/${id}`)
  }

}

export default Team