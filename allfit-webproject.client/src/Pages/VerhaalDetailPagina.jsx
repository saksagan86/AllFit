import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import FotoGalerij from '../Components/FotoGalerij';
function VerhaalDetailPagina() {
    const { verhaalId } = useParams();
    const [verhaal, setVerhaal] = useState(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        if (!verhaalId) return;
        const fetchVerhaal = async () => {
            setLoading(true);
            try {
                const response = await fetch(`/api/community/${verhaalId}`);
                if (!response.ok) throw new Error('Kan verhaal niet ophalen');
                const data = await response.json();
                setVerhaal(data);
            } catch (error) {
                console.error(error);
            } finally {
                setLoading(false);
            }
        };
        fetchVerhaal();
    }, [verhaalId]);

    if (loading) return <p>Laden...</p>;
    if (!verhaal) return <p>Verhaal niet gevonden.</p>;

    return (
        <div style={{ maxWidth: 900, margin: '40px auto', padding: '0 5%' }}>
            <h1 style={{ fontSize: '28px', fontWeight: '700', color: '#000', marginBottom: '4px' }}>{verhaal.titel}</h1>
            <div style={{ fontSize: '12px', color: '#888', marginBottom: '20px', lineHeight: '1.4' }}>
                <p style={{ margin: 0 }}>{new Date(verhaal.geplaatstOp).toLocaleDateString('nl-NL')}</p>
                <p style={{ margin: 0 }}>{verhaal.gebruikersNaam}</p>
            </div>
            <div style={{fontSize: '15px', lineHeight: '1.8', color: '#333' }}>
                {verhaal.inhoud}
            </div>
            <FotoGalerij fotos={verhaal.fotos} />

        </div>
    );
}

export default VerhaalDetailPagina;