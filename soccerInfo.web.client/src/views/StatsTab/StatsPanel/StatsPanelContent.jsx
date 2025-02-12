import { useEffect, useState, useCallback } from "react";
import axios from "axios";
import { MaterialReactTable } from "material-react-table";
import { getTableData } from "./GetTableData";
import useTableConfig from "./useTableConfig";
import useStatsContentFetch from "./useStatsContentFetch";

const StatsPanelContent = ({ criteria }) => {
  const [descending, setIsDescending] = useState(true);

  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(20);

  // useEffect(() => {
  //   console.log(criteria);
  //   getStats();
  // }, []);

  const { stats, columns, loading, hasMore }= useStatsContentFetch(
    criteria, 
    pageNumber, 
    pageSize,
    descending
  )

  const incrementPageNumber =  useCallback(() => {
    setPageNumber(prev => prev + 1)
  }, [pageNumber]);

  const table = useTableConfig(columns, stats, loading, incrementPageNumber);

  return (
    <div className="stats-panel-content">
      {columns && stats && <MaterialReactTable table={table} />}
    </div>
  );



  // async function getStats() {
  //   const response = await axios.post("/api/GetPlayersByStats", {
  //     Criteria: criteria.Type,
  //     PageNumber: 1,
  //     PageSize: 10,
  //     IsSortDescending: true,
  //   });

  //   const data = response.data;

  //   console.log(data);

  //   const tableInfo = getTableData(criteria, data);

  //   console.log("columns: ", tableInfo.columns);
  //   console.log("data: ", tableInfo.data);

  //   setStats(tableInfo.data);
  //   setColumns(tableInfo.columns);
  //   setIsLoaded(true);
  // }
};

export default StatsPanelContent;
