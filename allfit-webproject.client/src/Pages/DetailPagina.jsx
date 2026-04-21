import React, {useState} from 'react';
import { useParams } from 'react-router-dom';
import FitnessDetail from '../Components/FitnessDetail';
import KickboksDetail from '../Components/KickboksDetail';
import GroepslesDetail from '../Components/GroepslesDetail';
import HuidigeSportschool from '../Components/HuidigeSportschool';
import locations from '../data/locations';


function DetailPagina() {

    const { sportNaam } = useParams();

    const [gekozenLocatie, setGekozenLocatie] = useState('');

    const sportComponenten = {
        'fitness': <FitnessDetail />,
        'kickboksen': <KickboksDetail />,
        'groepslessen': <GroepslesDetail />
    };

    const GeselecteerdComponent = sportComponenten[sportNaam?.toLowerCase()];


    return (
        <div>
            <div>

                <HuidigeSportschool locaties={locations} geselecteerdeLocatie={gekozenLocatie} alsLocatieVerandert={setGekozenLocatie} />

                {GeselecteerdComponent}
            </div>

        </div>
    );
}

export default DetailPagina;