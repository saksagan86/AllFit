import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCalendar, faClock } from '@fortawesome/free-solid-svg-icons';
import { useState } from 'react';

function LessenRooster({ lessen, geselecteerd, onSelectie }) {
    const toggleLes = (id, isVol) => {
        if (isVol) return;
        const nieuw = new Set(geselecteerd);
        if (nieuw.has(id)) {
            nieuw.delete(id);
        } else {
            nieuw.add(id);
        }
        onSelectie(nieuw);
    };

    return (
        <div className="lessen-rooster">
            {lessen.map((les) => {
                const isVol = les.vrijePlekken == 0;
                const isIngeschreven = les.isIngeschreven;
                return (
                    <div key={les.id} className={`les-kaart ${geselecteerd.has(les.id) ? 'geselecteerd' : ''} ${isVol || isIngeschreven ? 'vol' : ''}`} onClick={() => toggleLes(les.id, isVol || isIngeschreven)}>
                        <p className="les-datum">
                            <FontAwesomeIcon icon={faCalendar} style={{ marginRight: '3px', color: '#3DC2C7' }} />
                            {new Date(les.datum).toLocaleDateString('nl-NL', { weekday: 'short', day: 'numeric', month: 'short' })}
                        </p>
                        <p className="les-tijd">
                            <FontAwesomeIcon icon={faClock} style={{ marginRight: '3px', color: '#aaa' }} />
                            {les.tijd.substring(0, 5)}
                        </p>
                        {isIngeschreven
                            ? <span className="les-plekken" style={{ backgroundColor: "#e8f0fe", color: "#1a56db" }}>Al ingeschreven</span>
                            : isVol
                                ? <span className="les-plekken" style={{ backgroundColor: "#fce8e8", color: "#A32D2D" }}>Vol</span>
                                : <span className="les-plekken">{les.vrijePlekken} plekken vrij</span>
                        }
                    </div>
                );
            })}
        </div>
    );
}

export default LessenRooster;