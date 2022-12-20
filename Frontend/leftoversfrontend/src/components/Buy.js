import { useNavigate, Link } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../context/AuthProvider";

const Buy = () => {
    const { setAuth } = useContext(AuthContext);
    const navigate = useNavigate();

    const logout = async () => {
        // if used in more components, this should be in context 
        // axios to /logout endpoint 
        setAuth({});
        navigate('/login');
    }

    return (
        <section>
            <h1>Kolkas pirkimų atlikti negalite</h1>
            
            <Link to="/chain">Restoranų tinklai</Link>
            <br />

            <div className="flexGrow">
                <button onClick={logout}>Atsijungti</button>
            </div>
        </section>
    )
}

export default Buy