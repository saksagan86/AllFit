import { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext.jsx';

function CoachingDashboardPage() {
    const { token } = useAuth();

    const [dashboard, setDashboard] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const [opslaanBezig, setOpslaanBezig] = useState(false);
    const [toonProfielForm, setToonProfielForm] = useState(false);

    const [profielForm, setProfielForm] = useState({
        doelId: '',
        leeftijd: '',
        lengteCm: '',
        gewichtKg: '',
        doelGewichtKg: '',
        activiteitniveau: 'Gemiddeld',
        doelTermijnMaanden: 6
    });

    const [weekForm, setWeekForm] = useState({
        gewichtKg: '',
        notitie: ''
    });

    useEffect(() => {
        haalDashboardOp();
    }, []);

    const vulProfielForm = (data) => {
        if (!data) return;

        if (data.heeftProfiel && data.profiel) {
            setProfielForm({
                doelId: data.doel?.id || '',
                leeftijd: data.profiel.leeftijd || '',
                lengteCm: data.profiel.lengteCm || '',
                gewichtKg: data.profiel.gewichtKg || '',
                doelGewichtKg: data.profiel.doelGewichtKg || '',
                activiteitniveau: data.profiel.activiteitniveau || 'Gemiddeld',
                doelTermijnMaanden: data.profiel.doelTermijnMaanden || 6
            });

            setWeekForm({
                gewichtKg: data.profiel.gewichtKg || '',
                notitie: ''
            });
            return;
        }

        setProfielForm((huidig) => ({
            ...huidig,
            doelId: data.doel?.id || data.beschikbareDoelen?.[0]?.id || ''
        }));
    };

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
            vulProfielForm(data);
        } catch {
            setError('Er is iets misgegaan bij het ophalen van je coaching dashboard.');
        } finally {
            setLoading(false);
        }
    };

    const handleProfielChange = (event) => {
        const { name, value } = event.target;

        setProfielForm((huidig) => ({
            ...huidig,
            [name]: value
        }));
    };
    const handleWeekChange = (event) => {
        const { name, value } = event.target;

        setWeekForm((huidig) => ({
            ...huidig,
            [name]: value
        }));
    };

    const slaWeekVoortgangOp = async (event) => {
        event.preventDefault();

        setOpslaanBezig(true);
        setError('');

        try {
            const response = await fetch('/api/Coaching/week-voortgang', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify({
                    gewichtKg:
                        weekForm.gewichtKg === ''
                            ? null
                            : Number(weekForm.gewichtKg),
                    notitie: weekForm.notitie
                })
            });

            const data = await response.json();

            if (!response.ok) {
                setError(data.message || 'Weekvoortgang kon niet worden opgeslagen.');
                return;
            }

            setDashboard(data);
            vulProfielForm(data);
        } catch {
            setError('Er is iets misgegaan bij het opslaan van je weekvoortgang.');
        } finally {
            setOpslaanBezig(false);
        }
    };

    const slaProfielOp = async (event) => {
        event.preventDefault();

        setOpslaanBezig(true);
        setError('');

        const aanvraag = {
            doelId: Number(profielForm.doelId),
            leeftijd: Number(profielForm.leeftijd),
            lengteCm: Number(profielForm.lengteCm),
            gewichtKg: Number(profielForm.gewichtKg),
            doelGewichtKg:
                profielForm.doelGewichtKg === ''
                    ? null
                    : Number(profielForm.doelGewichtKg),
            activiteitniveau: profielForm.activiteitniveau,
            doelTermijnMaanden: Number(profielForm.doelTermijnMaanden)
        };

        if (
            !aanvraag.doelId ||
            !aanvraag.leeftijd ||
            !aanvraag.lengteCm ||
            !aanvraag.gewichtKg
        ) {
            setError('Vul alle verplichte velden in.');
            setOpslaanBezig(false);
            return;
        }

        try {
            const response = await fetch('/api/Coaching/profiel', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify(aanvraag)
            });

            const data = await response.json();

            if (!response.ok) {
                setError(data.message || 'Profiel kon niet worden opgeslagen.');
                return;
            }

            setDashboard(data);
            vulProfielForm(data);
            setToonProfielForm(false);
        } catch {
            setError('Er is iets misgegaan bij het opslaan van je profiel.');
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
            vulProfielForm(data);
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

    const moetProfielInvullen = dashboard && dashboard.heeftProfiel === false;
    const profielFormZichtbaar = moetProfielInvullen || toonProfielForm;

    return (
        <main className="page-container">
            <section className="auth-card dashboard-card">
                <h1>Mijn coaching</h1>

                <p className="dashboard-intro">
                    Vul je doel en basisgegevens in. AllFit gebruikt deze gegevens om een
                    BMI-indicatie, persoonlijk voedingsadvies, trainingsschema en
                    weekvoortgang te tonen.
                </p>

                {error && (
                    <p className="form-error" role="alert">
                        {error}
                    </p>
                )}

                {profielFormZichtbaar && (
                    <section className="dashboard-section">
                        <h2>
                            {moetProfielInvullen
                                ? 'Persoonlijk advies instellen'
                                : 'Profiel wijzigen'}
                        </h2>

                        <form onSubmit={slaProfielOp}>
                            <div className="dashboard-grid">
                                <div className="form-group">
                                    <label htmlFor="doelId">Doel</label>
                                    <select
                                        id="doelId"
                                        name="doelId"
                                        value={profielForm.doelId}
                                        onChange={handleProfielChange}
                                        required
                                    >
                                        <option value="">Kies een doel</option>
                                        {doelen.map((doel) => (
                                            <option key={doel.id} value={doel.id}>
                                                {doel.naam}
                                            </option>
                                        ))}
                                    </select>
                                </div>

                                <div className="form-group">
                                    <label htmlFor="leeftijd">Leeftijd</label>
                                    <input
                                        id="leeftijd"
                                        name="leeftijd"
                                        type="number"
                                        min="12"
                                        max="100"
                                        value={profielForm.leeftijd}
                                        onChange={handleProfielChange}
                                        required
                                    />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="lengteCm">Lengte in cm</label>
                                    <input
                                        id="lengteCm"
                                        name="lengteCm"
                                        type="number"
                                        min="100"
                                        max="250"
                                        step="0.1"
                                        value={profielForm.lengteCm}
                                        onChange={handleProfielChange}
                                        required
                                    />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="gewichtKg">Gewicht in kg</label>
                                    <input
                                        id="gewichtKg"
                                        name="gewichtKg"
                                        type="number"
                                        min="30"
                                        max="250"
                                        step="0.1"
                                        value={profielForm.gewichtKg}
                                        onChange={handleProfielChange}
                                        required
                                    />
                                </div>
                                <div className="form-group">
                                    <label htmlFor="doelGewichtKg">Doelgewicht in kg</label>
                                    <input
                                        id="doelGewichtKg"
                                        name="doelGewichtKg"
                                        type="number"
                                        min="30"
                                        max="250"
                                        step="0.1"
                                        value={profielForm.doelGewichtKg}
                                        onChange={handleProfielChange}
                                        placeholder="Bijv. 78"
                                    />
                                </div>

                                <div className="form-group">
                                    <label htmlFor="activiteitniveau">Activiteitniveau</label>
                                    <select
                                        id="activiteitniveau"
                                        name="activiteitniveau"
                                        value={profielForm.activiteitniveau}
                                        onChange={handleProfielChange}
                                    >
                                        <option value="Laag">Laag / beginner</option>
                                        <option value="Gemiddeld">Gemiddeld</option>
                                        <option value="Hoog">Hoog / actief</option>
                                    </select>
                                </div>

                                <div className="form-group">
                                    <label htmlFor="doelTermijnMaanden">Termijn</label>
                                    <select
                                        id="doelTermijnMaanden"
                                        name="doelTermijnMaanden"
                                        value={profielForm.doelTermijnMaanden}
                                        onChange={handleProfielChange}
                                    >
                                        <option value="3">3 maanden</option>
                                        <option value="6">6 maanden</option>
                                        <option value="12">12 maanden</option>
                                    </select>
                                </div>
                            </div>

                            <div style={{ display: 'flex', gap: '1rem', marginTop: '1rem' }}>
                                <button
                                    className="button"
                                    type="submit"
                                    disabled={opslaanBezig}
                                >
                                    {opslaanBezig ? 'Opslaan...' : 'Advies maken'}
                                </button>

                                {dashboard?.heeftProfiel && (
                                    <button
                                        className="button"
                                        type="button"
                                        disabled={opslaanBezig}
                                        onClick={() => setToonProfielForm(false)}
                                    >
                                        Annuleren
                                    </button>
                                )}
                            </div>
                        </form>
                    </section>
                )}

                {dashboard && dashboard.heeftProfiel === true && !profielFormZichtbaar && (
                    <>
                        <section className="dashboard-section">
                            <h2>Jouw persoonlijke profiel</h2>

                            <div className="dashboard-grid">
                                <article className="dashboard-panel">
                                    <h3>{dashboard.doel?.naam}</h3>
                                    <p>{dashboard.doel?.beschrijving}</p>
                                    <p>
                                        Termijn: {dashboard.profiel.doelTermijnMaanden}{' '}
                                        maanden
                                    </p>
                                </article>

                                <article className="dashboard-panel">
                                    <h3>BMI-indicatie</h3>
                                    <p>
                                        <strong>{dashboard.profiel.bmi}</strong> —{' '}
                                        {dashboard.profiel.bmiCategorie}
                                    </p>
                                    <p>
                                        Deze indicatie wordt alleen gebruikt om het advies
                                        beter af te stemmen.
                                    </p>
                                </article>

                                <article className="dashboard-panel">
                                    <h3>Basisgegevens</h3>
                                    <p>Leeftijd: {dashboard.profiel.leeftijd}</p>
                                    <p>Lengte: {dashboard.profiel.lengteCm} cm</p>
                                    <p>Gewicht: {dashboard.profiel.gewichtKg} kg</p>
                                    <p>
                                        Activiteitniveau:{' '}
                                        {dashboard.profiel.activiteitniveau}
                                    </p>
                                </article>
                            </div>
                                 <article className="dashboard-panel">
                                    <h3>Langetermijndoel</h3>
                                    <p>Startgewicht: {dashboard.profiel.startGewichtKg ?? '-'} kg</p>
                                    <p>Doelgewicht: {dashboard.profiel.doelGewichtKg ?? '-'} kg</p>
                                    <p>
                                        Startdatum:{' '}
                                        {dashboard.profiel.startDatum
                                            ? new Date(dashboard.profiel.startDatum).toLocaleDateString('nl-NL')
                                            : '-'}
                                    </p>
                                    <p>
                                        Einddatum:{' '}
                                        {dashboard.profiel.eindDatum
                                            ? new Date(dashboard.profiel.eindDatum).toLocaleDateString('nl-NL')
                                            : '-'}
                                    </p>
                                </article>
                        </section>

                        <section className="dashboard-section">
                            <h2>Voedingsadvies</h2>

                            {dashboard.advies ? (
                                <article className="dashboard-panel">
                                    <h3>{dashboard.advies.titel}</h3>
                                    <p>{dashboard.advies.beschrijving}</p>

                                    <h4>Calorieadvies</h4>
                                    <p>{dashboard.advies.calorieAdvies}</p>

                                    <h4>Eiwitadvies</h4>
                                    <p>{dashboard.advies.eiwitAdvies}</p>

                                    <h4>Tips</h4>
                                    <p>{dashboard.advies.algemeneTips}</p>
                                </article>
                            ) : (
                                <article className="dashboard-panel">
                                    <p>
                                        Er is nog geen passend voedingsadvies gevonden.
                                        Controleer of er templates in de database staan.
                                    </p>
                                </article>
                            )}
                        </section>

                        <section className="dashboard-section">
                            <h2>Voortgang deze week</h2>

                            <article className="dashboard-panel">
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
                            </article>
                            <article className="dashboard-panel" style={{ marginTop: '1rem' }}>
                                <h3>Weekgegevens bijwerken</h3>

                                <form onSubmit={slaWeekVoortgangOp}>
                                    <div className="form-group">
                                        <label htmlFor="weekGewichtKg">Gewicht deze week</label>
                                        <input
                                            id="weekGewichtKg"
                                            name="gewichtKg"
                                            type="number"
                                            min="30"
                                            max="250"
                                            step="0.1"
                                            value={weekForm.gewichtKg}
                                            onChange={handleWeekChange}
                                        />
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="weekNotitie">Notitie</label>
                                        <input
                                            id="weekNotitie"
                                            name="notitie"
                                            type="text"
                                            value={weekForm.notitie}
                                            onChange={handleWeekChange}
                                            placeholder="Bijv. drukke week, cardio gemist..."
                                        />
                                    </div>

                                    <button className="button" type="submit" disabled={opslaanBezig}>
                                        Week opslaan
                                    </button>
                                </form>
                            </article>
                        </section>
                        {dashboard.langeTermijnEvaluatie && (
                        <section className="dashboard-section">
                            <h2>Langetermijn evaluatie</h2>

                            <article className="dashboard-panel">
                                <h3>{dashboard.langeTermijnEvaluatie.status}</h3>

                                <p>{dashboard.langeTermijnEvaluatie.analyseTekst}</p>

                                <p>
                                    Trainingsconsistentie:{' '}
                                    {dashboard.langeTermijnEvaluatie.trainingsConsistentiePercentage}%
                                </p>

                                <p>
                                    Weken doel behaald:{' '}
                                    {dashboard.langeTermijnEvaluatie.wekenDoelBehaald}/
                                    {dashboard.langeTermijnEvaluatie.aantalWeken}
                                </p>

                                {!dashboard.langeTermijnEvaluatie.isEindDatumBereikt && (
                                    <p>
                                        Nog {dashboard.langeTermijnEvaluatie.dagenTotEinddatum} dagen tot
                                        de einddatum.
                                    </p>
                                )}
                            </article>
                        </section>
                    )}
                    

                        <section className="dashboard-section">
                            <h2>Trainingsschema</h2>

                            <div className="dashboard-grid">
                                {dashboard.trainingsschema.map((training) => (
                                    <article
                                        key={`${training.dag}-${training.titel}`}
                                        className="dashboard-panel"
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
                        <section className="dashboard-section">
                            <h2>Maandoverzicht</h2>

                            {dashboard.maandHistorie.length === 0 ? (
                                <article className="dashboard-panel">
                                    <p>Er is nog geen maandoverzicht beschikbaar.</p>
                                </article>
                            ) : (
                                <div className="dashboard-grid">
                                    {dashboard.maandHistorie.map((maand) => (
                                        <article key={maand.maand} className="dashboard-panel">
                                            <h3>{maand.maand}</h3>
                                            <p>
                                                {maand.afgerondeTrainingen}/{maand.weekDoelTotaal}{' '}
                                                trainingen afgerond
                                            </p>
                                            <p>{maand.statusTekst}</p>
                                            <p>Progressie: {maand.percentage}%</p>
                                            <p>
                                                Gemiddeld gewicht:{' '}
                                                {maand.gemiddeldGewichtKg
                                                    ? `${maand.gemiddeldGewichtKg} kg`
                                                    : '-'}
                                            </p>
                                        </article>
                                    ))}
                                </div>
                            )}
                        </section>
                        <section className="dashboard-section">
                            <h2>Weekhistorie</h2>

                            {dashboard.weekHistorie.length === 0 ? (
                                <article className="dashboard-panel">
                                    <p>
                                        Er is nog geen weekhistorie. Rond trainingen af om
                                        je voortgang per week te zien.
                                    </p>
                                </article>
                            ) : (
                                <div className="dashboard-grid">
                                    {dashboard.weekHistorie.map((week) => (
                                        <article
                                            key={week.weekStartDatum}
                                            className="dashboard-panel"
                                        >
                                            <h3>
                                                Week van{' '}
                                                {new Date(
                                                    week.weekStartDatum
                                                ).toLocaleDateString('nl-NL')}
                                            </h3>
                                            <p>
                                                {week.afgerondeTrainingen}/
                                                {week.weekDoel} trainingen afgerond
                                            </p>
                                            <p>{week.statusTekst}</p>
                                            <strong>{week.percentage}%</strong>
                                        </article>
                                    ))}
                                </div>
                            )}
                        </section>

                        

                        <section className="dashboard-section">
                            <button
                                className="button"
                                type="button"
                                onClick={() => setToonProfielForm(true)}
                            >
                                Profiel of doel wijzigen
                            </button>
                        </section>
                    </>
                )}
            </section>
        </main>
    );
}

export default CoachingDashboardPage;