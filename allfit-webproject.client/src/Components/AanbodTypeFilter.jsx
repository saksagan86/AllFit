function AanbodTypeFilter({ waarde, alsWaardeVerandert }) {
    return (
        <div>
            <label htmlFor="aanbodtype-select" style={{ marginRight: '10px', fontWeight: 'bold', color: '#333' }}>
                Aanbod:
            </label>
            <select
                id="aanbodtype-select"
                value={waarde}
                onChange={(e) => alsWaardeVerandert(e.target.value)}
                className="aanbod-locatie-dropdown"
            >
                <option value="alle">Alle aanbod</option>
                <option value="Kickboks">Kickboksen</option>
                <option value="Fitness">Fitness</option>
                <option value="Groepsles">Groepsles</option>
            </select>
        </div>
    );
}

export default AanbodTypeFilter;