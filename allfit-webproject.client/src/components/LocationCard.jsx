import React from 'react';
import { Link } from 'react-router-dom';

function LocationCard({ location, setLocation }) {
    return (
        <article className="location-card">
            <h2>{location.naam}</h2>
            <p className="location-city">{location.stad}</p>

            <section className="location-section">
                <h3>Adres</h3>
                <p>{location.adres}</p>
                <button className="button" onClick={setLocation}>Bekijk op kaart.</button>
            </section>

            <section className="location-section">
                <h3>Openingstijden</h3>
                <ul>
                    {location.openingstijden.map((openingstijd, index) => (
                        <li key={index}>
                            {openingstijd.dag}: {openingstijd.tijdOpen} - {openingstijd.tijdSluit}
                        </li>
                    ))}
                </ul>
            </section>

            <section className="location-section">
                <h3>Faciliteiten</h3>
                <ul>
                    {location.faciliteiten.map((facility, index) => (
                        <li key={index}>{facility.naam}</li>
                    ))}
                </ul>
            </section>

            <div className="location-button-wrapper">
            <Link to="/aanbod" state={{ selectedLocationId: location.id }} className="button">
                Bekijk ons aanbod
                </Link>
            </div>
           

        </article>
    )
}

export default LocationCard