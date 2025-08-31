import { useState, useRef, useCallback } from "react";
import Player from "./Player";
import Team from "./Team";
import useSearch from "./useSearch";
import LoadingIcon from "./LoadingIcon";

const SearchView = () => {
  const [keyword, setKeyword] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [isTeamsSearch, setIsTeamsSearch] = useState(false);
  const [placeholder, setPlaceholder] = useState("Players");

  const searchTypePlayersDiv = useRef();
  const searchTypeTeamsDiv = useRef();

  const { data, loading, hasMore } = useSearch(
    isTeamsSearch,
    keyword,
    pageNumber,
    pageSize
  );

  const observer = useRef();
  const bottomDivRef = useCallback(
    (node) => {
      if (loading) return;
      if (observer.current) observer.current.disconnect();
      observer.current = new IntersectionObserver((entries) => {
        if (entries[0].isIntersecting && hasMore) {
          console.log("visible");
          setPageNumber((prevPageNumber) => prevPageNumber + 1);
        }
      });
      if (node) observer.current.observe(node);
    },
    [loading]
  );

  const handleSearch = (e) => {
    setKeyword(e.target.value);
    setPageNumber(1);
  };

  const handleSearchTypeClick = (e) => {
    if (e.target.classList.contains('search-players-type')){
      setIsTeamsSearch(false);
      setPlaceholder("Players");
    }
    else if (e.target.classList.contains('search-teams-type')) {
      setIsTeamsSearch(true);
      setPlaceholder("Teams");
    }

    searchTypePlayersDiv.current.classList.toggle('active');
    searchTypeTeamsDiv.current.classList.toggle('active');
  }

  return (
    <div className="container">
      <div className="search-types-container">
        <div className="search-players-type active" ref={searchTypePlayersDiv} onClick={handleSearchTypeClick}>Players</div>
        <div className="search-teams-type" ref={searchTypeTeamsDiv} onClick={handleSearchTypeClick}>Teams</div>
      </div>
      <div className="search-players">
        <input
          value={keyword}
          onChange={handleSearch}
          className="search__input"
          type="text"
          placeholder={`Search ${placeholder}`}
        ></input>
      </div>
      <div className="search-results">
        {data && !isTeamsSearch &&
          data.map((p) => (
            <Player
              key={p.id}
              id={p.id}
              name={p.name}
              faceImage={p.faceImage}
              faceImageMimeType={p.faceImageMimeType}
              teamId={p.teamId}
              teamLogo={p.teamLogo}
              teamLogoMimeType={p.teamLogoMimeType}
              leagueLogo={p.leagueLogo}
              leagueLogoMimeType={p.leagueLogoMimeType}
              leagueId={p.leagueId}
            />
          ))}
        {data && isTeamsSearch &&
          data.map((t) => (
            <Team
              key={t.id}
              id={t.id}
              name={t.name}
              teamLogo={t.teamLogo}
              teamLogoMimeType={t.teamLogoMimeType}
              leagueLogo={t.leagueLogo}
              leagueLogoMimeType={t.leagueLogoMimeType}
              leagueId={t.leagueId}
            />
          ))}          
        {loading && <LoadingIcon />}
        <div className="search-players-bottom" ref={bottomDivRef}></div>
      </div>
    </div>
  );
};

export default SearchView;
