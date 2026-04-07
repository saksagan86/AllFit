import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

function FitnessDetail() {
    return (
        <div>

            <div className="aanbod-container">

                <div className="detail-kaarten">

                    {/* Detail Kaart 1 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/krachttraining.jpg" alt="Krachttraining ruimte" />

                        <div className="detail-info">
                            <h3 className="titel" style={{marginTop: "0" }}>Krachttraining Ruimte</h3>
                        </div>
                    </div>

                    {/* Detail Kaart 2 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/vrijegewichten.jpg" alt="Vrije gewichten ruimte" />

                        <div className="detail-info">
                            <h3 className="titel" style={{marginTop: "0" }}>Vrije gewichten</h3>
                        </div>
                    </div>   

                    {/* Detail Kaart 3 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/cardioruimte.jpg" alt="Cardio ruimte" />

                        <div className="detail-info">
                            <h3 className="titel" style={{marginTop: "0" }}>Cardio Ruimte</h3>
                        </div>
                    </div>

                    {/* Detail Kaart 4 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/openruimte.jpg" alt="Open ruimte" />

                        <div className="detail-info">
                            <h3 className="titel" style={{marginTop: "0" }}>Open Ruimte</h3>
                        </div>
                    </div>   

                </div>
            </div>

            <div className="introductie">

                <div className="detail-introductie">

                    <ul style={{ listStyleType: "none", paddingLeft: "0px", margin: "0" }}>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Persoonlijke aandacht
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Ervaren trainers 
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Samen gefocust op jou doel
                        </li>
                        <li>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Altijd beschikbaar
                        </li>
                    </ul>

                </div>

                <div className="foto-container">
                    <img className="foto-introductie" src="/images/fitnesstrainers.jpg" alt="Fitness Trainers"/>
                    <p style={{ fontsize: "14px" }}>AllFit fitness trainers 'Jose & Lucia'</p>
                </div>

            </div>

        </div>
    );

}

export default FitnessDetail;