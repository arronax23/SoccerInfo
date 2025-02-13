import { useEffect, useState } from "react";
import axios from "axios";
import  getTableData  from "./GetTableData";
import  { useNavigate }  from "react-router";

export default function useStatsContentFetch(requestFilter, criteriaText) {
  const [loading, setLoading] = useState(true);
  const [columns, setColumns] = useState([]);
  const [stats, setStats] = useState([]);

  const navigate = useNavigate();


  useEffect(() => {
    console.log("loading",loading)
  }, [loading]);

  useEffect(() => {
    console.log("1",requestFilter)
    setColumns([]);
    setStats([]);
  }, [requestFilter.criteria, requestFilter.positions]);

  useEffect(() => {
    console.log("2",requestFilter)
    setLoading(true);

    axios({
      method: "POST",
      url: "api/GetPlayersByStats",
      data: requestFilter
    })
    .then((res) => {
      const newData = getTableData(criteriaText, res.data, navigate);
      setStats((prevStats) => {
        return [...prevStats, ...newData.data];
    });
      setColumns(newData.columns)
      setLoading(false);
    })
    .catch((e) => {
      if (axios.isCancel(e)) return;
      console.log(e);
    });

  }, [requestFilter.pageNumber, requestFilter.pageSize, requestFilter.positions]);

  return { stats, columns, loading };
}
