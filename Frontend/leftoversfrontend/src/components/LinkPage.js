import { Link } from "react-router-dom"

const LinkPage = () => {
    return (
        <section>
            <h1>Links</h1>
            <br />
            <h2>Public</h2>
            
            <Link to="/register">Register</Link>
            <Link to="/login">Login</Link>
            <br />
            <h2>Private</h2>
            <Link to="/">Home</Link>
            <Link to="/editor">Restaurant User Page</Link>
            <Link to="/admin">Admin Page</Link>
        </section>
    )
}

export default LinkPage