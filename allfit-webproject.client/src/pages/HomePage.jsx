import ScrollBlock from "../Components/ScrollBlock";
import InfoBlock from "../Components/infoBlock"
import Proefles from "../Components/Proefles"
import LocationCard from "../Components/LocationCard";
import LidmaatschapOverzicht from "../Components/LidmaatschapOverzicht";
import { useEffect, useState } from 'react';

function HomePage() {

    const [locations, setLocations] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

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

    const proeflesButton = <Proefles></Proefles>
    const allFitInfo = [
        <InfoBlock title="Over AllFit" description="Allfit is een inclusieve sportschool waar community centraal staat!" foto_url="https://cdn.pixabay.com/photo/2022/06/29/13/31/power-club-7291776_1280.jpg" buttoninhoud="Proefles aanvragen!" ></InfoBlock >,
        <InfoBlock title="Proeflessen" description="Benieuwd naar ons aanbod? Boek een proefles!" foto_url="https://cdn.pixabay.com/photo/2022/06/29/13/31/power-club-7291776_1280.jpg" custombutton={proeflesButton}></InfoBlock>,
        <InfoBlock title="Sporten met een beperking" description="Bij AllFit is het ook mogelijk om te sporten met een beperking. Benieuwd? Bekijk onze aanbod met extra begleidingsopties" foto_url="https://cdn.pixabay.com/photo/2022/06/29/13/31/power-club-7291776_1280.jpg" buttoninhoud="Bekijk aanbod" link="/aanbod/extraopties"></InfoBlock>
    ];

    return (
        <main>
            <ScrollBlock
                items={allFitInfo}></ScrollBlock>
            <section className="locations-grid">
                {locations.map((location) => (
                    <LocationCard key={location.id} location={location} />
                ))}
            </section>
            <LidmaatschapOverzicht></LidmaatschapOverzicht>
        </main>
  );
}

export default HomePage;