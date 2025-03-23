import { NavLink } from "react-router";

const Navbar = () => {
  return (
    <nav className="navbar">
      <NavLink className="nav-home nav-link" to="/">
        <img className="logo" src={"/soccer-favicon.svg"} />
        <h1 className="name">Soccer Info</h1>
      </NavLink>
      <div className="nav-links">
        <NavLink className="nav-link" to="/">
          <div className="nav-item">Home</div>
        </NavLink>
        <NavLink className="nav-link" to="/leagues">
          <div className="nav-item">Leagues</div>
        </NavLink>        
        <div className="nav-item">Teams</div>
        <NavLink className="nav-link" to="/players">
          <div className="nav-item">Players</div>
        </NavLink>
        <NavLink className="nav-link" to="/stats">
          <div className="nav-item">Stats</div>
        </NavLink>
      </div>
    </nav>
  );
};

export default Navbar;
