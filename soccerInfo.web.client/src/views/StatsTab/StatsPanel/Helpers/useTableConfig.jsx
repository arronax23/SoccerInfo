import { useCallback, useEffect, useRef, useState } from "react";
import { useMaterialReactTable } from "material-react-table";

export default function useTableConfig(
  columns,
  stats,
  loading,
  incrementPageNumber, 
  setRequestFilter
) {
  const enableFetchRef = useRef(false);
  const [columnFilters, setColumnFilters] = useState([]);  

  // const handleFilterChange = (e) =>{
  //   console.log("filter fired")
  //   console.log(e);
  // }

  useEffect(()=> {
    console.log('columnFilters', columnFilters);
    console.log('getPositionFilter()', getPositionFilter());
    setRequestFilter(prev => ({...prev, positions: getPositionFilter(), pageNumber: 1}))
  },[columnFilters])

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

  const getPositionFilter = () => {
    if(columnFilters.length > 0) return columnFilters.find(x => x.id == "position").value
    else return []
  };

  useEffect(() => {
    if (!loading) {
      enableFetchRef.current = true; 
    }
  }, [loading]); 

  return useMaterialReactTable({
    columns: columns,
    data: stats,
    // state: { isLoading: loading },
    enablePagination: false,
    enableRowVirtualization: true,
    enableHiding: false,
    // enableFilters: false,
    // enableFilters: false,
    // onColumnFiltersChange: (e) =>handleFilterChange(e),
    // manualFiltering: true,

    manualFiltering: true, //turn off client-side filtering
    onColumnFiltersChange: setColumnFilters, //hoist internal columnFilters state to your state
    //state: { columnFilters: columnFilters, isLoading: loading, showLoadingOverlay:true,  showSkeletons: false}, //pass in your own managed columnFilters state    
    state: { columnFilters: columnFilters}, //pass in your own managed columnFilters state    
    initialState: { showColumnFilters: true },

    enableDensityToggle: false,
    muiTableBodyCellProps: {
      align: "center",
      sx: {
        padding: "0.25rem 0 0.25rem 0"
      },
    },
    muiTableHeadCellProps: {
      align: "center",
      style: { 
        padding: "0.5rem 0 0.5rem 0",
        alignSelf: "flex-start",
        borderBottom: "none"
       }, 
    },
    muiTableContainerProps: {
      sx: { overflowY: "auto", overflowX: "hidden", maxHeight: "450px" },
      onScroll: (e) => fetchMoreOnBottomReached(e.target),
    },
    enableBottomToolbar: false,
  });
}
