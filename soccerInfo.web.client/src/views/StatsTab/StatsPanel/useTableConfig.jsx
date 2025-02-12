import { useCallback } from "react";
import { useMaterialReactTable } from "material-react-table";

export default function useTableConfig(columns, stats, loading, incrementPageNumber) {
    const fetchMoreOnBottomReached = useCallback(
        (containerRefElement) => {
          if (containerRefElement) {
            const { scrollHeight, scrollTop, clientHeight } = containerRefElement;
            if (
              scrollHeight - scrollTop - clientHeight < 100 && !loading
            ) {
            incrementPageNumber();
            // setPageNumber(prevPageNumber => prevPageNumber + 1)
            }
          }
        },
        [loading],
      );
    

    return useMaterialReactTable({
        columns: columns,
        data: stats,
        state: { isLoading: loading },
        enablePagination: false,
        enableRowVirtualization: true,
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
          sx: { overflowY: "auto",overflowX: "hidden", maxHeight: "450px"},
          onScroll: (e) =>
            fetchMoreOnBottomReached(e.target),           
        },
        enableBottomToolbar: false
      });
}
