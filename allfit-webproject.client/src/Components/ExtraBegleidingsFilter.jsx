function ExtraBegeleidingFilter({ waarde, alsWaardeVerandert }) {
    return (
        <div>
            <label htmlFor="begeleiding-select" style={{ marginRight: '10px', fontWeight: 'bold', color: '#333' }}>
                Begeleiding:
            </label>
            <select
                id="begeleiding-select"
                value={waarde}
                onChange={(e) => alsWaardeVerandert(e.target.value)}
                className="aanbod-locatie-dropdown"
            >
                <option value="alle">Alle lessen</option>
                <option value="met">Met extra begeleiding</option>
            </select>
        </div>
    );
}

export default ExtraBegeleidingFilter;