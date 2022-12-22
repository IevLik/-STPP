import { useRef, useState, useEffect, useContext } from 'react';
//import AuthContext from "../context/AuthProvider";
import UseAuth from "../hooks/UseAuth";
import { Link, useNavigate} from 'react-router-dom';
//import useAxiosPrivate from "../hooks/useAxiosPrivate";
import decode from 'jwt-decode';
import axios from '../api/axios';
import  "../components/navBar/shake.css";
const LOGIN_URL = '/login';

    

    

const Login = () => {
    const { setAuth } = UseAuth();
    const [shake, setShake] = useState(false);
    
    const animate = () => {
        
        // Button begins to shake
        setShake(true);
        
        // Buttons stops to shake after 2 seconds
        setTimeout(() => setShake(false), 2000);
        
    }
    //const axiosPrivate = useAxiosPrivate();//ciaaa
    const navigate = useNavigate();//ciaaa

    const userRef = useRef();
    const errRef = useRef();

    const [user, setUser] = useState('');
    const [pwd, setPwd] = useState('');
    const [errMsg, setErrMsg] = useState('');
    const [success, setSuccess] = useState(false);

    useEffect(() => {
        userRef.current.focus();
    }, [])

    useEffect(() => {
        setErrMsg('');
    }, [user, pwd])

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post(LOGIN_URL,
                JSON.stringify({ userName: user, password: pwd }),//ciaaa
                {
                    headers: { 'Content-Type': 'application/json' },
                    withCredentials: true
                }
            );
            console.log(JSON.stringify(response?.data));
            //console.log(JSON.stringify(response));
            const accessToken = response?.data?.accessToken;
            const decoded = decode(accessToken);
            if(decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == "RestaurantUser")
            {
                const roles = (["RestaurantUser", ""])
                setAuth({ user, pwd, roles, accessToken });
            }
            else{
                const roles = decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
                setAuth({ user, pwd, roles, accessToken });
            }
            //const roles = response?.data?.roles;
            //setAuth({ user, pwd, roles, accessToken }); //why
            setUser('');
            setPwd('');
            //setSuccess(true);//why
            navigate("/chain");
        } catch (err) {
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 400) {
                setErrMsg('Missing Username or Password');
            } else if (err.response?.status === 401) {
                setErrMsg('Unauthorized');
            } else {
                setErrMsg('Login Failed');
            }
            errRef.current.focus();
        }
    }

    return (
        <>
            {success ? (
                <section>
                    <h1>You are logged in!</h1>
                    <br />
                    <p>
                        <a href="/chain">Go to Home</a>
                    </p>
                </section>
            ) : (
                <section>
                    <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
                    <h1>Sign In</h1>
                    <form onSubmit={handleSubmit}>
                        <label htmlFor="username">Username:</label>
                        <input
                            type="text"
                            id="username"
                            ref={userRef}
                            autoComplete="off"
                            onChange={(e) => setUser(e.target.value)}
                            value={user}
                            required
                        />

                        <label htmlFor="password">Password:</label>
                        <input
                            type="password"
                            id="password"
                            onChange={(e) => setPwd(e.target.value)}
                            value={pwd}
                            required
                        />
                        <button onClick = {animate} className = {shake ? `shake` : null}>Sign In</button>
                    </form>
                    <p>
                        Need an Account?<br />
                        <span className="line">
                            {/*put router link here*/}
                            <Link to="/register">Sign Up</Link>
                        </span>
                    </p>
                </section>
            )}
        </>
    )
}

export default Login