import React, { useState } from "react";
import PopUp from "./PopUp";
function Proefles() {
    const [showPopUp, setShowPopUp] = useState(false);
    const [sentForm, setSendingForm] = useState(false);
    const [locaties, setLocaties] = useState([]);
    const [lessen, setLessen] = useState([]);

    function openForm() {
        setShowPopUp(true);
        setSendingForm(false);
        fetchLocaties();
    }
    async function fetchLocaties() {
        const res = await fetch("https://localhost:7093/api/sportschool");
        if (!res.ok) {
            throw new Error("Kan sportscholen niet ophalen");
        }
        const data = await res.json();
        setLocaties(data);
    }

    async function loadLessen(sportschool) {
        if (sportschool) {
            const res = await fetch(`https:localhost:7093/api/sportscholen/${sportschool}/aanbod/all`);
            if (!res.ok) {
                throw new Error("Kan geen lessen vinden");
            }
            const data = await res.json();
            setLessen(data)
        }
    }

    // Verstuurt de form met data naar de backend om daar te verwerken.
    async function sendForm(formData) {
        setShowPopUp(false);
        setSendingForm(true);
        try {
            const response = await fetch("https://localhost:7093/api/form/proefles", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    Name: formData.get("name"),
                    Email: formData.get("email"),
                    Telefoon: formData.get("tel"),
                    SportschoolID: formData.get("locatie"),
                    LesID: formData.get("les"),
                })
            });
            if (!response.ok) {
                const text = await response.text();
                console.log("Backend error:", text);
                throw new Error(`Verzending mislukt (${response.status})`);
            }
        } catch (err) {
            console.log(err.message);
        }
    }

    return (
        <div>
            <button style={{ marginTop: '1em' }} className='button' onClick={openForm}>{sentForm ? "Bedankt voor het boeken!" : "Boek een proefles!"}</button>
            <PopUp showPopUp={showPopUp} closePopUp={() => setShowPopUp(false)}>
                <h2>"Boek een proefles!"</h2>
                <p>Vul het formulier in om een gesprek te plannen.</p>
                <form className="form-group" action={sendForm}>

                    <label htmlFor="locatie">Locatie: </label>
                    <select id="locatie" name="locatie" required onChange={e => loadLessen(e.target.value)}>
                        {locaties.map((loc) => (
                            <option key={loc.id} value={loc.id}>{loc.naam}</option>
                        ))}
                    </select>

                    <label htmlFor="les">Les: </label>
                    <select id="les" name="les" required>
                        {lessen.map((les) => (
                            <option key={les.id} value={les.id}>{les.naam}</option>
                        ))}
                    </select>

                    <label htmlFor="name">Naam: </label>
                    <input id="name" type="text" name="name" required />

                    <label htmlFor="email">Email: </label>
                    <input id="email" type="email" name="email" required />

                    <label htmlFor="tel">Telefoonnummer: </label>
                    <input id="tel" type="tel" name="tel" required />

                    <label htmlFor="akkoord">Met het versturen van de gegevens ga ik akkoord om mij te benaderen voor verdere informatie.</label>
                    <input id="akkoord" type="submit" value="Verstuur" />

                </form>
            </PopUp>
        </div>

  );
}

export default Proefles;