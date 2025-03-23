import { useRef } from "react";
import { useCountUp } from "react-countup";

const Counter = ({ end }) => {
  const countUpRef = useRef(null);

  useCountUp({
    ref: countUpRef,
    start: 0,
    end,
    duration: 2.5,
    separator: " ",
  });

  return <span ref={countUpRef} />;
};

export default Counter;