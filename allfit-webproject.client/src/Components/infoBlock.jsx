import { Link } from 'react-router-dom';

function buildCustomButton(buttoninhoud, link, custombutton) {
    if (custombutton) {
        return (custombutton);
    }
    return (
        <Link className='button' to={link} style={{ textAlign: "center", display:"inline-block"}}>
            {buttoninhoud}
        </Link>
    );
}
function InfoBlock({ title, description, foto_url, buttoninhoud, link, custombutton }) {
    return (
        <div className='block'>
            <img src={foto_url} alt="allfit sporters" className='block-foto'></img>
            <div className='block-info'>
                <h2>
                    {title}
                </h2>
                <p>
                    {description}
                </p>
                {buildCustomButton(buttoninhoud, link, custombutton)}
            </div>
        </div>
    );
}

export default InfoBlock;