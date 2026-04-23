import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import GegevensBox from '../Components/GegevensBox';
import LidmaatschapKaart from '../Components/LidmaatschapKaart';

function InschrijfPage() {
    const location = useLocation();
    const navigate = useNavigate();
    const lidmaatschap = location.state?.lidmaatschap;

    const eersteBetaling = lidmaatschap
        ? (lidmaatschap.inschrijfgeld + lidmaatschap.lidmaatschapgeld).toFixed(2)
        : "0.00";

    const persoonlijkeVelden = [
        { naam: "Voornaam", type: "text" },
        { naam: "Achternaam", type: "text" },
        { naam: "Email", type: "email" },
        { naam: "Telefoonnummer", type: "tel" },
        { naam: "Geboortedatum", type: "date" },
        { naam: "Wachtwoord", type: "password" },
        { naam: "Bevestig wachtwoord", type: "password" }
    ];

    const adresVelden = [
        { naam: "Adres", type: "text" },
        { naam: "Huisnummer", type: "text" },
        { naam: "Stad", type: "text" },
        { naam: "Postcode", type: "tel" },
    ];

    const alleVelden = [...persoonlijkeVelden, ...adresVelden];

    const initialFormData = {};
    alleVelden.forEach(f => {
        const id = f.naam.toLowerCase().replace(/\s+/g, '-');
        initialFormData[id] = "";
    });

    const [formData, setFormData] = useState(initialFormData);
    const [akkoord, setAkkoord] = useState(false);
    const [nieuwsbrief, setNieuwsbrief] = useState(false);
    const [error, setError] = useState("");

    const handleChange = (id, value) => {
        setFormData(prev => ({
            ...prev,
            [id]: value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const alleVeldenIngevuld = Object.values(formData).every(
            (value) => value && value.trim() !== ""
        );

        if (!alleVeldenIngevuld || !akkoord) {
            setError("Vul je gegevens aan en/of ga akkoord met onze algemene voorwaarden");
            return;
        }

        // wachtwoorden velden controleren nog

        setError("");
        // Hier kunnen we nog een API maken
        navigate('/aanbod');
    };

    return (
        <div style={{ display: "flex", alignItems: "flex-start", gap: "40px" }}>

            <form onSubmit={handleSubmit} className="inschrijf-links" style={{ display: "flex", flexDirection: "column", alignItems: "flex-start", flex: 1 }}>

                <div className="titelaanbodoverzicht">
                    <h2>Inschrijven</h2>
                </div>

                <GegevensBox titel="Persoonlijke Gegevens" gegevens={persoonlijkeVelden} formData={formData} onChange={handleChange} />

                <GegevensBox titel="Adres Gegevens" gegevens={adresVelden} formData={formData} onChange={handleChange}/>

                <div style={{ display: "flex", flexDirection: "column", alignItems: "flex-start", margin: "20px 0px" }}>
                    <div>
                        <input type="checkbox" id="voorwaarden" checked={akkoord} onChange={(e) => setAkkoord(e.target.checked)} />
                        <label htmlFor="voorwaarden">Ik ga akkoord met de algemene voorwaarden</label>
                    </div>

                    <div>
                        <input type="checkbox" id="nieuwsbrief" checked={nieuwsbrief} onChange={(e) => setNieuwsbrief(e.target.checked)} />
                        <label htmlFor="nieuwsbrief">Ik wil de nieuwsbrief graag ontvangen</label>
                    </div>

                    <button type="submit" className="button" style={{ margin: "20px 0px", width: "175px", textAlign: "center" }}>Inschrijven</button>

                    {error && (<p style={{ color: "red", marginTop: "10px" }}> {error} </p>)}
                </div>

            </form>

            <LidmaatschapKaart lid={lidmaatschap} className="inschrijf-rechts" style={{ position: "sticky", marginLeft: "auto", top: "25vh", border: "1px solid #E67862", flexShrink: "0" }} />

        </div>
    );
}

export default InschrijfPage;