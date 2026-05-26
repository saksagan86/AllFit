import { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext.jsx';

function VoedingsadviesPage() {
    const { token } = useAuth();

    const [loading, setLoading] = useState(true);
    const [adviesData, setAdviesData] = useState(null);
    const [error, setError] = useState('');
    const [opslaanBezig, setOpslaanBezig] = useState(false);

    useEffect(() => {
        haalMijnSchemaOp();
    }, []);

    const haalMijnSchemaOp = async () => {
        setLoading(true);
        setError('');

        try {
            const response = await fetch('/api/Voedingsadvies/mijn-schema', {
                method: 'GET',
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            const data = await response.json();

            if (!response.ok) {
                setError(data.message || 'Voedingsadvies kon niet worden opgehaald.');
                return;
            }

            setAdviesData(data);
        } catch {
            setError('Er is iets misgegaan bij het ophalen van je voedingsadvies.');
        } finally {
            setLoading(false);
        }
    };

    const kiesDoel = async (doelId) => {
        setOpslaanBezig(true);
        setError('');

        try {
            const response = await fetch('/api/Voedingsadvies/kies-doel', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify({ doelId })
            });

            const data = await response.json();

            if (!response.ok) {
                setError(data.message || 'Doel kon niet worden opgeslagen.');
                return;
            }

            setAdviesData(data);
        } catch {
            setError('Er is iets misgegaan bij het opslaan van je doel.');
        } finally {
            setOpslaanBezig(false);
        }
    };

    if (loading) {
        return (
            <main className="page-container">
                <section className="auth-card">
                    <h1>Voedingsadvies</h1>
                    <p>Laden...</p>
                </section>
            </main>
        );
    }

    const doelen = adviesData?.beschikbareDoelen || [];

    return (
        <main className="page-container">
            <section className="auth-card">
                <h1>Voedingsadvies</h1>

                {error && (
                    <p className="form-error" role="alert">
                        {error}
                    </p>
                )}

                {adviesData && adviesData.heeftDoel === false && (
                    <section style={{ marginTop: '1.5rem' }}>
                        <h2>Kies je doel</h2>
                        <p>Kies een doel om jouw voedingsadvies te bekijken.</p>

                        <div
                            style={{
                                display: 'grid',
                                gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))',
                                gap: '1rem',
                                marginTop: '1rem'
                            }}
                        >
                            {doelen.map((doel) => (
                                <article
                                    key={doel.id}
                                    style={{
                                        border: '1px solid #ddd',
                                        borderRadius: '12px',
                                        padding: '1rem',
                                        backgroundColor: '#fff'
                                    }}
                                >
                                    <h3>{doel.naam}</h3>
                                    <p>{doel.beschrijving}</p>

                                    <button
                                        className="button"
                                        type="button"
                                        disabled={opslaanBezig}
                                        onClick={() => kiesDoel(doel.id)}
                                    >
                                        {opslaanBezig ? 'Opslaan...' : 'Kies dit doel'}
                                    </button>
                                </article>
                            ))}
                        </div>
                    </section>
                )}

                {adviesData && adviesData.heeftDoel === true && (
                    <>
                        <section style={{ marginTop: '1.5rem' }}>
                            <h2>Jouw doel</h2>

                            <article
                                style={{
                                    border: '1px solid #ddd',
                                    borderRadius: '12px',
                                    padding: '1rem',
                                    backgroundColor: '#fff'
                                }}
                            >
                                <h3>{adviesData.doel.naam}</h3>
                                <p>{adviesData.doel.beschrijving}</p>
                            </article>
                        </section>

                        <section style={{ marginTop: '1.5rem' }}>
                            <h2>{adviesData.schema.titel}</h2>
                            <p>{adviesData.schema.beschrijving}</p>

                            <div style={{ marginTop: '1rem' }}>
                                {adviesData.schema.regels.map((regel) => (
                                    <article
                                        key={regel.id}
                                        style={{
                                            borderBottom: '1px solid #ddd',
                                            padding: '1rem 0'
                                        }}
                                    >
                                        <h3>
                                            {regel.volgorde}. {regel.maaltijdMoment}
                                        </h3>
                                        <p>{regel.advies}</p>
                                    </article>
                                ))}
                            </div>
                        </section>

                        <section style={{ marginTop: '2rem' }}>
                            <h2>Doel wijzigen</h2>

                            <div
                                style={{
                                    display: 'flex',
                                    flexWrap: 'wrap',
                                    gap: '1rem',
                                    marginTop: '1rem'
                                }}
                            >
                                {doelen.map((doel) => (
                                    <button
                                        key={doel.id}
                                        className="button"
                                        type="button"
                                        disabled={opslaanBezig || doel.id === adviesData.doel.id}
                                        onClick={() => kiesDoel(doel.id)}
                                    >
                                        {doel.naam}
                                    </button>
                                ))}
                            </div>
                        </section>
                    </>
                )}
            </section>
        </main>
    );
}

export default VoedingsadviesPage;