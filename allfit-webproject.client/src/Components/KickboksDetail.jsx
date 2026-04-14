import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import kickbokslessen from '../data/kickboksaanbod';

function KickboksDetail({ lessen }) {
    return (
        <div>

            <div className="introductie">

                <div className="detail-introductie">

                    <h2 className="titelaanbodoverzicht" style={{ textAlign: "start"}}>Ontdek onze Kickbokslessen</h2>

                    <p>Train je techniek, conditie en kracht met onze kickbokslessen.
                        Van beginner tot gevorderd, met focus op discipline en zelfvertrouwen.</p>

                    <ul style={{ listStyleType: "none", paddingLeft: "0px", margin: "0" }}>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Verschillende niveaus en leeftijden
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Professionele trainers
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Focus op techniek en conditie
                        </li>
                        <li>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            5 dagen per week
                        </li>
                    </ul>

                </div>

                <div className="foto-container">
                    <img className="foto-introductie" src="/images/kickbokstrainers.jpg" alt="Kickboks Trainers" />
                    <p>AllFit kickbokstrainers 'Rico & Destiny'</p>
                </div>

            </div>

            <div className="detail-kaarten">

                {kickbokslessen?.map((les) => (
                    <div key={les.id} className="detail-kaart">

                        <img className="detail-foto" src={les.image} alt={`${les.titel} foto`}/>

                        <div className="detail-info">
                            <h3 className="titel" style={{ margin: "0 0" }}>{les.titel}</h3>

                            <p style={{ fontSize: "14px", marginTop: "0" }}>{les.doelgroep}</p>

                            <Link className="button" to="/aanbod/kickboksen">Inschrijven</Link>
                        </div>

                    </div>
                ))}
            </div>


        </div>
    );
}

export default KickboksDetail;