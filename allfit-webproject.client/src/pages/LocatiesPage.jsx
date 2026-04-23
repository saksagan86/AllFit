import LocationCard from '../Components/LocationCard'
import locations from '../data/locations'
import LocationsMap from "../Components/LocationsMap";


function LocatiesPage() {
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
                    <LocationCard key={location.id} location={location} />
                ))}
            </section>
            <LocationsMap></LocationsMap>

        </main>
    )
}

export default LocatiesPage