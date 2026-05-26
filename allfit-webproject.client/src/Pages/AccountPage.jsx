import { useState, useEffect } from 'react';
import { useAuth } from '../context/AuthContext.jsx';

function AccountPage() {
    const { token } = useAuth();
    const [bewerkModus, setBewerkModus] = useState(false);
    const [error, setError] = useState('');
    const [formData, setFormData] = useState({
        naam: '',
        email: '',
        wachtwoord: '',
        telefoonnummer: '',
        adres: '',
        huisnummer: '',
        postcode: '',
        stad: '',
    });

    useEffect(() => {
        const fetchGegevens = async () => {
            try {
                const response = await fetch('/api/leden/account', {
                    headers: { Authorization: `Bearer ${token}` }
                });
                if (!response.ok) throw new Error('Kan gegevens niet ophalen');
                const data = await response.json();
                setFormData({
                    naam: data.naam || '',
                    email: data.email || '',
                    wachtwoord: '',
                    telefoonnummer: data.telefoonnummer || '',
                    adres: data.adres || '',
                    huisnummer: data.huisnummer || '',
                    postcode: data.postcode || '',
                    stad: data.stad || '',
                });
            } catch (error) {
                console.error(error);
            }
        };
        fetchGegevens();
    }, []);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleOpslaan = async () => {
        try {
            const response = await fetch('/api/leden/update', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify(formData)
            });
            if (!response.ok) {
                const data = await response.json();
                const fouten = Object.values(data.errors || {}).flat();
                setError(fouten[0] || 'Controleer je gegevens en probeer opnieuw.');
                return;
            }
            setError('');
            setBewerkModus(false);
        } catch (error) {
            setError('Er is iets misgegaan, probeer opnieuw.');
        }
    };

    return (
        <main className="page-container">
            <section className="auth-card">
                <h1>Mijn account</h1>

                <div className="account-details">
                    {[
                        { label: 'Naam', key: 'naam', type: 'text' },
                        { label: 'E-mailadres', key: 'email', type: 'email' },
                        { label: 'Telefoonnummer', key: 'telefoonnummer', type: 'tel' },
                        { label: 'Adres', key: 'adres', type: 'text' },
                        { label: 'Huisnummer', key: 'huisnummer', type: 'text' },
                        { label: 'Postcode', key: 'postcode', type: 'text' },
                        { label: 'Stad', key: 'stad', type: 'text' },
                    ].map(({ label, key, type }) => (
                        <div key={key} className="account-veld">
                            <strong>{label}:</strong>
                            {bewerkModus ? (
                                <input
                                    type={type}
                                    name={key}
                                    value={formData[key]}
                                    onChange={handleChange}
                                    className="account-input"
                                />
                            ) : (
                                <span>{formData[key] || 'Onbekend'}</span>
                            )}
                        </div>
                    ))}
                </div>

                {error && <p style={{ color: 'red', marginTop: '0.5rem' }}>{error}</p>}

                <div style={{ marginTop: '1rem', display: 'flex', gap: '1rem' }}>
                    {bewerkModus ? (
                        <>
                            <button className="button" onClick={handleOpslaan}>Opslaan</button>
                            <button className="button" onClick={() => setBewerkModus(false)}>Annuleren</button>
                        </>
                    ) : (
                        <button className="button" onClick={() => setBewerkModus(true)}>Gegevens wijzigen</button>
                    )}
                </div>
            </section>
        </main>
    );
}

export default AccountPage;