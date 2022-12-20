import { Link } from "react-router-dom"

const RestaurantUser = () => {
    return (
        <section>
            <h1>Restaurant Users Page</h1>
            <br />
            <p>You must have been assigned an Restaurant User role.</p>
            <div className="flexGrow">
                <Link to="/">Home</Link>
            </div>
        </section>
    )
}

export default RestaurantUser