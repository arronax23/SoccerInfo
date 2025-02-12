import { useState, useRef, useCallback } from "react";
import Player from "./Player";
import usePlayersSearch from "./../../utils/usePlayersSearch";

const PlayersView = () => {
  const [keyword, setKeyword] = useState("");
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  const { players, loading, hasMore } = usePlayersSearch(
    keyword,
    pageNumber,
    pageSize
  );

  const observer = useRef();
  const lastPlayerElementRef = useCallback(
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

  return (
    <div className="container">
      <div className="search-players">
        <input
          value={keyword}
          onChange={handleSearch}
          className="search__input"
          type="text"
          placeholder="Search players"
        ></input>
      </div>
      <div className="found-players">
        {players &&
          players.map((p) => (
            <Player
              key={p.id}
              id={p.id}
              name={p.name}
              faceImageBase64={p.faceImageBase64}
              teamId={p.teamId}
              teamImageBase64={p.teamImageBase64}
              leagueImageBase64={p.leagueImageBase64}
              leagueId={p.leagueId}
            />
          ))}
        <div>{loading && "Loading..."}</div>
        <div ref={lastPlayerElementRef}></div>
      </div>
    </div>
  );
};

export default PlayersView;
