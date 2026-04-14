import React from 'react';
import { useParams } from 'react-router-dom';
import FitnessDetail from '../Components/FitnessDetail';
import KickboksDetail from '../Components/KickboksDetail';
import GroepslesDetail from '../Components/GroepslesDetail';


function DetailPagina() {

    const { sportNaam } = useParams();

    const sportComponenten = {
        'fitness': <FitnessDetail />,
        'kickboksen': <KickboksDetail />,
        'groepslessen': <GroepslesDetail />
    };

    const GeselecteerdComponent = sportComponenten[sportNaam?.toLowerCase()];


    return (
        <div>
            <div>
                {GeselecteerdComponent}
            </div>

        </div>
    );
}

export default DetailPagina;