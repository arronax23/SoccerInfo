import {
  handleMarketValueDisplay,
  handleDateRangeDisplay,
} from "../../../../utils/formatter";
import { Box } from "@mui/material";

export default function getTableData(criteriaText, data, filteringOptions, navigate) {
  console.log('f',filteringOptions)
  const sharedColumns = [
    { accessorKey: "index", header: "#", enableColumnFilter: false, size: 70 },
    // { accessorKey: "name", header: "Name",enableColumnFilter: false,filterVariant: "select",filterSelectOptions: ["Robert Lewandowski", "Canada", "Brazil", "Germany", "France"]  },
    { accessorKey: "name", header: "Name", enableColumnFilter: false, size: 130 },
    {
      accessorKey: "portrait",
      header: "Portrait",
      enableColumnFilter: false,
      size: 100,
      Cell: ({ row }) => (
        <div className="img-cell-wrapper">
          <Box
            component="img"
            sx={{
              height: 55,
              borderRadius: 1,
            }}
            src={`data:image/jpeg;base64,${row.original.faceImageBase64}`}
          />
        </div>
      ),
    },
    {
      accessorKey: "position",
      header: "Position",
      size: 120,
      filterVariant: "multi-select",
      filterSelectOptions: filteringOptions.positions,
    },    
    {
      accessorKey: "nationality",
      header: "Nationalities",
      size: 120,
      filterVariant: "multi-select",
      filterSelectOptions: filteringOptions.nationalities,
      Cell: ({ row }) =>
        row.original.nationalities.map((n) => (
          <div key={n.name} title={n.name} className="img-cell-wrapper">
            <Box
              component="img"
              className="nationality-img"
              sx={{
                height: 30,
                marginRight: 1,
                borderRadius: 1,
                border: "2px solid black"
              }}
              src={`data:image/svg+xml; base64, ${n.imageBase64}`}
            />
          </div>
        )),
    },    
    {
      accessorKey: "team",
      header: "Team",
      size: 90,
      filterVariant: "multi-select",
      filterSelectOptions: filteringOptions.teams,
      Cell: ({ row }) => (
        <div onClick={() => handleLogoClick("team", row.original.team.teamId)} title={row.original.team.name} className="img-cell-wrapper clickable">
          <Box
            component="img"
            sx={{
              height: 55,
            }}
            src={`data:image/jpeg;base64,${row.original.team.teamImageBase64}`}
          />
        </div>
      ),
    },
    {
      accessorKey: "league",
      header: "League",
      size: 90,
      filterVariant: "multi-select",
      filterSelectOptions: filteringOptions.leagues,
      Cell: ({ row }) => (
        <div onClick={() => handleLogoClick("league", row.original.league.leagueId)}  title={row.original.league.name} className="img-cell-wrapper clickable">
          <Box
            component="img"
            sx={{
              height: 55,
            }}
            src={`data:image/jpeg;base64,${row.original.league.leagueImageBase64}`}
          />
        </div>
      ),
    },
  ];


  const marketValueColumns = [
    { accessorKey: "marketValueFormatted", header: "Market Value", enableColumnFilter: false, size: 100 },
  ];

  const goalsColumns = [{ accessorKey: "goals", header: "Goals", enableColumnFilter: false, size: 100 }];

  const assitsColumns = [{ accessorKey: "assists", header: "Assists", enableColumnFilter: false, size: 100 }];

  const goalsAndAssistsColumns = [
    { accessorKey: "goalsAndAssists", header: "Goals and Assists", enableColumnFilter: false, size: 100 },
  ];

  const cleanSheetsColumns = [
    { accessorKey: "cleanSheets", header: "Clean Sheets", enableColumnFilter: false, size: 100 },
  ];

  const goalsConcededColumns = [
    { accessorKey: "goalsConceded", header: "Goals Conceded", enableColumnFilter: false, size: 100 },
  ];

  const heightColumns = [{ accessorKey: "height", header: "Height", enableColumnFilter: false, size: 100 }];

  const ageColumns = [{ accessorKey: "age", header: "Age", enableColumnFilter: false, }];

  const contractDurationColumns = [
    { accessorKey: "contractDuration", header: "Contract Duration", enableColumnFilter: false, },
  ];

  const prepareMarketValueData = (data) => {
    return data.map((item) => ({
      ...item,
      marketValueFormatted: handleMarketValueDisplay(
        item.marketValue,
        item.marketValueUnit
      ),
    }));
  };

  const prepareAgeData = (data) => {
    return data.map((item) => ({
      ...item,
      age: handleDateRangeDisplay(item.age),
    }));
  };

  const prepareContractDurationData = (data) => {
    return data.map((item) => ({
      ...item,
      contractDuration: handleDateRangeDisplay(item.contractPeriod),
    }));
  };

  const handleLogoClick = (type, id) => {
    if (type === "league")
      navigate(`/league/${id}`)
    else if (type === "team")
      navigate(`/team/${id}`)
  }

  switch (criteriaText) {
    case "Market Value":
      return {
        columns: [...sharedColumns, ...marketValueColumns],
        data: prepareMarketValueData(data),
      };
    case "Goals":
      return {
        columns: [...sharedColumns, ...goalsColumns],
        data: data,
      };
    case "Assists":
      return {
        columns: [...sharedColumns, ...assitsColumns],
        data: data,
      };
    case "Goals and Assists":
      return {
        columns: [...sharedColumns, ...goalsAndAssistsColumns],
        data: data,
      };
    case "Clean Sheets":
      return {
        columns: [...sharedColumns, ...cleanSheetsColumns],
        data: data,
      };
    case "Goals Condeded":
      return {
        columns: [...sharedColumns, ...goalsConcededColumns],
        data: data,
      };
    case "Height":
      return {
        columns: [...sharedColumns, ...heightColumns],
        data: data,
      };
    case "Age":
      return {
        columns: [...sharedColumns, ...ageColumns],
        data: prepareAgeData(data),
      };
    case "Contract Duration":
      return {
        columns: [...sharedColumns, ...contractDurationColumns],
        data: prepareContractDurationData(data),
      };
    default:
      break;
  }
};
