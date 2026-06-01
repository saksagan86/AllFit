import { useState, useEffect } from 'react';
import { useAuth } from '../context/AuthContext.jsx';
import InschrijvingKaart from '../Components/InschrijvingKaart.jsx';

function InschrijvingenPagina() {
    const { token } = useAuth();
    const [inschrijvingen, setInschrijvingen] = useState([]);
    const [loading, setLoading] = useState(false);

    const handleUitschrijven = (id) => {
        setInschrijvingen(prev => prev.filter(i => i.id !== id));
    };

    useEffect(() => {
        const fetchInschrijvingen = async () => {
            setLoading(true);
            try {
                const response = await fetch('/api/inschrijving', {
                    headers: { Authorization: `Bearer ${token}` }
                });
                if (!response.ok) throw new Error('Kan inschrijvingen niet ophalen');
                const data = await response.json();
                setInschrijvingen(data);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        };
        fetchInschrijvingen();
    }, []);

    return (
        <div style={{ maxWidth: 900, margin: '40px auto', padding: '0 5%' }}>
            <h1>Mijn inschrijvingen</h1>
            {loading && <p>Laden...</p>}
            {!loading && inschrijvingen.map((inschrijving) => (
                <InschrijvingKaart key={inschrijving.id} inschrijving={inschrijving} onUitschrijven={handleUitschrijven} />
            ))}
            {!loading && inschrijvingen.length === 0 && (
                <p>Geen inschrijvingen gevonden.</p>
            )}
        </div>
    );
}

export default InschrijvingenPagina;