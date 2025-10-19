export const handleTextDisplay = (text) => (text == null ? '-' : text);
export const handleDateDisplay = (date) => (date == null ? '-' : new Date(date).toLocaleDateString());
export const handleHeightDisplay = (height) => (height == null ? '-' : `${height.toFixed(2)} m`);

export const handleMarketValueDisplay = (marketValue, marketValueUnit) => (marketValue == null || marketValueUnit == null ? '-' : `${marketValue}${marketValueUnit}€`);
export const handleMarketValueProgressDisplay = (marketValueProgress, marketValueUnit) => {
    if (marketValueProgress == null || marketValueUnit == null){
        return '-';
    }
    else if (marketValueProgress > 0 ){
        return `+${marketValueProgress}${marketValueUnit}€`
    }
    else{
        return `${marketValueProgress}${marketValueUnit}€`
    } 
}
export const handleDateRangeDisplay = ({ years, months, days }) => (years == null || months == null || days == null ? '-' : `${years} Years, ${months} Months, ${days} Days`);