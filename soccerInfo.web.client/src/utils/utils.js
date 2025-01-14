export const handleTextDisplay = (text) => (text == null ? '-' : text);
export const handleDateDisplay = (date) => (date == null ? '-' : new Date(date).toLocaleDateString());
export const handleHeightDisplay = (height) => (height == null ? '-' : `${height.toFixed(2)} m`);