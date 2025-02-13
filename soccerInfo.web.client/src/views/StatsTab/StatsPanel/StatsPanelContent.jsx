import { useState, useCallback } from "react";
import { MaterialReactTable } from "material-react-table";
import useTableConfig from "./Helpers/useTableConfig";
import useStatsContentFetch from "./Helpers/useStatsContentFetch";

const StatsPanelContent = ({ criteria }) => {
  const [descending, setIsDescending] = useState(true);

  const [requestFilter, setRequestFilter] = useState({
    criteria: criteria.Type,
    pageNumber: 1,
    pageSize: 20,
    isSortDescending: descending,
    positions: ["Centre-Back"]
  });

  const { stats, columns, loading } = useStatsContentFetch(requestFilter, criteria.Text);

  const incrementPageNumber = useCallback(() => {
    setRequestFilter(prev => ({...prev, pageNumber: prev.pageNumber + 1 }))
  }, [requestFilter]);

  const table = useTableConfig(columns, stats, loading, incrementPageNumber, setRequestFilter);

  return (
    <div className="stats-panel-content">
      {columns && stats && <MaterialReactTable table={table} />}
    </div>
  );
};

export default StatsPanelContent;
