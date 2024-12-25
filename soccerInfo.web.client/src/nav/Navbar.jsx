import React from "react";
import { Link } from "react-router";

const Navbar = () => {
  return (
    <nav className="navbar">
      <Link className="nav-home not-decorated" to="/">
        <img className="logo" src={"/soccer-favicon.svg"} />
        <h1 className="name">Soccer Info</h1>
      </Link>

      <div className="nav-links">
        <Link className="not-decorated" to="/">
          <div className="nav-item">Leagues</div>
        </Link>
        <div className="nav-item">Teams</div>
        <Link className="not-decorated" to="/players">
        <div className="nav-item">Players</div>
        </Link>
        <div className="nav-item">Stats</div>
      </div>
    </nav>
  );
};

export default Navbar;
