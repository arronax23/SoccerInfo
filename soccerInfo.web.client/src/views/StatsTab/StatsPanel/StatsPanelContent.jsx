import { useState, useCallback } from "react";
import { MaterialReactTable } from "material-react-table";
import useTableConfig from "./Helpers/useTableConfig";
import useStatsFetch from "./Helpers/useStatsFetch";
import LoadingIcon from "./Helpers/LoadingIcon";

const StatsPanelContent = ({ criteria }) => {
  const [descending, setIsDescending] = useState(true);

  const [requestFilter, setRequestFilter] = useState({
    criteria: criteria.Type,
    pageNumber: 1,
    pageSize: 20,
    isSortDescending: descending,
    positions: [],
    teams: [],
    leagues: [],
    nationalities: []
  });

  const { stats, columns, loading } = useStatsFetch(requestFilter, criteria.Text);

  const incrementPageNumber = useCallback(() => {
    setRequestFilter(prev => ({...prev, pageNumber: prev.pageNumber + 1 }))
  }, [requestFilter]);

  const table = useTableConfig(columns, stats, loading, incrementPageNumber, requestFilter, setRequestFilter);

  return (
    <div className="stats-panel-content">
      {loading && <LoadingIcon />}
      {columns && stats && <MaterialReactTable table={table} />}
    </div>
  );
};

export default StatsPanelContent;
