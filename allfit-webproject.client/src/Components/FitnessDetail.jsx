import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';
import fitnessaanbod from '../data/fitnessaanbod';

function FitnessDetail() {
    return (
        <div>

            <div className="introductie">

                <div className="detail-introductie">

                    <h2 className="titelaanbodoverzicht" style={{ textAlign:"start"}}>Ontdek onze Fitness aanbod</h2>

                    <p>Train op jouw manier in onze moderne fitnessruimtes.
                        Van krachttraining tot cardio en functioneel trainen.</p>

                    <ul style={{ listStyleType: "none", paddingLeft: "0px", margin: "0" }}>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Persoonlijke aandacht
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Ervaren trainers
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Samen gefocust op jouw doel
                        </li>
                        <li>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Altijd beschikbaar
                        </li>
                    </ul>

                </div>

                <div className="foto-container">
                    <img className="foto-introductie" src="/images/fitnesstrainers.jpg" alt="Fitness Trainers" />
                    <p>AllFit fitness trainers 'Jose & Lucia'</p>
                </div>

            </div>

            <div className="detail-kaarten">

                {fitnessaanbod?.map((fitness =>
                    <div key={fitness.id} className="detail-kaart">
                        <img className="detail-foto" src={fitness.image} alt={`${fitness.titel} foto`} />

                        <div className="detail-info">
                            <h3 className="titel" style={{ marginTop: "0" }}>{fitness.titel}</h3>
                        </div>
                    </div>
                
                ))}
            </div>
        </div>
    );

}

export default FitnessDetail;