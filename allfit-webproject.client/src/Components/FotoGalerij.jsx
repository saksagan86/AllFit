import { useState } from 'react';

function FotoGalerij({ fotos }) {
    const [geselecteerd, setGeselecteerd] = useState(null);

    if (!fotos || fotos.length === 0) return null;

    const vorigefoto = () => setGeselecteerd(geselecteerd > 0 ? geselecteerd - 1 : fotos.length - 1);
    const volgendefoto = () => setGeselecteerd(geselecteerd < fotos.length - 1 ? geselecteerd + 1 : 0);

    return (
        <div className="fotogalerij">
            <h3 className="fotogalerij-titel">Fotogalerij</h3>
            <div className="fotogalerij-grid">
                {fotos.map((foto, index) => (
                    <img key={index} src={foto} alt={`foto ${index + 1}`} className="fotogalerij-img" onClick={() => setGeselecteerd(index)} />
                ))}
            </div>

            {geselecteerd !== null && (
                <div className="popup-overlay">
                    <div className="popup-inhoud">
                        <img src={fotos[geselecteerd]} alt="vergroot" className="popup-foto" />
                        <div className="popup-navigatie">
                            <button className="popup-pijl" onClick={vorigefoto}>&#8592;</button>
                            <span>{geselecteerd + 1} / {fotos.length}</span>
                            <button className="popup-pijl" onClick={volgendefoto}>&#8594;</button>
                        </div>
                    </div>
                    <button className="popup-sluiten" onClick={() => setGeselecteerd(null)}>&#10005;</button>
                </div>
            )}
        </div>
    );
}

export default FotoGalerij; 