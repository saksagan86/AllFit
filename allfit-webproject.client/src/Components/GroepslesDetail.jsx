import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

function GroepslesDetail() {
    return (
        <div>

                <div className="detail-kaarten">

                    {/* Detail Kaart 1 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/zumba.jpg" alt="Zumba" />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>Zumba</h3>
                            <Link className="button" to="/aanbod/groepslessen">Inschrijven</Link>
                        </div>
                    </div>

                    {/* Detail Kaart 2 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/yoga.jpg" alt="Yoga" />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>Yoga</h3>
                            <Link className="button" to="/aanbod/groepslessen">Inschrijven</Link>
                        </div>
                    </div>

                    {/* Detail Kaart 3 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/hiit.jpg" alt="HIIT" />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>HIIT</h3>
                            <Link className="button" to="/aanbod/groepslessen">Inschrijven</Link>
                        </div>
                    </div>

                    {/* Detail Kaart 4 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/buikspierkwartier.jpg" alt="Buikspierkwartier" style={{ objectPosition: "85%" }} />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>Buikspierkwartier</h3>
                            <Link className="button" to="/aanbod/groepslessen">Inschrijven</Link>
                        </div>
                    </div>

                    {/* Detail Kaart 5 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/circuittraining.jpg" alt="Circuittraining" />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>Circuittraining</h3>
                            <Link className="button" to="/aanbod/groepslessen">Inschrijven</Link>
                        </div>
                   </div>
            </div>


            <div className="introductie">

                <div className="detail-introductie">

                    <ul style={{ listStyleType: "none", paddingLeft: "0px", margin: "0" }}>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Meerdere lessen per dag
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Leukste trainers van de stad
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Voor elk niveau
                        </li>
                        <li>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                             Sportieve en gezellige sfeer
                        </li>
                    </ul>

                </div>

                <div className="foto-container">
                    <img className="foto-introductie" src="/images/groepslestrainers.jpg" alt="Groepsles Trainers" />
                    <p style={{ fontsize: "14px" }}>AllFit groepsles trainers</p>
                </div>

            </div>
        </div>
    );
}

export default GroepslesDetail;