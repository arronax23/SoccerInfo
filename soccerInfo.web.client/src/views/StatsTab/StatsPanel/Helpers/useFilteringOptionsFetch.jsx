import { useEffect, useRef } from "react";
import axios from "axios";

export default function useStatsFetch() {
  let filteringOptions = useRef({
    positions: [],
    teams: [],
    leagues: [],
    nationalities: []
  });
  // const fetched = useRef();

  useEffect(() => {
    // if (fetched.current) return; // Skip if already fetched
    // fetched.current = true;

    axios.get('api/GetStatsFilteringOptions')
    .then(resp => {
      const data = resp.data;
      console.log('d',data)
      filteringOptions.current = data;
    });
  }, []);


  // useEffect(() => {
  //   const fetchFilteringOptions = async () => {
  //     try {
  //       const response = await axios.get("api/GetStatsFilteringOptions");
  //       setFilteringOptions(response.data);
  //     } catch (error) {
  //       console.error("Error fetching filtering options:", error);
  //     }
  //   };

  //   if (!filteringOptions) {
  //     fetchFilteringOptions();
  //   }
  // }, []);

  return filteringOptions.current;
}
