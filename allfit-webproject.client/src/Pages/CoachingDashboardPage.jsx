import { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext.jsx';

function CoachingDashboardPage() {
    const { token } = useAuth();

    const [dashboard, setDashboard] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [opslaanBezig, setOpslaanBezig] = useState(false);

    useEffect(() => {
        haalDashboardOp();
    }, []);

    const haalDashboardOp = async () => {
        setLoading(true);
        setError('');

        try {
            const response = await fetch('/api/Coaching/dashboard', {
                method: 'GET',
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            const data = await response.json();

            if (!response.ok) {
                setError(data.message || 'Dashboard kon niet worden opgehaald.');
                return;
            }

            setDashboard(data);
        } catch {
            setError('Er is iets misgegaan bij het ophalen van je coaching dashboard.');
        } finally {
            setLoading(false);
        }
    };

    const kiesDoel = async (doelId) => {
        setOpslaanBezig(true);
        setError('');

        try {
            const response = await fetch('/api/Coaching/kies-doel', {
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

            setDashboard(data);
        } catch {
            setError('Er is iets misgegaan bij het opslaan van je doel.');
        } finally {
            setOpslaanBezig(false);
        }
    };

    const rondTrainingAf = async (trainingsDag) => {
        setOpslaanBezig(true);
        setError('');

        try {
            const response = await fetch('/api/Coaching/training-afronden', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify({
                    trainingsDag,
                    notitie: 'Training afgerond via het dashboard.'
                })
            });

            const data = await response.json();

            if (!response.ok) {
                setError(data.message || 'Training kon niet worden opgeslagen.');
                return;
            }

            setDashboard(data);
        } catch {
            setError('Er is iets misgegaan bij het opslaan van je voortgang.');
        } finally {
            setOpslaanBezig(false);
        }
    };

    if (loading) {
        return (
            <main className="page-container">
                <section className="auth-card dashboard-card">
                    <h1>Mijn coaching</h1>
                    <p>Laden...</p>
                </section>
            </main>
        );
    }

    const doelen = dashboard?.beschikbareDoelen || [];
    const progress = dashboard?.progress || {
        afgerondDezeWeek: 0,
        weekDoel: 0,
        percentage: 0
    };

    return (
        <main className="page-container">
            <section className="auth-card dashboard-card">
                <h1>Mijn coaching</h1>

                <p className="dashboard-intro">
                    Stel je sportdoel in, bekijk je trainingsschema en houd je voortgang bij.
                </p>

                {error && (
                    <p className="form-error" role="alert">
                        {error}
                    </p>
                )}

                {dashboard && dashboard.heeftDoel === false && (
                    <section style={{ marginTop: '1.5rem' }}>
                        <h2>Kies je doel</h2>
                        <p>
                            Kies een doel. Op basis hiervan wordt jouw trainingsschema en
                            voortgangsoverzicht opgebouwd.
                        </p>

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

                {dashboard && dashboard.heeftDoel === true && (
                    <>
                        <section style={{ marginTop: '1.5rem' }}>
                            <h2>Jouw actieve doel</h2>

                            <article
                                style={{
                                    border: '1px solid #ddd',
                                    borderRadius: '12px',
                                    padding: '1rem',
                                    backgroundColor: '#fff'
                                }}
                            >
                                <h3>{dashboard.doel.naam}</h3>
                                <p>{dashboard.doel.beschrijving}</p>
                            </article>
                        </section>

                        <section style={{ marginTop: '1.5rem' }}>
                            <h2>Voortgang deze week</h2>

                            <div
                                style={{
                                    border: '1px solid #ddd',
                                    borderRadius: '12px',
                                    padding: '1rem',
                                    backgroundColor: '#fff'
                                }}
                            >
                                <p>
                                    {progress.afgerondDezeWeek} van {progress.weekDoel}{' '}
                                    trainingen afgerond
                                </p>

                                <div
                                    role="progressbar"
                                    aria-valuenow={progress.percentage}
                                    aria-valuemin="0"
                                    aria-valuemax="100"
                                    aria-label="Wekelijkse trainingsvoortgang"
                                    style={{
                                        width: '100%',
                                        height: '18px',
                                        backgroundColor: '#f3f3f3',
                                        borderRadius: '999px',
                                        overflow: 'hidden',
                                        marginTop: '0.75rem'
                                    }}
                                >
                                    <div
                                        style={{
                                            width: `${progress.percentage}%`,
                                            height: '100%',
                                            backgroundColor: '#3DC2C7'
                                        }}
                                    />
                                </div>

                                <strong style={{ display: 'block', marginTop: '0.75rem' }}>
                                    {progress.percentage}%
                                </strong>
                            </div>
                        </section>

                        <section style={{ marginTop: '1.5rem' }}>
                            <h2>Trainingsschema</h2>

                            <div
                                style={{
                                    display: 'grid',
                                    gridTemplateColumns: 'repeat(auto-fit, minmax(240px, 1fr))',
                                    gap: '1rem',
                                    marginTop: '1rem'
                                }}
                            >
                                {dashboard.trainingsschema.map((training) => (
                                    <article
                                        key={`${training.dag}-${training.titel}`}
                                        style={{
                                            border: '1px solid #ddd',
                                            borderRadius: '12px',
                                            padding: '1rem',
                                            backgroundColor: '#fff'
                                        }}
                                    >
                                        <h3>
                                            {training.dag}: {training.titel}
                                        </h3>

                                        <ul>
                                            {training.oefeningen.map((oefening) => (
                                                <li key={oefening}>{oefening}</li>
                                            ))}
                                        </ul>

                                        <button
                                            className="button"
                                            type="button"
                                            disabled={opslaanBezig}
                                            onClick={() =>
                                                rondTrainingAf(
                                                    `${training.dag} - ${training.titel}`
                                                )
                                            }
                                        >
                                            Training afgerond
                                        </button>
                                    </article>
                                ))}
                            </div>
                        </section>

                        <section style={{ marginTop: '1.5rem' }}>
                            <h2>Historie</h2>

                            {dashboard.historie.length === 0 ? (
                                <p>Je hebt nog geen trainingen afgerond.</p>
                            ) : (
                                <div style={{ marginTop: '1rem' }}>
                                    {dashboard.historie.map((item) => (
                                        <article
                                            key={item.id}
                                            style={{
                                                borderBottom: '1px solid #ddd',
                                                padding: '0.75rem 0'
                                            }}
                                        >
                                            <strong>{item.trainingsDag}</strong>
                                            <p>
                                                Afgerond op:{' '}
                                                {new Date(item.afgerondOp).toLocaleDateString(
                                                    'nl-NL'
                                                )}
                                            </p>
                                            {item.notitie && <p>{item.notitie}</p>}
                                        </article>
                                    ))}
                                </div>
                            )}
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
                                        disabled={
                                            opslaanBezig || doel.id === dashboard.doel.id
                                        }
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

export default CoachingDashboardPage;