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
                <option value="kickboksen">Kickboksen</option>
                <option value="fitness">Fitness</option>
                <option value="groepsles">Groepsles</option>
            </select>
        </div>
    );
}

export default AanbodTypeFilter;