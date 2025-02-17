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

  useEffect(()=> {
    console.log('columnFilters', columnFilters);
    console.log('getPositionFilter()', getPositionsFilter());
    setRequestFilter(prev => ({
      ...prev,
       positions: getPositionsFilter(),
       teams: getTeamsFilter(),
       leagues: getLeaguesFilter(),
       nationalities: getNationalitiesFilter(),
       pageNumber: 1
    }))
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

  const getPositionsFilter = () => {
    const positonsFilter = columnFilters.find(x => x.id == "position");
    return positonsFilter ? positonsFilter.value : []
  };

  const getTeamsFilter = () => {
    const teamsFilter = columnFilters.find(x => x.id == "team");
    return teamsFilter ? teamsFilter.value : []
  };


  const getLeaguesFilter = () => {
    const leaguesFilter = columnFilters.find(x => x.id == "league");
    return leaguesFilter ? leaguesFilter.value : []
  };


  const getNationalitiesFilter = () => {
    const nationalitiesFilter = columnFilters.find(x => x.id == "nationality");
    return nationalitiesFilter ? nationalitiesFilter.value : []

  };


  // const getPositionsFilter = () => {
  //   const positonFilter = columnFilters.find(x => x.id == "position");
  //   return positonFilter ? positonFilter.value : []
  // };



  
  // const getTeamsFilter = () => {
  //   if(columnFilters.length > 0) return columnFilters.find(x => x.id == "team").value
  //   else return []
  // };


  // const getLeaguesFilter = () => {
  //   if(columnFilters.length > 0) return columnFilters.find(x => x.id == "league").value
  //   else return []
  // };


  // const getNationalitiesFilter = () => {
  //   if(columnFilters.length > 0) return columnFilters.find(x => x.id == "nationality").value
  //   else return []
  // };


  

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
