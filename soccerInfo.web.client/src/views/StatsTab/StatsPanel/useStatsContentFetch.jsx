import { useEffect, useState } from "react";
import axios from "axios";
import { getTableData } from "./GetTableData";

export default function useStatsContentFetch(criteria, pageNumber, pageSize, descending) {
  const [loading, setLoading] = useState(true);
  const [columns, setColumns] = useState([]);
  const [stats, setStats] = useState([]);
  const [hasMore, setHasMore] = useState(false);

  useEffect(() => {
    setColumns([]);
    setStats([]);
  }, [criteria]);

  useEffect(() => {
    setLoading(true);

    axios({
      method: "POST",
      url: "api/GetPlayersByStats",
      data: {
        Criteria: criteria.Type,
        PageNumber: pageNumber,
        PageSize: pageSize,
        IsSortDescending: descending,
      }
    })
    .then((res) => {
      const newData = getTableData(criteria, res.data);
      setStats((prevStats) => {
        return [...prevStats, ...newData.data];
    });
      setColumns(newData.columns)
      setHasMore(res.data.length > 0);
      setLoading(false);
    })
    .catch((e) => {
      if (axios.isCancel(e)) return;
      console.log(e);
    });

  }, [criteria, pageNumber, pageSize]);

  return { stats, columns, loading, hasMore };
}
