import ScrollBlock from "../Components/ScrollBlock";
import InfoBlock from "../Components/infoBlock"
import Navbar from "../Components/Navbar";
import AanbodOverzicht from "../Components/AanbodOverzicht";
import LocationCard from "../Components/LocationCard";
import locations from '../data/locations';
import LidmaatschapOverzicht from "../Components/LidmaatschapOverzicht"
import LocatiesPage from "./LocatiesPage";
import LocationsMap from "../Components/LocationsMap";

function HomePage() {
    const allFitInfo = [
        <InfoBlock title="Over AllFit" description="Allfit is een inclusieve sportschool waar community centraal staat!" foto_url="https://cdn.pixabay.com/photo/2022/06/29/13/31/power-club-7291776_1280.jpg" ></InfoBlock >,
        <InfoBlock title="Proeflessen" description="Benieuwd naar ons aanbod? Boek een proefles!" foto_url="https://cdn.pixabay.com/photo/2022/06/29/13/31/power-club-7291776_1280.jpg"></InfoBlock>,
        <InfoBlock title="Sporten met een beperking" description="Bij AllFit is het ook mogelijk om te sporten met een beperking. Benieuwd? Boek een kennismakingsgesprek" foto_url="https://cdn.pixabay.com/photo/2022/06/29/13/31/power-club-7291776_1280.jpg"></InfoBlock>
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