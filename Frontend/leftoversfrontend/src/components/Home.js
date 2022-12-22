import { useNavigate, Link } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../context/AuthProvider";
import logo from "../components/images/logo2-removebg.png";
import '../App.css'

const Home = () => {
    const { setAuth } = useContext(AuthContext);
    const navigate = useNavigate();

    const logout = async () => {
        // if used in more components, this should be in context 
        // axios to /logout endpoint 
        setAuth({});
        navigate('/linkpage');
    }

    return (       
            
                
            <img src={logo}  alt="Logo" class="responsive"/>
            

    )
}

export default Home