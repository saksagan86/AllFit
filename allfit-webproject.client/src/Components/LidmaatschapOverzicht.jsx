import React, { useState } from 'react'; 
import { Link } from 'react-router-dom';
import lidmaatschappen from '../data/lidmaatschappen';
import LidmaatschapKaart from './LidmaatschapKaart';

function LidmaatschapOverzicht() {
    const [actieveTermijn, setActieveTermijn] = useState('1 maand');
    const clipGrootte = "clamp(20px, 6vw, 80px)";
    const gefilterdeLidmaatschappen = lidmaatschappen.filter(
        (lidmaatschap) => lidmaatschap.duur === actieveTermijn
    );

    return (
        <div>
            <div style={{
                backgroundColor: "#F3E5D3", padding: `calc(10px + ${clipGrootte}) 0 calc(30px + ${clipGrootte}) 0`,
                clipPath: `polygon(0 ${clipGrootte}, 100% 0, 100% calc(100% - ${clipGrootte}), 0 100%)`, marginTop:"100px" }}>
            {/* Titel*/}
            <div className="titelaanbodoverzicht">
                <h2 style={{ marginBottom: "5px" }}>Lidmaatschappen</h2>
            </div>


            <div className="lidmaatschap-knoppen">
                <button className={actieveTermijn === '1 maand' ? 'actief' : ''} onClick={() => setActieveTermijn('1 maand')}>1 maand</button>
                <button className={actieveTermijn === '3 maanden' ? 'actief' : ''} onClick={() => setActieveTermijn('3 maanden')}>3 maanden</button>
                <button className={actieveTermijn === '6 maanden' ? 'actief' : ''} onClick={() => setActieveTermijn('6 maanden')}>6 maanden</button>
                <button className={actieveTermijn === '12 maanden' ? 'actief' : ''} onClick={() => setActieveTermijn('12 maanden')}>12 maanden</button>
            </div>


                <div className="lidmaatschap-kaarten">
                    {gefilterdeLidmaatschappen.map((lid) => {
                        return (
                            <LidmaatschapKaart key={lid.id} lid={lid}>
                                <Link className="button" to="/inschrijven" state={{ lidmaatschap: lid }} style={{ textAlign: "center", margin: "15px" }}>Inschrijven</Link>
                            </LidmaatschapKaart>
                        );
                    })}
                </div>
             </div>


        </div>


    );
}
export default LidmaatschapOverzicht;