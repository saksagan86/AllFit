import { useNavigate } from 'react-router-dom';

function AccountOverzicht() {
    const navigate = useNavigate();

    return (
        <main className="page-container">
            <section className="auth-card">
                <h1>Mijn overzicht</h1>

                <p>
                    Bekijk hier je persoonlijke AllFit onderdelen zoals je voedingsadvies,
                    lessen en accountgegevens.
                </p>

                <div
                    style={{
                        display: 'grid',
                        gridTemplateColumns: 'repeat(auto-fit, minmax(220px, 1fr))',
                        gap: '1rem',
                        marginTop: '1.5rem'
                    }}
                >
                    <article
                        style={{
                            border: '1px solid #ddd',
                            borderRadius: '12px',
                            padding: '1rem',
                            backgroundColor: '#fff'
                        }}
                    >
                        <h2>Voedingsadvies</h2>
                        <p>Kies je doel en bekijk het voedingsadvies dat daarbij hoort.</p>

                        <button
                            className="button"
                            type="button"
                            onClick={() => navigate('/voedingsadvies')}
                        >
                            Bekijk voedingsadvies
                        </button>
                    </article>

                    <article
                        style={{
                            border: '1px solid #ddd',
                            borderRadius: '12px',
                            padding: '1rem',
                            backgroundColor: '#fff'
                        }}
                    >
                        <h2>Mijn inschrijvingen</h2>
                        <p>Bekijk hier je huidige inschrijvingen</p>

                        <button
                            className="button"
                            type="button"
                            onClick={() => navigate('/inschrijvingen')}
                        >
                            Bekijk inschrijvingen
                        </button>
                    </article>

                    <article
                        style={{
                            border: '1px solid #ddd',
                            borderRadius: '12px',
                            padding: '1rem',
                            backgroundColor: '#fff'
                        }}
                    >
                        <h2>Accountgegevens</h2>
                        <p>Bekijk en wijzig je persoonlijke gegevens.</p>

                        <button
                            className="button"
                            type="button"
                            onClick={() => navigate('/account')}
                        >
                            Bekijk gegevens
                        </button>
                    </article>
                </div>
            </section>
        </main>
    );
}

export default AccountOverzicht;