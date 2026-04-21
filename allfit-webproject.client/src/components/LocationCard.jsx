function LocationCard({ location }) {
    return (
        <article className="location-card">
            <h2>{location.name}</h2>
            <p className="location-city">{location.city}</p>

            <section className="location-section">
                <h3>Adres</h3>
                <p>{location.address}</p>
            </section>

            <section className="location-section">
                <h3>Openingstijden</h3>
                <ul>
                    {location.openingHours.map((hours, index) => (
                        <li key={index}>{hours}</li>
                    ))}
                </ul>
            </section>

            <section className="location-section">
                <h3>Faciliteiten</h3>
                <ul>
                    {location.facilities.map((facility, index) => (
                        <li key={index}>{facility}</li>
                    ))}
                </ul>
            </section>
        </article>
    )
}

export default LocationCard