import React from 'react';
import { Link } from 'react-router-dom';

function AanbodOverzicht() {
    return (
        <div className="aanbod-container">

            {/* Titel Aanbodoverzicht */}
            <div className="titelaanbodoverzicht">
                <h2 style={{ marginBottom:"5px" }}>Ontdek ons Sportaanbod</h2>
                <div className="lijntje"></div>
            </div>

            <div className="aanbod-kaarten">

                {/* Kaart 1: Fitness */}
                <div className="aanbod-kaart">

                    <img className="aanbodfoto" src="./images/fotofitness.jpg" alt="Fitness" style={{ objectPosition: "50% 10%" }} />

                    <div className="aanbodinfo">
                        <div className="linkerkant">
                            <h3 className="titel" style={{ fontSize: "23px"}}>Fitness</h3>
                            <Link className="button" to="/aanbod/fitness">Bekijk details</Link>
                        </div>
                        <div className="rechterkant">
                            <p>Kom vrij trainen in onze ruime en moderne fitnessruimte, volledig voorzien van de allernieuwste apparatuur. Of je nu voornamelijk focust op krachttraining in de uitgebreide free-weight zone of juist werkt aan je conditie, je vindt hier alles wat je zoekt. Ervaar de ultieme vrijheid om op jouw eigen tempo je persoonlijke sportdoelen te bereiken.</p>
                        </div>
                    </div>
                </div>

                {/* Kaart 2: Groepslessen */}
                <div className="aanbod-kaart">

                    <img className="aanbodfoto" src="/images/fotogroepsles.jpg" alt="Groepslessen" style={{ objectPosition: "0% 40%" }} />

                    <div className="aanbodinfo">
                        <div className="linkerkant">
                            <h3 className="titel" style={{ fontSize: "23px" }}>Groepslessen</h3>
                            <Link className="button" to="/aanbod/groepslessen">Bekijk details</Link>
                        </div>
                        <div className="rechterkant">
                            <p>Ontdek de perfecte balans tussen inspanning en ontspanning met ons diverse en uitdagende lesaanbod. Van rustgevende Yoga-sessies tot energieke Zumba-workouts, er is altijd wel een les die perfect bij jouw behoeftes past. Laat je meeslepen door de groepsdynamiek en ervaar hoe samen sporten met onze topinstructeurs je enorm zal motiveren!</p>
                        </div>
                    </div>
                </div>

                {/* Kaart 3: Kickboksen */}
                <div className="aanbod-kaart">

                    <img className="aanbodfoto" src="/images/fotokickboks.jpg" alt="Kickboksen" style={{ objectPosition: "0% 60%" }} />

                    <div className="aanbodinfo">
                        <div className="linkerkant">
                            <h3 className="titel" style={{ fontSize: "23px" }}>Kickboksen</h3>
                            <Link className="button" to="/aanbod/kickboksen">Bekijk details</Link>
                        </div>
                        <div className="rechterkant">
                            <p>Verleg elke training je grenzen, bouw een ijzersterke conditie op en verbeter je techniek tijdens onze professionele kickbokstrainingen. Of je nu een beginner bent of al vergevorderd, iedereen is welkom om op zijn eigen niveau uitgedaagd te worden. Werk in een veilige en respectvolle omgeving samen aan de sterkste versie van jezelf.</p>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    );
}
    export default AanbodOverzicht;