import React from 'react'

const PlayerGeneralInfo = ({ playerGeneralInfo }) => {
    console.log("playerGeneralInfo",playerGeneralInfo)
  return (
    <div>
        <div>{playerGeneralInfo.Name}</div>
        <div>{playerGeneralInfo.Age}</div>
        <div>{playerGeneralInfo.Position}</div>
    </div>
  )
}

export default PlayerGeneralInfo