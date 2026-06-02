import { useAuth } from '../context/AuthContext.jsx';
import { useState } from 'react';

function InschrijvingKaart({ inschrijving, onUitschrijven }) {
    const { token } = useAuth();
    const [melding, setMelding] = useState(null);

    const handleUitschrijven = async () => {
        try {
            const response = await fetch(`/api/inschrijving/${inschrijving.id}`, {
                method: 'DELETE',
                headers: { Authorization: `Bearer ${token}` }
            });
            if (!response.ok) {
                const data = await response.json();
                setMelding({ kleur: 'red', tekst: data.message || 'Uitschrijven mislukt' });
                return;
            }
            setMelding({ kleur: 'green', tekst: 'Uitschrijving gelukt!' });
        } catch (error) {
            setMelding({ kleur: 'red', tekst: 'Er is iets misgegaan, probeer opnieuw.' });
        }
    };

    return (
        <div className="inschrijving-kaart">
            <div className="inschrijving-info">
                <p className="inschrijving-type">{inschrijving.aanbodNaam}</p>
                <p className="inschrijving-datum">{new Date(inschrijving.datum).toLocaleDateString('nl-NL')}</p>
                <p className="inschrijving-tijd">{inschrijving.tijd.substring(0, 5)}</p>
                <div className="inschrijving-begeleiding">
                    <input type="checkbox" checked={inschrijving.extraBegeleiding} readOnly />
                    <label>Extra begeleiding</label>
                </div>
            </div>
            <button className="button" onClick={handleUitschrijven}>Uitschrijven</button>

            {melding && (
                <div className="popup-overlay">
                    <div className="popup">
                        <p style={{ color: melding.kleur }}>{melding.tekst}</p>
                        <button className="button" onClick={() => {
                            setMelding(null);
                            if (melding.kleur === 'green') onUitschrijven(inschrijving.id);
                        }}>
                            Sluiten
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
}

export default InschrijvingKaart;