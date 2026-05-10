function InfoBlock({ title, description, foto_url }) {
    return (
        <div className='block'>
            <img src={foto_url} className='block-foto'></img>
            <div className='block-info'>
                <h2>
                    {title}
                </h2>
                <p>
                    {description}
                </p>
                <button className='button'>
                    Proefles aanvragen!
                </button>
            </div>
        </div>
    );
}

export default InfoBlock;