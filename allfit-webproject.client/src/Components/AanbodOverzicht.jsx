import React, { useState, useEffect } from 'react';
import { Link, useLocation } from 'react-router-dom';
import locatiesData from '../data/locations';
import HuidigeSportschool from './HuidigeSportschool';


function AanbodOverzicht() {
    const routerLocation = useLocation();

    const [gekozenLocatie, setGekozenLocatie] = useState('');

    useEffect(() => {
        if (routerLocation.state?.selectedLocation) {
            setGekozenLocatie(routerLocation.state.selectedLocation);
        }
    }, [routerLocation.state]);

    const huidigeLocatie = gekozenLocatie || (locatiesData && locatiesData[0]?.city) || '';


    return (
        <div className="aanbod-container">

            <HuidigeSportschool locaties={locatiesData} geselecteerdeLocatie={gekozenLocatie} alsLocatieVerandert={setGekozenLocatie} />

            <div className="titelaanbodoverzicht" style={{ marginTop: "0px", marginBottom: "10px" }}>
                <h2 style={{ marginTop: "0px", marginBottom: "5px" }}>Ontdek ons Sportaanbod</h2>
                <div className="lijntje"></div>
            </div>
            <div className="aanbod-kaarten">

                {/* Kaart 1: Fitness */}
                <div className="aanbod-kaart">

                    <img className="aanbodfoto" src="./images/fotofitness.jpg" alt="Fitness" style={{ objectPosition: "50% 10%" }} />

                    <div className="aanbodinfo">
                        <div className="linkerkant">
                            <h3 className="titel" style={{ fontSize: "20px", padding: "0px 10px" }}>Fitness</h3>
                            <Link
                                className="button"
                                to="/aanbod/fitness"
                                state={{ selectedLocation: huidigeLocatie }}
                            >
                                Bekijk details
                            </Link>
                        </div>
                        <div className="rechterkant">
                            <p>Kom trainen in onze ruime en moderne fitnessruimte, voorzien van de allernieuwste apparatuur. Of je focus nu ligt op zware krachttraining in de free-weight zone of het verbeteren van je conditie, wij hebben alles in huis. Ervaar de vrijheid om jouw persoonlijke sportdoelen te behalen!</p>
                        </div>
                    </div>
                </div>

                {/* Kaart 2: Groepslessen */}
                <div className="aanbod-kaart">

                    <img className="aanbodfoto" src="/images/fotogroepsles.jpg" alt="Groepslessen" style={{ objectPosition: "0% 40%" }} />

                    <div className="aanbodinfo">
                        <div className="linkerkant">
                            <h3 className="titel" style={{ fontSize: "20px", padding: "0px 10px" }}>Groepslessen</h3>
                            <Link
                                className="button"
                                to="/aanbod/groepslessen"
                                state={{ selectedLocation: huidigeLocatie }}
                            >
                                Bekijk details
                            </Link>
                        </div>
                        <div className="rechterkant">
                            <p>Ontdek de perfecte balans tussen ontspanning en inspanning met ons diverse lesaanbod. Van rustgevende Yoga tot intensieve Zumba, er is altijd een groepsles die bij jouw voorkeur past. Laat je motiveren door de sterke groepsdynamiek en onze enthousiaste topinstructeurs!</p>
                        </div>
                    </div>
                </div>

                {/* Kaart 3: Kickboksen */}
                <div className="aanbod-kaart">

                    <img className="aanbodfoto" src="/images/fotokickboks.jpg" alt="Kickboksen" style={{ objectPosition: "0% 60%" }} />

                    <div className="aanbodinfo">
                        <div className="linkerkant">
                            <h3 className="titel" style={{ fontSize: "20px", padding: "0px 10px" }}>Kickboksen</h3>
                            <Link
                                className="button"
                                to="/aanbod/kickboksen"
                                state={{ selectedLocation: huidigeLocatie }}
                            >
                                Bekijk details
                            </Link>
                        </div>
                        <div className="rechterkant">
                            <p>Verleg je grenzen, bouw een ijzersterke conditie op en verbeter je techniek bij onze professionele kickbokstrainingen. Of je nu een beginner bent of al vergevorderd, iedereen wordt op zijn eigen niveau uitgedaagd. Train samen in een veilige, respectvolle omgeving!</p>
                        </div>
                    </div>
                </div>

            </div>
        </div>


    );
}
export default AanbodOverzicht;