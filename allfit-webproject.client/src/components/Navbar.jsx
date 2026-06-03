import { useState, useEffect } from 'react'
import { NavLink, useNavigate } from 'react-router-dom'
import { useAuth } from '../context/AuthContext'

function Navbar() {
    const [isOpen, setIsOpen] = useState(false)
    const [dropdownOpen, setDropdownOpen] = useState(false)
    const { isAuthenticated, logout } = useAuth()
    const [accountDropdownOpen, setAccountDropdownOpen] = useState(false)
    const navigate = useNavigate()

    const closeMenu = () => setIsOpen(false);

    const getLinkClass = ({ isActive }) =>
        isActive ? 'nav-link active' : 'nav-link'

    const handleLogout = () => {
        logout();
        closeMenu();
        navigate('/');
    }

    const [sportscholen, setLocations] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchSportscholen = async () => {
            try {
                const response = await fetch("https://localhost:7093/api/sportschool/navbar");
                if (!response.ok) throw new Error("Kan sportscholen niet ophalen");
                const data = await response.json();
                setLocations(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };
        fetchSportscholen();
    }, []);

    return (
        <header className="navbar">
            <div className="navbar-container">
                <NavLink to="/" className="logo" onClick={closeMenu} aria-label="AllFit home">
                    <img src="/images/allfit-logo.png" alt="AllFit logo" className="navbar-logo-img" />
                </NavLink>

                <button
                    className="hamburger"
                    onClick={() => setIsOpen(!isOpen)}
                    aria-label="Open menu"
                    aria-expanded={isOpen}
                >
                    ☰
                </button>

                <nav className={isOpen ? 'nav-menu open' : 'nav-menu'}>
                    <NavLink to="/" end className={getLinkClass} onClick={closeMenu}>
                        Home
                    </NavLink>

                    <NavLink to="/locaties" className={getLinkClass} onClick={closeMenu}>
                        Locaties
                    </NavLink>

                    <div
                        className="dropdown-container"
                        onMouseEnter={() => setDropdownOpen(true)}
                        onMouseLeave={() => setDropdownOpen(false)}
                    >
                        <span
                            className="nav-link dropdown-trigger"
                            style={{ userSelect: "none", cursor: "pointer" }}
                            onClick={() => setDropdownOpen(!dropdownOpen)}
                        >
                            Aanbod ▾
                        </span>

                        {dropdownOpen && (
                            <div className="dropdown-menu">
                                {sportscholen.map((sportschool) => (
                                    <NavLink
                                        key={sportschool.id}
                                        to="/aanbod"
                                        state={{ selectedLocationId: sportschool.id }}
                                        className="dropdown-item"
                                        onClick={closeMenu}
                                    >
                                        {sportschool.naam}
                                    </NavLink>
                                ))}
                            </div>
                        )}
                    </div>

                    <NavLink to="/community" className={getLinkClass} onClick={closeMenu}>
                        Community
                    </NavLink>

                    <NavLink to="/contact" className={getLinkClass} onClick={closeMenu}>
                        Contact
                    </NavLink>

                    {!isAuthenticated ? (
                        <NavLink to="/login" className={getLinkClass} onClick={closeMenu}>
                            Inloggen
                        </NavLink>
                    ) : (
                        <div
                            className="dropdown-container"
                            onMouseEnter={() => setAccountDropdownOpen(true)}
                            onMouseLeave={() => setAccountDropdownOpen(false)}
                        >
                            <span
                                className="nav-link dropdown-trigger"
                                style={{ userSelect: "none", cursor: "pointer" }}
                                onClick={() => setAccountDropdownOpen(!accountDropdownOpen)}
                            >
                                Account ▾
                            </span>

                            {accountDropdownOpen && (
                                <div className="dropdown-menu">
                                    <NavLink to="/account/overzicht" className="dropdown-item" onClick={closeMenu}>
                                        Overzicht
                                    </NavLink>
                                    <button
                                        type="button"
                                        className="dropdown-item logout-button"
                                        onClick={handleLogout}
                                    >
                                        Uitloggen
                                    </button>
                                </div>
                            )}
                        </div>
                    )}
                </nav>
            </div>
        </header>
    )
}

export default Navbar