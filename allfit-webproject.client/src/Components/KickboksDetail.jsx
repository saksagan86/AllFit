import React from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

function KickboksDetail() {
    return (
        <div>

            

            {/*<h3 className="kopje" style={{ margin:"0px"}}>Ontdenk onze lessen</h3>*/}

                <div className="detail-kaarten" >

                    {/* Detail Kaart 1 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/kickboksenjunior1.jpg" alt="Kickboksen Junior 1" />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>Kickboksen Junior 1</h3>
                            <p style={{ fontSize: "14px", marginTop: "0" }}>4 t/m 10 jaar</p>

                            <Link className="button" to="/aanbod/kickboksen">Inschrijven</Link>
                        </div>
                    </div>

                    {/* Detail Kaart 2 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/kickboksenjunior2.jpg" alt="Kickboksen Junior 2" />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>Kickboksen Junior 2</h3>
                            <p style={{ fontSize: "14px", marginTop: "0" }}>11 t/m 16 jaar</p>

                            <Link className="button" to="/aanbod/kickboksen">Inschrijven</Link>
                        </div>
                    </div>

                    {/* Detail Kaart 3 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/kickboksenvolwassenen1.jpg" alt="Kickboksen Volwassenen 1" style={{ objectPosition: "30% 0%" }} />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>Kickboksen Volwassenen 1</h3>
                            <p style={{ fontSize: "14px", marginTop: "0" }}>17+ Beginnersklas</p>

                            <Link className="button" to="/aanbod/kickboksen">Inschrijven</Link>
                        </div>
                    </div>

                    {/* Detail Kaart 4 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/kickboksenvolwassenen2.jpg" alt="Kickboksen Volwassenen 2" />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>Kickboksen Volwassenen 2</h3>
                            <p style={{ fontSize: "14px", marginTop: "0" }}>17+ Gevorderden</p>

                            <Link className="button" to="/aanbod/kickboksen">Inschrijven</Link>
                        </div>
                    </div>

                    {/* Detail Kaart 5 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/kickboksenbokszak.jpg" alt="Bokszak Training" style={{ objectPosition:"100% 0%"}} />

                        <div className="detail-info">
                        <h3 className="titel" style={{margin: "0 0" }}>Bokszak Training</h3>
                            <p style={{ fontSize: "14px", marginTop: "0" }}>Alle leeftijden</p>

                            <Link className="button" to="/aanbod/kickboksen">Inschrijven</Link>
                        </div>
                    </div>

                    {/* Detail Kaart 6 */}
                    <div className="detail-kaart">
                        <img className="detail-foto" src="/images/kickboksenextra.jpg" alt="Kickboksen Extra" />

                        <div className="detail-info">
                            <h3 className="titel" style={{margin: "0 0" }}>Kickboksen Extra</h3>
                            <p style={{ fontSize: "14px", marginTop: "0" }}>Voor extra persoonlijke aandacht</p>

                            <Link className="button" to="/aanbod/kickboksen">Inschrijven</Link>
                        </div>
                    </div>

            </div>

            <div className="introductie">

                <div className="detail-introductie">

                    <ul style={{ listStyleType: "none", paddingLeft: "0px", margin: "0" }}>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Verschillende niveaus en leeftijden
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Professionele trainers
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            Focus op techniek en conditie
                        </li>
                        <li>
                            <FontAwesomeIcon icon={faCheck} style={{ color: "#FF8971", marginRight: "12px", fontSize: "22px" }} />
                            5 dagen per week
                        </li>
                    </ul>

                </div>

                <div className="foto-container">
                    <img className="foto-introductie" src="/images/kickbokstrainers.jpg" alt="Kickboks Trainers" />
                    <p style={{ fontsize: "14px" }}>AllFit kickbokstrainers 'Rico & Destiny'</p>
                </div>

            </div>


        </div>
    );
}

export default KickboksDetail;