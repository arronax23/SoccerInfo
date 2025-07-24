import { useEffect, useState } from "react";
import axios from "axios";

export default function useSearch(isTeamsSearch, keyword, pageNumber, pageSize) {
  const [loading, setLoading] = useState(false);
  const [data, setData] = useState([]);
  const [hasMore, setHasMore] = useState(false);
  const [previsTeamsSearch, setPrevisTeamsSearch] = useState(false);

  useEffect(() => {
    setData([]);
  }, [keyword]);

  useEffect(() => {
    if (previsTeamsSearch != isTeamsSearch){
      setData([]);
      setPrevisTeamsSearch(isTeamsSearch);
    }
  }, [previsTeamsSearch, isTeamsSearch]);

  useEffect(() => {
    if(keyword < 2) return;
    
    setLoading(true);
    let cancel;

    let endpoint;
    if (isTeamsSearch){
      endpoint = "SearchTeams"
    }
    else {
      endpoint = "SearchPlayers"
    }

    axios({
      method: "GET",
      url: `api/${endpoint}/${keyword}/${pageNumber}/${pageSize}`,
      cancelToken: new axios.CancelToken((c) => (cancel = c)),
    })
    .then((res) => {
      setData((prevPlayers) => {
        return [...prevPlayers, ...res.data];
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

  return { data, loading, hasMore };
}
