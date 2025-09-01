import {
  handleTextDisplay,
  handleDateDisplay,
  handleHeightDisplay,
} from "../../utils/formatter";
import CustomWebsiteIcon from "/socialMediaIcons/custom-website-icon.jpg";
import FacebookIcon from "/socialMediaIcons/facebook-icon.svg";
import XIcon from "/socialMediaIcons/x-icon.svg";
import InstagramIcon from "/socialMediaIcons/instagram-icon.svg";
import YoutubeIcon from "/socialMediaIcons/youtube-icon.svg";

const PlayerAdditionalInfo = ({ playerAdditionalInfo }) => {
  return (
    <div className="player-additional-info">
      <div className="header">Additional information</div>
      <div className="height">
        <div className="label">Height</div>
        <div className="content">
          {handleHeightDisplay(playerAdditionalInfo.height)}
        </div>
      </div>
      <div className="birth-place">
        <div className="left">
          <div className="country">
            <img
              className="country-flag-img"
              title={playerAdditionalInfo.brithPlace.country}
              src={`data:${playerAdditionalInfo.brithPlace.countryImage.mimeType};base64, ${playerAdditionalInfo.brithPlace.countryImage.base64}`}
            />
          </div>
        </div>
        <div className="center">
          <div className="label">Birth place</div>
          <div className="content">
            <div className="city">{playerAdditionalInfo.brithPlace.city}</div>
          </div>
        </div>
        <div className="right"></div>
      </div>

      {playerAdditionalInfo.nationalTeam != null ? (
        <div className="national-team">
          <div className="left">
            <div className="country">
              <img
                className="country-flag-img"
                title={playerAdditionalInfo.nationalTeam.country}
                src={`data:${playerAdditionalInfo.nationalTeam.countryImage.mimeType};base64, ${playerAdditionalInfo.nationalTeam.countryImage.base64}`}
              />
            </div>
          </div>
          <div className="center">
            <div className="label">National team</div>
            <div className="content">
              <div className="caps">
                <strong>Matches:</strong>{" "}
                {playerAdditionalInfo.nationalTeam.caps}
              </div>
              <div className="goals">
                <strong>Goals:</strong>{" "}
                {playerAdditionalInfo.nationalTeam.goals}
              </div>
            </div>
          </div>
          <div className="right"></div>
        </div>
      ) : (
        <div className="national-team">
          <div className="left"></div>
          <div className="center">
            <div className="label">National team</div>
            <div className="content">-</div>
          </div>
          <div className="right"></div>
        </div>
      )}

      <div className="leading-foot">
        <div className="label">Leading foot</div>
        <div className="content">
          {handleTextDisplay(playerAdditionalInfo.leadingFoot)}
        </div>
      </div>
      <div className="club-join-date">
        <div className="label">Joined the club</div>
        <div className="content">
          {handleDateDisplay(playerAdditionalInfo.clubJoinDate)}
        </div>
      </div>
      <div className="contract-expiration-date">
        <div className="label">Contract expires</div>
        <div className="content">
          {handleDateDisplay(playerAdditionalInfo.contractExpirationDate)}
        </div>
      </div>
      <div className="socials">
        <div className="label">Social media</div>
        <div className="content">
          {playerAdditionalInfo.socials.map((x) => (
            <div key={x.platform} className="social-media">
              {x.platform == "" ? (
                <a href={x.link} title="Official Website" target="_blank" rel="noopener noreferrer">
                  <img className="icon" src={CustomWebsiteIcon} />
                </a>
              ) : null}      
              {x.platform == "YouTube" ? (
                <a href={x.link} title={x.platform} target="_blank" rel="noopener noreferrer">
                  <img className="icon" src={YoutubeIcon} />
                </a>
              ) : null}                            
              {x.platform == "Facebook" ? (
                <a href={x.link} title={x.platform} target="_blank" rel="noopener noreferrer">
                  <img className="icon" src={FacebookIcon} />
                </a>
              ) : null}
              {x.platform == "Twitter" ? (
                <a href={x.link} title={x.platform} target="_blank" rel="noopener noreferrer">
                  <img className="icon" src={XIcon} />
                </a>
              ) : null}
              {x.platform == "Instagram" ? (
                <a href={x.link} title={x.platform} target="_blank" rel="noopener noreferrer">
                  <img className="icon" src={InstagramIcon} />
                </a>
              ) : null}           
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default PlayerAdditionalInfo;
