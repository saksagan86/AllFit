function InfoBlock({ title, description, foto_url }) {
    return (
        <div class='block'>
            <img src={foto_url} class='block-foto'></img>
            <div class='block-info'>
                <h2>
                    {title}
                </h2>
                <p>
                    {description}
                </p>
                <button>
                    Call To Action
                </button>
            </div>
        </div>
    );
}

export default InfoBlock;