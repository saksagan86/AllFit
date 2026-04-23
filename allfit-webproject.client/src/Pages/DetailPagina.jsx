import React, { useState, useEffect } from 'react';
import { useParams, useLocation, useNavigate } from 'react-router-dom';
import FitnessDetail from '../Components/FitnessDetail';
import KickboksDetail from '../Components/KickboksDetail';
import GroepslesDetail from '../Components/GroepslesDetail';
import HuidigeSportschool from '../Components/HuidigeSportschool';
import locations from '../data/locations';

function DetailPagina() {
    const { sportNaam } = useParams();
    const location = useLocation();
    const navigate = useNavigate();

    // initialiseer gekozen locatie uit router state indien aanwezig,
    // anders fallback naar eerste locatie in data (of lege string)
    const initialLocatie = location.state?.selectedLocation || (locations && locations[0]?.city) || '';
    const [gekozenLocatie, setGekozenLocatie] = useState(initialLocatie);

    // sync als router-state buiten deze component verandert (navigatie met andere state)
    useEffect(() => {
        if (location.state?.selectedLocation && location.state.selectedLocation !== gekozenLocatie) {
            setGekozenLocatie(location.state.selectedLocation);
        }
    }, [location.state, gekozenLocatie]);

    const handleLocatieChange = (nieuweLocatie) => {
        setGekozenLocatie(nieuweLocatie);

        // update huidige history entry met nieuwe state (vervang de state, geen extra history-entry)
        const currentPath = location.pathname + (location.search || '');
        navigate(currentPath, { replace: true, state: { selectedLocation: nieuweLocatie } });
    };

    const sportComponenten = {
        'fitness': <FitnessDetail />,
        'kickboksen': <KickboksDetail />,
        'groepslessen': <GroepslesDetail />
    };

    const GeselecteerdComponent = sportComponenten[sportNaam?.toLowerCase()];

    return (
        <div>
            <HuidigeSportschool
                locaties={locations}
                geselecteerdeLocatie={gekozenLocatie}
                alsLocatieVerandert={handleLocatieChange}
            />

            <div style={{ maxWidth: 1200, margin: '1rem auto', padding: '0 1rem' }}>
                {GeselecteerdComponent}
            </div>
        </div>
    );
}

export default DetailPagina;