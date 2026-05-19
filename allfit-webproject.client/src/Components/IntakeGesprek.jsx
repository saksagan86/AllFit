import React, { useState } from "react";
import PopUp from "./PopUp";

function IntakeGesprek() {
    const [showPopUp, setShowPopUp] = useState(false);

    function openForm() {
        setShowPopUp(true);
    }

    // Verstuurt de form met data naar de backend om daar te verwerken.
    async function sendForm(formData) {
        try {
            const response = await fetch("https://localhost:7093/api/form/submit", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    Naam: formData.get("name"),
                    Email: formData.get("email"),
                    Telefoon: formData.get("tel"),
                    Beschrijving: formData.get("description")
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
            <button style={{ marginTop: '1em' }} className='button' onClick={openForm}>Plan een gesprek!</button>
            <PopUp showPopUp={showPopUp} closePopUp={() => setShowPopUp(false)}>
               <h2>Plan een gesprek</h2>
                <p>Vul het formulier in om een gesprek te plannen.</p>
                <form className="form-group" action={sendForm}>
                    <label>Naam: </label>
                    <input type="text" name="name" required />
                    <label>Email: </label>
                    <input type="email" name="email" required />
                    <label>Telefoonnummer: </label>
                    <input type="tel" name="tel" required />
                    <label>Beschrijving: </label>
                    <input type="text" name="description" maxLength="100" />
                    <label>Met het versturen van de gegevens ga ik akkoord om mij te benaderen voor verdere informatie. </label>
                    <input type="submit" value="Verstuur" />
                </form>
            </PopUp>
        </div>

  );
}

export default IntakeGesprek;