import LocationCard from '../components/LocationCard'
import locations from '../data/locations'

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
        </main>
    )
}

export default LocatiesPage