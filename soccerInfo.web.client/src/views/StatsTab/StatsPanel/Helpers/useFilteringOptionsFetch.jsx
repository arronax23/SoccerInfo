import { useEffect, useRef } from "react";
import axios from "axios";

export default function useStatsFetch() {
  let filteringOptions = useRef({
    positions: [],
    teams: [],
    leagues: [],
    nationalities: []
  });

  useEffect(() => {
    axios.get('api/GetStatsFilteringOptions')
    .then(resp => {
      const data = resp.data;
      console.log('d',data)
      filteringOptions.current = data;
    });
  }, [])

  return filteringOptions;
}
