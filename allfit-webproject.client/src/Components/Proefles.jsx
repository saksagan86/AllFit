import React, { useState } from "react";
import PopUp from "./PopUp";
function Proefles() {
    const [showPopUp, setShowPopUp] = useState(false);
    const [locaties, setLocaties] = useState([]);
    const [lessen, setLessen] = useState([]);

    function openForm() {
        setShowPopUp(true);
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
            const res = await fetch(`https:localhost:7093/api/sportscholen/${sportschool}/aanbod/groepslessen`);
            if (!res.ok) {
                throw new Error("Kan geen lessen vinden");
            }
            const data = await res.json();
            setLessen(data)
        }
        console.log(`${sportschool}: ${lessen}`)
    }

    // Verstuurt de form met data naar de backend om daar te verwerken.
    async function sendForm(formData) {
        try {
            const response = await fetch("https://localhost:7093/api/proefles/submit", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    Locatie: formData.get("locatie"),
                    Les: formData.get("les"),
                    Naam: formData.get("name"),
                    Email: formData.get("email"),
                    Telefoon: formData.get("tel"),
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
        setShowPopUp(false)
    }

    return (
        <div>
            <button style={{ marginTop: '1em' }} className='button' onClick={openForm}>Boek een proefles!</button>
            <PopUp showPopUp={showPopUp} closePopUp={() => setShowPopUp(false)}>
                <h2>"Boek een proefles!"</h2>
                <p>Vul het formulier in om een gesprek te plannen.</p>
                <form className="form-group" action={sendForm}>
                    <label>Locatie: </label>
                    <select name="locatie" required onChange={e => loadLessen(e.target.value)}>
                    {locaties.map((loc) => (
                        <option key={loc.id} value={loc.id}>{loc.naam}</option>
                    ))}
                    </select>
                    <label>Les: </label>
                    <select name="les" required>
                        {lessen.map((les) => (
                            <option key={les.id} value={les.id}>{les.naam}</option>
                        ))}
                    </select>
                    <label>Naam: </label>
                    <input type="text" name="name" required />
                    <label>Email: </label>
                    <input type="email" name="email" required />
                    <label>Telefoonnummer: </label>
                    <input type="tel" name="tel" required />
                    <label>Met het versturen van de gegevens ga ik akkoord om mij te benaderen voor verdere informatie. </label>
                    <input type="submit" value="Verstuur" />
                </form>
            </PopUp>
        </div>

  );
}

export default Proefles;