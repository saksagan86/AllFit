import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext.jsx';

function VerhaalNieuwePagina() {
    const { token } = useAuth();
    const navigate = useNavigate();
    const [titel, setTitel] = useState('');
    const [inhoud, setInhoud] = useState('');
    const [fotos, setFotos] = useState([]);
    const [aanbodOpties, setAanbodOpties] = useState([]);
    const [aanbodId, setAanbodId] = useState('');

    const handleFotoSelectie = (e) => {
        setFotos([...e.target.files]);
    };

    const handleOpslaan = async () => {
        const formData = new FormData();
        formData.append('titel', titel);
        formData.append('inhoud', inhoud);
        formData.append('aanbodId', aanbodId);
        const aanbodNaam = aanbodOpties.find(a => a.id == aanbodId)?.naam;
        const image = aanbodNaam ? `/images/${aanbodNaam.toLowerCase().replace(/\s+/g, '')}.jpg` : '';
        formData.append('image', image);
        fotos.forEach(foto => formData.append('fotos', foto));

        try {
            const response = await fetch('/api/community', {
                method: 'POST',
                headers: { Authorization: `Bearer ${token}` },
                body: formData
            });
            if (!response.ok) {
                const data = await response.json();
                console.log(data);
                throw new Error('Opslaan mislukt');
            }
            navigate('/community');
        } catch (error) {
            console.error(error);
        }
    };


    useEffect(() => {
        const fetchAanbod = async () => {
            const response = await fetch('/api/aanbod');
            const data = await response.json();
            setAanbodOpties(data);
        };
        fetchAanbod();
    }, []);

    return (
        <div style={{ maxWidth: 900, margin: '40px auto', padding: '0 5%' }}>
            <h1 style={{ marginBottom: '20px' }}>Verhaal toevoegen</h1>

            <input
                type="text"
                placeholder="Titel"
                value={titel}
                onChange={(e) => setTitel(e.target.value)}
                style={{ width: '100%', padding: '10px', fontSize: '16px', borderRadius: 'var(--border-radius-md)', border: '1px solid #ccc', marginBottom: '16px' }}
            />

            <textarea
                placeholder="Schrijf hier je verhaal..."
                value={inhoud}
                onChange={(e) => setInhoud(e.target.value)}
                style={{ width: '100%', height: '300px', padding: '10px', fontSize: '15px', borderRadius: 'var(--border-radius-md)', border: '1px solid #ccc', resize: 'vertical', lineHeight: '1.6' }}
            />


            <div style={{ marginTop: '16px', marginBottom: '16px' }}>
                <label style={{ fontWeight: 'bold', display: 'block', marginBottom: '8px' }}>
                    Selecteer een aanbodtype
                </label>
                <select value={aanbodId} onChange={(e) => setAanbodId(e.target.value)} style={{ width: '100%', padding: '10px', fontSize: '16px', borderRadius: 'var(--border-radius-md)', border: '1px solid #ccc' }}>
                    <option value="">Kies een aanbodtype</option>
                    {aanbodOpties.map((aanbod) => (
                        <option key={aanbod.id} value={aanbod.id}>{aanbod.naam}</option>
                    ))}
                </select>
            </div>


            <div style={{ marginTop: '16px' }}>
                <label style={{ fontWeight: 'bold', display: 'block', marginBottom: '8px' }}>
                    Foto's toevoegen
                </label>
                <input
                    type="file"
                    accept="image/*"
                    multiple
                    onChange={handleFotoSelectie}
                />
            </div>


            <div style={{ display: 'flex', gap: '12px', marginTop: '16px' }}>
                <button className="button" onClick={() => navigate('/community')}>Annuleren</button>
                <button className="button" onClick={ handleOpslaan }>Posten</button>
            </div>
        </div>
    );
}

export default VerhaalNieuwePagina;