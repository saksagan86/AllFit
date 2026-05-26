import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

function KickboksDetail({ aanbod, extraBegeleiding }) {
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

                {aanbod?.map((les) => (
                    <div key={les.id} className="detail-kaart">
                        <img className="detail-foto" src={les.image} alt={`${les.naam} foto`} />
                        <div className="detail-info">
                            <h3 className="titel" style={{ margin: "0 0" }}>{les.naam}</h3>
                            {extraBegeleiding === 'met' ? (
                                <p style={{ fontSize: "13px", marginTop: "4px" }}>
                                    <strong>Extra begeleiding: </strong>{les.beschrijvingBegeleiding}
                                </p>
                            ) : (
                                <p style={{ fontSize: "14px", marginTop: "0" }}>{les.doelgroep}</p>
                            )}
                            <Link className="button" to={`/lessen/${les.id}`} state={{ naam: les.naam, extraBegeleiding: extraBegeleiding === 'met' }}>Inschrijven</Link>
                        </div>
                    </div>
                ))}
            </div>

            {aanbod?.length === 0 && (
                <p>Geen kickbokslessen gevonden voor deze sportschool.</p>
            )}


        </div>
    );
}

export default KickboksDetail;