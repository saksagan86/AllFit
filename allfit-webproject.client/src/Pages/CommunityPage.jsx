import AanbodTypeFilter from "../Components/AanbodTypeFilter"
import { useState, useEffect } from 'react';
import VerhaalKaart from "../Components/VerhaalKaart";
import { useAuth } from '../context/AuthContext.jsx';
import { useNavigate } from 'react-router-dom';

function CommunityPage() {
    const [aanbodType, setAanbodType] = useState('alle');
    const [verhalen, setVerhalen] = useState([]);
    const [loading, setLoading] = useState(false);
    const { isAuthenticated } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchVerhalen = async () => {
            setLoading(true);
            try {
                const url = aanbodType === 'alle'
                    ? '/api/community'
                    : `/api/community?aanbodType=${aanbodType}`;
                const response = await fetch(url);
                if (!response.ok) throw new Error('Kan verhalen niet ophalen');
                const data = await response.json();
                setVerhalen(data);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        };
        fetchVerhalen();
    }, [aanbodType]);

    return (
        <div>
            <div style={{ display: 'flex', justifyContent: 'space-between', padding: '0 5%', marginTop: '20px', alignItems: 'center' }}>
                {isAuthenticated && (
                    <button className="button" style={{ border: 'none'}} onClick={() => navigate('/community/nieuw')}>
                        Verhaal toevoegen 
                    </button>
                )}
                <AanbodTypeFilter
                    waarde={aanbodType}
                    alsWaardeVerandert={setAanbodType}
                />
            </div>

            <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', margin:'20px'}}>
                {loading && <p>Laden...</p>}
                {!loading && verhalen.map((verhaal) => (
                    <VerhaalKaart key={verhaal.id} verhaal={verhaal} />
                ))}
                {!loading && verhalen.length === 0 && (
                    <p>Geen verhalen gevonden.</p>
                )}
            </div>

        </div>
    );
}

export default CommunityPage