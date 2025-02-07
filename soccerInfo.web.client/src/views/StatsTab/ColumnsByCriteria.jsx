import {
  handleMarketValueDisplay,
  handleDateRangeDisplay,
} from "../../utils/formatter";
import { Box } from "@mui/material";
export const getTableColumns = (criteria, data) => {
  const sharedColumns = [
    { accessorKey: "index", header: "#" },
    { accessorKey: "name", header: "Name" },
    {
      accessorKey: "faceImageBase64",
      header: "Portrait",
      Cell: ({ row }) => (
        <div className="img-cell-wrapper">
          <Box
            component="img"
            sx={{
              height: 55,
              borderRadius: 1
            }}
            src={`data:image/jpeg;base64,${row.original.faceImageBase64}`}
          />
        </div>
      ),
    },
    {
      accessorKey: "teamImageBase64",
      header: "Team",
      Cell: ({ row }) => (
        <div className="img-cell-wrapper">
          <Box
            component="img"
            sx={{
              height: 55,
            }}
            src={`data:image/jpeg;base64,${row.original.teamImageBase64}`}
          />
        </div>
      ),
    },
    {
      accessorKey: "leagueImageBase64",
      header: "League",
      Cell: ({ row }) => (
        <div className="img-cell-wrapper">
          <Box
            component="img"
            sx={{
              height: 55
            }}
            src={`data:image/jpeg;base64,${row.original.leagueImageBase64}`}
          />
        </div>
      ),
    },
  ];

  const marketValueColumns = [
    { accessorKey: "marketValueFormatted", header: "Market Value" },
  ];

  const goalsColumns = [{ accessorKey: "goals", header: "Goals" }];

  const assitsColumns = [{ accessorKey: "assists", header: "Assists" }];

  const goalsAndAssistsColumns = [
    { accessorKey: "goalsAndAssists", header: "Goals and Assists" },
  ];

  const cleanSheetsColumns = [
    { accessorKey: "cleanSheets", header: "Clean Sheets" },
  ];

  const goalsConcededColumns = [
    { accessorKey: "goalsConceded", header: "Goals Conceded" },
  ];

  const heightColumns = [{ accessorKey: "height", header: "Height" }];

  const ageColumns = [{ accessorKey: "age", header: "Age" }];

  const contractDurationColumns = [
    { accessorKey: "contractDuration", header: "Contract Duration" },
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

  switch (criteria.Text) {
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
