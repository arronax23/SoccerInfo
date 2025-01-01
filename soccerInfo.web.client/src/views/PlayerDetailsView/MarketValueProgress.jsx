import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import Chart from "react-apexcharts";

const MarketValueProgress = ({ marketValueChanges }) => {

  const [series, setSeries] = useState();

  useEffect(() => {
    prepareChartSeries();
  }, []);

  
const chartOptions = {
  chart: {
    id: "area-datetime",
    type: "area",
    zoom: {
      autoScaleYaxis: true,
    },
  },
  colors: ["#fff"],
  fill: {
    type: "gradient",
    colors: ["#273469"],
    gradient: {
      type: "vertical",
      shadeIntensity: 0, 
      gradientToColors: ["#1E2749"],
      opacityFrom: 0.75,
      opacityTo: 1,
      stops: [0, 100],
    },
  },
  stroke: {
    curve: "smooth",
    width: 4,
    colors: ["#fff"],
  },        
  markers: {
    size: 5,
    colors: ["#000"],
    strokeColors: "#fff",
    strokeWidth: 2,
    shape: "circle",
    hover: {
      size: 7,
    },
  },    
  dataLabels: {
    enabled: false,
  },
  xaxis: {
    type: "datetime",
    tickAmount: 6,
  },
  yaxis: {
    labels: {
      formatter: (value) => value.toFixed(0),
    },
  },
  tooltip: {
    custom: ({ series, seriesIndex, dataPointIndex, w }) => {
      const marketValueChangeId = w.globals.seriesZ[seriesIndex][dataPointIndex]

      const age = marketValueChanges.find(x => x.id == marketValueChangeId).age;
      const imgBase64 = marketValueChanges.find(x => x.id == marketValueChangeId).teamImageBase64;
     
      let date = new Date(w.globals.seriesX[seriesIndex][dataPointIndex]).toLocaleDateString();
      let marketValue = series[seriesIndex][dataPointIndex];

      if (marketValue < 1){
        marketValue =  marketValue * 100 + "k€";
      }
      else {
        marketValue += "m€";
      }

      if(imgBase64 != null){
        return `
        <div class="market-value-tooltip">
          <div class="date row">Date: ${date}</div>
          <div class="row">Market Value: ${marketValue}</div>
          <div class="row">Age: ${age}</div>
          <div class="row"><img class="team-img" src="data:image/jpeg;base64,${imgBase64}"/></div>
        </div>
      `;
      }
      else {
        return `
        <div class="market-value-tooltip">
          <div class="date row">Date: ${date}</div>
          <div class="row">Market Value: ${marketValue}</div>
          <div class="row">Age: ${age}</div>
        </div>
      `;
      }
    },
  },
};

  return (
    series && (
      <Chart className="chart" options={chartOptions} series={series} type="area" width={700} height={300} />
    )
  );

  function prepareChartSeries(){
    const seriesData = marketValueChanges.map(change => {
      let value;
      if (change.marketValueUnit === "m") {
        value = change.marketValue;
      } else if (change.marketValueUnit === "k") {
        value = change.marketValue / 1000; 
      } else {
        value = change.marketValue;
      }

      return {
        x: new Date(change.changeDate).getTime(),
        y: value,
        z: change.id
      };
    });

    setSeries([
      {
        name: "Market Value",
        data: seriesData,
      },
    ]);    
  }

  
};


export default MarketValueProgress;
