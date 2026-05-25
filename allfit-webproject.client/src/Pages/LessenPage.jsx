import React, { useState, useEffect } from 'react';
import { useAuth } from '../context/AuthContext.jsx';
import { useParams, useLocation, useNavigate } from 'react-router-dom';
import LessenRooster from '../Components/LessenRooster.jsx';

function LessenPage() {
    const { aanbodId } = useParams();
    const location = useLocation();
    const naam = location.state?.naam || 'Les';
    const extraBegeleiding = location.state?.extraBegeleiding || false;
    const { token } = useAuth();
    const [lessen, setLessen] = useState([]);
    const [loading, setLoading] = useState(false);
    const [geselecteerdeLessen, setGeselecteerdeLessen] = useState(new Set());
    const [error, setError] = useState('');
    const [succes, setSucces] = useState(false);
    const navigate = useNavigate();

    const handleInschrijven = async () => {
        try {
            const response = await fetch('/api/inschrijving', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                },
                body: JSON.stringify({
                    lesIds: [...geselecteerdeLessen],
                    extraBegeleiding: extraBegeleiding
                })
            });
            if (!response.ok) {
                const data = await response.json();
                setError(data.message || 'Inschrijven mislukt');
                return;
            }
            setSucces(true);
        } catch (error) {
            setError('Er is iets misgegaan, probeer opnieuw.');
        }
    };

    useEffect(() => {
        if (!aanbodId) return;
        const fetchLessen = async () => {
            setLoading(true);
            try {
                const response = await fetch(`/api/les/${aanbodId}/lessen`, {
                    headers: { Authorization: `Bearer ${token}` }
                });
                if (!response.ok) throw new Error('Kan lessen niet ophalen');
                const data = await response.json();
                setLessen(data);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        };
        fetchLessen();
    }, [aanbodId]);

    return (
        <div>
            <div className="lessen-page">
                <div className="lessen-header">
                    <h1 className="lessen-titel">{naam}</h1>
                    <p className="lessen-subtitel">Kies een of meerdere lessen</p>
                </div>

                {loading && <p>Laden...</p>}
                {!loading && <LessenRooster lessen={lessen} geselecteerd={geselecteerdeLessen} onSelectie={setGeselecteerdeLessen} />}

                <div className="">
                    <p className="">{geselecteerdeLessen.size} les{geselecteerdeLessen.size !== 1 ? 'sen' : ''} geselecteerd</p>
                    <button
                        className="button"
                        onClick={handleInschrijven}
                        disabled={geselecteerdeLessen.size === 0}
                    >
                        Inschrijven
                    </button>
                </div>

                {error && <p style={{ color: 'red' }}>{error}</p>}

                {succes && (
                    <div className="popup-overlay">
                        <div className="popup">
                            <p>Inschrijving gelukt!</p>
                            <button className="button" onClick={() => navigate('/account/overzicht')}>
                                Sluiten
                            </button>
                        </div>
                    </div>
                )}

            </div>
        </div>
    );
}

export default LessenPage;