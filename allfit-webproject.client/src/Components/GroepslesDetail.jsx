import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import groepslesaanbod from '../data/groepslesaanbod';

function GroepslesDetail() {
    return (
        <div>

            <div className="introductie">

                <div className="detail-introductie">

                    <h2 className="titelaanbodoverzicht" style={{ textAlign: "start" }}>Ontdek onze Groepslessen</h2>

                    <p>Blijf gemotiveerd met onze energieke groepslessen.
                        Van ontspannende sessies tot intensieve workouts, voor elk niveau.</p>

                    <ul style={{ listStyleType: "none", paddingLeft: "0px", margin: "0" }}>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Meerdere lessen per dag
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Leukste trainers van de stad
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Voor elk niveau
                        </li>
                        <li>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Sportieve en gezellige sfeer
                        </li>
                    </ul>

                </div>

                <div className="foto-container">
                    <img className="foto-introductie" src="/images/groepslestrainers.jpg" alt="Groepsles Trainers" />
                    <p>AllFit groepsles trainers</p>
                </div>

            </div>

            <div className="detail-kaarten">

                {groepslesaanbod?.map((les) => (
                    <div key={les.id} className="detail-kaart">
                        <img className="detail-foto" src={les.image} alt={`${les.titel} foto`} />

                        <div className="detail-info">

                            <h3 className="titel" style={{ margin: "0 0" }}>{les.titel}</h3>

                            <div className="les-specificaties">
                                <p><strong>Trainer: </strong>{les.trainer}</p>
                                <p><strong>Niveau: </strong>{les.niveau}</p>
                                <p><strong>Duur: </strong>{les.duur} minuten</p>
                            </div>

                            <Link className="button" to="/aanbod/groepslessen">Inschrijven</Link>
                        </div>
                    </div>
                ))}
            </div>


        </div>
    );
}

export default GroepslesDetail;