import React, { useState, useEffect } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import FitnessDetail from '../Components/FitnessDetail';
import KickboksDetail from '../Components/KickboksDetail';
import GroepslesDetail from '../Components/GroepslesDetail';
import HuidigeSportschool from '../Components/HuidigeSportschool';
import ExtraBegeleidingFilter from '../Components/ExtraBegleidingsFilter';

function DetailPagina() {
    const routerLocation = useLocation();
    const [gekozenLocatieId, setGekozenLocatieId] = useState('');
    const [sportscholen, setSportscholen] = useState([]);
    const [aanbodData, setAanbodData] = useState([]);
    const { sportNaam } = useParams();
    const [extraBegeleiding, setExtraBegeleiding] = useState('alle');

    const gefilterdAanbod = aanbodData.filter((item) => {
        if (extraBegeleiding === 'met') return item.extraBegeleiding === true;
        return true;
    });

    const sportComponenten = {
        'fitness': <FitnessDetail aanbod={gefilterdAanbod} />,
        'kickboksen': <KickboksDetail aanbod={gefilterdAanbod} extraBegeleiding={extraBegeleiding} />,
        'groepslessen': <GroepslesDetail aanbod={gefilterdAanbod} extraBegeleiding={extraBegeleiding} />
    };

    const GeselecteerdComponent = sportComponenten[sportNaam?.toLowerCase()];

    useEffect(() => {
        if (routerLocation.state?.selectedLocationId) {
            setGekozenLocatieId(routerLocation.state.selectedLocationId);
        }
    }, [routerLocation.state]);

    useEffect(() => {
        const fetchSportscholen = async () => {
            try {
                const response = await fetch("/api/sportschool/navbar");
                if (!response.ok) throw new Error("Kan sportscholen niet ophalen");
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
        if (!gekozenLocatieId || !sportNaam) return;
        const fetchAanbod = async () => {
            try {
                const response = await fetch(
                    `/api/sportscholen/${gekozenLocatieId}/aanbod/${sportNaam}`
                );
                if (!response.ok) throw new Error("Kan aanbod niet ophalen");
                const data = await response.json();
                setAanbodData(data);
            } catch (error) {
                console.error(error);
            }
        };
        fetchAanbod();
    }, [gekozenLocatieId, sportNaam]);

    return (
        <div>
            <HuidigeSportschool
                locaties={sportscholen}
                geselecteerdeLocatie={gekozenLocatieId}
                alsLocatieVerandert={setGekozenLocatieId}
            >
                {(sportNaam === 'kickboksen' || sportNaam === 'groepslessen') && (
                    <ExtraBegeleidingFilter
                        waarde={extraBegeleiding}
                        alsWaardeVerandert={setExtraBegeleiding}
                    />
                )}
            </HuidigeSportschool>
            <div style={{ maxWidth: 1200, margin: '1rem auto', padding: '0 1rem' }}>
                {GeselecteerdComponent}
            </div>
        </div>
    );
}

export default DetailPagina;