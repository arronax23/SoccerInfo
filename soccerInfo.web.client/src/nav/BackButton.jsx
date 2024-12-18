import React from "react";
import { useNavigate, useLocation } from "react-router";

const BackButton = () => {
  const navigate = useNavigate();
  const location = useLocation();
  return (
    <div>
        {location.pathname !== '/' && (
                  <button className="go-back-btn" onClick={goBack}>
                  <img src="/left-arrow.svg" alt="back" />
                </button>
        )}
    </div>
  );

  function goBack() {
    navigate(-1);
  }
};

export default BackButton;
