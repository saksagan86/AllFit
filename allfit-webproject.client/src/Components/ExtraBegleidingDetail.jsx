import React from 'react';
import { Link } from 'react-router-dom';

function ExtraBegeleidingDetail({ item }) {
    return (

        <div className="detail-kaart">
            <img className="detail-foto" src={item.image} alt={`${item.naam} foto`} />
            <div className="detail-info">
                <h3 className="titel" style={{ margin: "0 0" }}>{item.naam}</h3>
                <p style={{ fontSize: "14px", marginTop: "0", color: "gray" }}>{item.sportType}</p>

                <div className="begeleiding-badge">
                    <p style={{ fontSize: "14px", margin: "0pz", textAlign:"left" }}><strong>Extra begeleiding:</strong> {item.beschrijvingBegeleiding}</p>
                </div>

                <Link className="button" to={`/aanbod/${item.sportType}`}>Inschrijven</Link>
            </div>
        </div>
    );
}

export default ExtraBegeleidingDetail;