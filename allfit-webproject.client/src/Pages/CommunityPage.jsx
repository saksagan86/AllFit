import AanbodTypeFilter from "../Components/AanbodTypeFilter"
import { useState } from 'react';

function CommunityPage() {
    const [aanbodType, setAanbodType] = useState('alle');

    return (
        <div style={{ display: 'flex', justifyContent: 'flex-end', padding: '0 5%', marginTop: '20px' }} >
            <AanbodTypeFilter
                waarde={aanbodType}
                alsWaardeVerandert={setAanbodType}
            />
        </div>
    );
}

export default CommunityPage