import { Link } from 'react-router-dom';
function InfoBlock({ title, description, foto_url, buttoninhoud, link }) {
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
                <Link className='button' to={link} style={{ textAlign: "center", display:"inline-block"}}>
                    {buttoninhoud}
                </Link>
            </div>
        </div>
    );
}

export default InfoBlock;