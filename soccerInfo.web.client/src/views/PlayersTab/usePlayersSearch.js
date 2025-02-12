import { useEffect, useState } from "react";
import axios from "axios";

export default function usePlayersSearch(keyword, pageNumber, pageSize) {
  const [loading, setLoading] = useState(true);
  const [players, setPlayers] = useState([]);
  const [hasMore, setHasMore] = useState(false);

  useEffect(() => {
    setPlayers([]);
  }, [keyword]);

  useEffect(() => {
    setLoading(true);
    let cancel;
    axios({
      method: "GET",
      url: `api/SearchPlayers/${keyword}/${pageNumber}/${pageSize}`,
      cancelToken: new axios.CancelToken((c) => (cancel = c)),
    })
    .then((res) => {
      setPlayers((prevPLayers) => {
        return [...prevPLayers, ...res.data];
      });
      setHasMore(res.data.length > 0);
      setLoading(false);
    })
    .catch((e) => {
      if (axios.isCancel(e)) return;
      console.log(e);
    });

    return () => cancel();
  }, [keyword, pageNumber, pageSize]);

  return { players, loading, hasMore };
}
