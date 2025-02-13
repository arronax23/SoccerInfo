import { useCallback, useEffect, useRef } from "react";
import { useMaterialReactTable } from "material-react-table";

export default function useTableConfig(
  columns,
  stats,
  loading,
  incrementPageNumber
) {
  const enableFetchRef = useRef(false);

  const fetchMoreOnBottomReached = useCallback(
    (containerRefElement) => {
      if (containerRefElement && !loading && enableFetchRef.current) {
        const { scrollHeight, scrollTop, clientHeight } = containerRefElement;
        if (scrollHeight - scrollTop - clientHeight < 200 && !loading) {
          enableFetchRef.current = false;
          incrementPageNumber();
        }
      }
    },
    [loading, incrementPageNumber]
  );


  useEffect(() => {
    if (!loading) {
      enableFetchRef.current = true; 
    }
  }, [loading]); 

  return useMaterialReactTable({
    columns: columns,
    data: stats,
    state: { isLoading: loading },
    enablePagination: false,
    enableRowVirtualization: true,
    muiTableBodyCellProps: {
      align: "center",
      sx: {
        paddingTop: 0.5,
        paddingBottom: 0.5,
      },
    },
    muiTableHeadCellProps: {
      align: "center",
    },
    muiTableContainerProps: {
      sx: { overflowY: "auto", overflowX: "hidden", maxHeight: "450px" },
      onScroll: (e) => fetchMoreOnBottomReached(e.target),
    },
    enableBottomToolbar: false,
  });
}
