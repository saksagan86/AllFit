import React, { useState, useEffect } from 'react';
import HuidigeSportschool from '../Components/HuidigeSportschool';
import ExtraBegeleidingDetail from '../Components/ExtraBegleidingDetail';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck } from '@fortawesome/free-solid-svg-icons';

function ExtraoptiesPagina() {
    const [gekozenLocatieId, setGekozenLocatieId] = useState('');
    const [sportscholen, setSportscholen] = useState([]);
    const [aanbod, setAanbod] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const fetchSportscholen = async () => {
            try {
                const response = await fetch('/api/sportschool/navbar');
                if (!response.ok) throw new Error('Kan sportscholen niet ophalen');
                const data = await response.json();
                setSportscholen(data);
            } catch (error) {
                console.error(error);
            }
        };
        fetchSportscholen();
    }, []);

    useEffect(() => {
        if (sportscholen.length === 0) return;
        setGekozenLocatieId((huidige) => {
            if (huidige) return huidige;
            return sportscholen[0].id;
        });
    }, [sportscholen]);

    useEffect(() => {
        if (!gekozenLocatieId) return;
        const fetchAanbod = async () => {
            setLoading(true);
            try {
                const response = await fetch(
                    `/api/sportscholen/${gekozenLocatieId}/aanbod/extrabegeleiding`
                );
                if (!response.ok) throw new Error('Kan aanbod niet ophalen');
                const data = await response.json();
                setAanbod(data);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        };
        fetchAanbod();
    }, [gekozenLocatieId]);

    const fitness = aanbod.filter(x => x.sportType === 'fitness');
    const groepslessen = aanbod.filter(x => x.sportType === 'groepsles');
    const kickboksen = aanbod.filter(x => x.sportType === 'kickboks');

    return (
        <div>
            <HuidigeSportschool
                locaties={sportscholen}
                geselecteerdeLocatie={gekozenLocatieId}
                alsLocatieVerandert={setGekozenLocatieId}
            />

            <div className="introductie">
                <div className="detail-introductie">
                    <h2 className="titelaanbodoverzicht" style={{ textAlign: "start" }}>Ontdek onze extra optie's</h2>
                    <p>Wil je net dat beetje extra ondersteuning? Onze trainers staan voor je klaar.</p>
                    <ul style={{ listStyleType: "none", paddingLeft: "0px", margin: "0" }}>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Persoonlijke aandacht
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Kickboksen en groepslessen
                        </li>
                        <li style={{ marginBottom: "15px" }}>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Afgestemd op jouw niveau
                        </li>
                        <li>
                            <FontAwesomeIcon icon={faCheck} className="voordeel-icon" />
                            Flexibel inpasbaar
                        </li>
                    </ul>
                </div>
                <div className="foto-container">
                    <img className="foto-introductie" src="/images/kickbokstrainers.jpg" alt="Extra Begeleiding" />
                    <p>AllFit trainers staan voor je klaar</p>
                </div>
            </div>

            <div className="max-page-content">
                {loading && <p>Laden...</p>}
                {!loading && (
                    <>
                        <div className="detail-kaarten">
                            {aanbod.map((item) => (
                                <ExtraBegeleidingDetail key={item.id} item={item} />
                            ))}
                        </div>
                        {aanbod.length === 0 && (
                            <p>Geen extra begeleiding beschikbaar voor deze locatie.</p>
                        )}
                    </>
                )}
            </div>
        </div>
    );
}

export default ExtraoptiesPagina;
