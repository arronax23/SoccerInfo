import { useEffect, useState } from "react";
import {
  MaterialReactTable,
  useMaterialReactTable,
} from "material-react-table";
import { getTableColumns } from "../ColumnsByCriteria";

const StatsPanelContent = ({ criteria }) => {
  const [stats, setStats] = useState([]);
  const [columns, setColumns] = useState([]);
  const [isLoaded, setIsLoaded] = useState(false);

  useEffect(() => {
    console.log(criteria);
    getStats();
  }, []);

  // const columns =  [
  //   { accessorKey: "id", header: "ID" },
  //   { accessorKey: "name", header: "Player Name" },
  // ];

  // const table = useMaterialReactTable({
  //   columns: columns,
  // });

  const table = useMaterialReactTable({
    columns: columns,
    data: stats,
    state: { isLoading: !isLoaded },
    enablePagination: false,
    muiTableBodyCellProps: {
      align: "center",
      sx: {   
        paddingTop: .5,
        paddingBottom: .5,
      },
    }, 
    muiTableHeadCellProps: {
      align: "center",
    },    
    muiTableContainerProps: {
      sx: { overflowY: "auto",overflowX: "hidden", maxHeight: "450px"}, // 👈 Set scroll here!
    },
    enableBottomToolbar: false
  });

  return (
    <div className="stats-panel-content">
      {isLoaded && columns && stats && <MaterialReactTable table={table} />}
    </div>
  );

  async function getStats() {
    const response = await fetch("/api/GetPlayersByStats", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        Criteria: criteria.Type,
        Take: 25,
        Skip: 0,
        IsSortDescending: true,
      }),
    });
    const data = await response.json();
    console.log(data);

    setColumns(
      Object.keys(data[0]).map((key) => ({
        header: key,
        accessorKey: key,
      }))
    );

    const tableInfo = getTableColumns(criteria, data);

    console.log("columns: ", tableInfo.columns);
    console.log("data: ", tableInfo.data);

    setStats(tableInfo.data);
    setColumns(tableInfo.columns);
    setIsLoaded(true);
  }
};

export default StatsPanelContent;
