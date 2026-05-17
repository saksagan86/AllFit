import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import GegevensBox from '../Components/GegevensBox';
import LidmaatschapKaart from '../Components/LidmaatschapKaart';

function InschrijfPage() {
    const location = useLocation();
    const navigate = useNavigate();
    const lidmaatschap = location.state?.lidmaatschap;

    //Eerste betaling wordt berekend
    const eersteBetaling = lidmaatschap
        ? (lidmaatschap.inschrijfgeld + lidmaatschap.lidmaatschapgeld).toFixed(2)
        : "0.00";

    //Persoonlijke gegevens
    const persoonlijkeVelden = [
        { naam: "Voornaam", type: "text" },
        { naam: "Achternaam", type: "text" },
        { naam: "Email", type: "email" },
        { naam: "Telefoonnummer", type: "tel" },
        { naam: "Geboortedatum", type: "date" },
        { naam: "Wachtwoord", type: "password" },
        { naam: "Bevestig wachtwoord", type: "password" }
    ];

    //Adres gegevens
    const adresVelden = [
        { naam: "Adres", type: "text" },
        { naam: "Huisnummer", type: "text" },
        { naam: "Stad", type: "text" },
        { naam: "Postcode", type: "tel" },
    ];

    console.log(lidmaatschap);

    //Combineren van alle gegevens
    const alleVelden = [...persoonlijkeVelden, ...adresVelden];

    //Een statelist aanmaken voor alle gegevens
    const initialFormData = {};
    alleVelden.forEach(f => {
        const id = f.naam.toLowerCase().replace(/\s+/g, '-');
        initialFormData[id] = "";
    });

    //Alle states m.b.t. het registreren
    const [formData, setFormData] = useState(initialFormData);
    const [akkoord, setAkkoord] = useState(false);
    const [nieuwsbrief, setNieuwsbrief] = useState(false);
    const [error, setError] = useState("");
    const [submitted, setSubmitted] = useState(false); // track if user tried to submit

    //Bij het veranderen van een veld, update de formData state en eventuele errors of submitted flag
    const handleChange = (id, value) => {
        setFormData(prev => ({
            ...prev,
            [id]: value
        }));

        if (error) setError("");
        if (submitted) setSubmitted(false);
    };

    //Alle velden moeten ingevuld zijn en correct zijn voordat de gebruiker kan submitten.
    const alleVeldenIngevuld = Object.values(formData).every(
        (value) => value && value.trim() !== ""
    );

    //Derived state berekend uit andere states
    const wachtwoord = (formData['wachtwoord'] || "");
    const bevestigWachtwoord = (formData['bevestig-wachtwoord'] || "");
    const wachtwoordenGelijk = wachtwoord === bevestigWachtwoord && wachtwoord.length > 0;
    const submitDisabled = !alleVeldenIngevuld; 

    //Bij het klikken van submit voeren we deze methode uit
    const handleSubmit = async (e) => {
        e.preventDefault();
        setSubmitted(true); // user attempted submit

        if (!akkoord) {
            setError("Ga akkoord met onze algemene voorwaarden");
            return;
        }

        if (!wachtwoordenGelijk) {
            setError("De wachtwoorden komen niet overeen.");
            return;
        }

        if (wachtwoord.length < 6) {
            setError("Wachtwoord moet minimaal 6 tekens bevatten.");
            return;
        }

        try {
            const response = await fetch("https://localhost:7093/api/leden", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    naam: formData["voornaam"] + " " + formData["achternaam"],
                    email: formData["email"],
                    wachtwoord: wachtwoord,
                    bevestigWachtwoord: bevestigWachtwoord,
                    telefoonnummer: formData["telefoonnummer"],
                    geboortedatum: formData["geboortedatum"],
                    adres: formData["adres"],
                    huisnummer: formData["huisnummer"],
                    woonplaats: formData["stad"],
                    postcode: formData["postcode"],
                    accepteerVoorwaarden: akkoord,
                    lidmaatschapId: lidmaatschap?.id
                })
            });

           
            if (!response.ok) {
                const text = await response.text();
                console.log("Backend error:", text);
                throw new Error(`Registratie mislukt (${response.status})`);
            }

            navigate("/aanbod");

        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div style={{ display: "flex", alignItems: "flex-start", gap: "40px" }}>

            <form onSubmit={handleSubmit} className="inschrijf-links" style={{ display: "flex", flexDirection: "column", alignItems: "flex-start", flex: 1 }}>

                <div className="titelaanbodoverzicht">
                    <h2>Inschrijven</h2>
                </div>

                <GegevensBox titel="Persoonlijke Gegevens" gegevens={persoonlijkeVelden} formData={formData} onChange={handleChange} />

                <GegevensBox titel="Adres Gegevens" gegevens={adresVelden} formData={formData} onChange={handleChange} />

                <div style={{ display: "flex", flexDirection: "column", alignItems: "flex-start", margin: "20px 0px" }}>
                    <div>
                        <input type="checkbox" id="voorwaarden" checked={akkoord} onChange={(e) => { setAkkoord(e.target.checked); if (error) setError(""); if (submitted) setSubmitted(false); }}/>
                        <label htmlFor="voorwaarden">Ik ga akkoord met de algemene voorwaarden</label>
                    </div>

                    <div>
                        <input type="checkbox" id="nieuwsbrief" checked={nieuwsbrief} onChange={(e) => setNieuwsbrief(e.target.checked)} />
                        <label htmlFor="nieuwsbrief">Ik wil de nieuwsbrief graag ontvangen</label>
                    </div>

                    <button type="submit" className="button" style={{ margin: "20px 0px", width: "175px", textAlign: "center",  cursor: submitDisabled ? 'not-allowed' : 'pointer' }} disabled={submitDisabled}>
                        Inschrijven
                    </button>

                    <p style={{ color: "red", marginTop: "10px" }}> {error} </p>

                </div>

            </form>

            <LidmaatschapKaart lid={lidmaatschap} className="inschrijf-rechts" style={{ position: "sticky", marginLeft: "auto", top: "25vh", border: "1px solid #E67862", flexShrink: "0" }} />

        </div>
    );
}

export default InschrijfPage;