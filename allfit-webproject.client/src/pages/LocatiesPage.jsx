import LocationCard from '../Components/LocationCard'
import LocationsMap from "../Components/LocationsMap";
import { useEffect, useState } from 'react';


function LocatiesPage() {


    const [locations, setLocations] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");
    const [selectedLocation, setSelectedLocation] = useState(null);

    useEffect(() => {
        const fetchSportscholen = async () => {
            try {

                const response = await fetch("https://localhost:7093/api/sportschool");
                if (!response.ok) {
                    throw new Error("Kan sportscholen niet ophalen");
                }
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
        <main className="locations-page">
            <header className="locations-header">
                <h1>Onze locaties</h1>
                <p>
                    Bekijk onze vestigingen, openingstijden en faciliteiten in Delft en
                    Den Haag.
                </p>
            </header>

            <section className="locations-grid">
                {locations.map((location) => (
                    <LocationCard key={location.id} location={location} setLocation={() => setSelectedLocation(location)} />
                ))}
            </section>
            <LocationsMap coordinateLonLat={selectedLocation ? selectedLocation.coordinaten : [0, 0]} />

        </main>
    )
}

export default LocatiesPage