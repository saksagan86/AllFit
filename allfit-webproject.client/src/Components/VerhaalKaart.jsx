import { Link } from 'react-router-dom';

function VerhaalKaart({ verhaal }) {
    const preview = verhaal.inhoud.length > 100
        ? verhaal.inhoud.substring(0, 100) + '...'
        : verhaal.inhoud;

    return (
        <div className="verhaal-kaart">
            <div className="verhaal-tekst">
                <h3 className="verhaal-titel">{verhaal.titel}</h3>
                <div className="verhaal-meta">
                    <p>{new Date(verhaal.geplaatstOp).toLocaleDateString('nl-NL')}</p>
                    <p>{verhaal.gebruikersNaam}</p>
                </div>
                <p className="verhaal-preview">{preview}</p>
                <Link className="button" to={`/community/${verhaal.id}`} style={{ display: 'inline-block' }}>Lees meer</Link>
            </div>
            <div className="verhaal-foto">
                {verhaal.image
                    ? <img src={verhaal.image} alt={verhaal.titel} className="verhaal-foto-img" />
                    : <span>Geen foto</span>
                }
            </div>
        </div>
    );
}

export default VerhaalKaart;