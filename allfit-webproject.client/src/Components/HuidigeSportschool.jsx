import React from 'react';

function HuidigeSportschool({ locaties, geselecteerdeLocatie, alsLocatieVerandert, children }) {
    return (
        <div style={{ display: 'flex', justifyContent: 'flex-end', alignItems: 'center', gap: '20px', padding: '0 5%', marginTop: '20px' }}>
            {children}
            <div>
                <label htmlFor="locatie-select" style={{ marginRight: '10px', fontWeight: 'bold', color: '#333' }}>
                    Locatie:
                </label>
                <select id="locatie-select" value={geselecteerdeLocatie || ''} onChange={(e) => alsLocatieVerandert(Number(e.target.value))} className="aanbod-locatie-dropdown">
                    {locaties.map((loc) => (
                        <option key={loc.id} value={loc.id}>
                            {loc.naam}
                        </option>
                    ))}
                </select>
            </div>
        </div>
    );
}

export default HuidigeSportschool;