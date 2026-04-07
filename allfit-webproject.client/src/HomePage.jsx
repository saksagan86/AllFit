import ScrollBlock from "./scrollBlock";
import InfoBlock from "./assets/infoBlock"

function HomePage() {
    const allFitInfo = [
        <InfoBlock title="Over AllFit" description="Allfit is een inclusieve sportschool waar community centraal staat!" foto_url="https://cdn.pixabay.com/photo/2022/06/29/13/31/power-club-7291776_1280.jpg" ></InfoBlock >,
        <InfoBlock title="Proeflessen" description="Benieuwd naar ons aanbod? Boek een proefles!" foto_url="https://cdn.pixabay.com/photo/2022/06/29/13/31/power-club-7291776_1280.jpg"></InfoBlock>,
        <InfoBlock title="Sporten met een beperking" description="Bij AllFit is het ook mogelijk om te sporten met een beperking. Benieuwd? Boek een kennismakingsgesprek" foto_url="https://cdn.pixabay.com/photo/2022/06/29/13/31/power-club-7291776_1280.jpg"></InfoBlock>
    ];

    //let locations = [{}]

    return (
        <div>
            {/*<NavBar></NavBar>*/}
            <ScrollBlock
                items={allFitInfo}></ScrollBlock>
            {/*<ScrollBlock*/}
            {/*    items={locations}></ScrollBlock>*/}
            {/*<Lidmaatschappen></Lidmaatschappen>*/}
        </div>
  );
}

export default HomePage;