import React from 'react';

function LidmaatschapKaart({ lid, className = "", style = {}, children }) {

    const eersteBetaling = (lid.inschrijfgeld + lid.lidmaatschapgeld).toFixed(2);

    return (
        <div className={`lidmaatschap-kaart ${className}`} style={{ ...style }}>
            <h3 className="titel">{lid.naam}</h3>
            <p style={{ fontSize: "12px" }}>
                <span style={{ fontSize: "30px", fontWeight: "bold", color: "#CC6753" }}>
                    &euro;{lid.lidmaatschapgeld}
                </span> p/m
            </p>

            <ul style={{ listStyleType: "none", paddingLeft: "0", marginTop: "15px" }}>
                <li style={{ marginBottom: "15px", fontSize: "14px", display: "flex", alignItems: "center" }}>
                    <span className="lidmaatschap-bullet oranje"></span>
                    Inschrijfkosten &euro;{lid.inschrijfgeld}
                </li>
                <li style={{ marginBottom: "15px", fontSize: "14px", display: "flex", alignItems: "center" }}>
                    <span className="lidmaatschap-bullet oranje"></span>
                    {lid.duur} contract
                </li>
                <li style={{ marginBottom: "15px", fontSize: "14px", display: "flex", alignItems: "center" }}>
                    <span className="lidmaatschap-bullet oranje"></span>
                    {lid.opzegtermijn} opzegtermijn
                </li>
                <li style={{ marginBottom: "15px", fontSize: "14px", display: "flex", alignItems: "center" }}>
                    <span className="lidmaatschap-bullet oranje"></span>
                    {lid.beschrijving}
                </li>
                <li style={{ fontSize: "14px", display: "flex", alignItems: "center", fontWeight: "bold", borderTop: "1px solid #eee", paddingTop: "10px" }}>
                    <span className="lidmaatschap-bullet grijs"></span>
                    Eerste betaling: &euro;{eersteBetaling}
                </li>
            </ul>

            {children}
        </div>
    );
}

export default LidmaatschapKaart;