import React, { useState, useEffect } from 'react';
import { Form, Button } from 'semantic-ui-react';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';

export default function EditRestaurant() {
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const [mealName, setName] = useState('');
    const [mealPrice, setMealPrice] = useState('');
    const [chainID, setChainID] = useState(null);
    const [restaurantID, setRestaurantID] = useState(null);
    useEffect(() => {
        setChainID(localStorage.getItem('ID'))
        }, [])
    const sendDataToAPI = () => {
        axiosPrivate.put('/chain/' + `${localStorage.getItem('ID')}` +'/restaurant/'+ `${localStorage.getItem('restaurantID')}` +'/meal', {
            mealName,
            mealPrice
        }).then(() => {
            navigate("/restaurant");//gal /detailed
        })
    }

    useEffect(() => {
        setName(localStorage.getItem('restaurantName'));
        setRestaurantID(localStorage.getItem('restaurantID'))
    }, [])

    return (
        <section>
            <h1>Redaguoti patiekalą</h1>
            <br></br>
            <Form>
                <Form.Field>
                    <label>Patiekalo pavadinimas</label>
                    <br></br>
                    <input name="fname"
                        type="text"
                        autoComplete="off"
                        required
                        value={mealName}
                        onChange={(e) => setName(e.target.value)}
                        placeholder='Restaurant Name' />
                </Form.Field>
                <Form.Field>
                    <label>Kaina</label>
                    <br></br>
                    <input
                        name="lname"
                        type="text"
                        autoComplete="off"
                        required
                        value={mealPrice}
                        placeholder='Description'
                        onChange={(e) => setMealPrice(e.target.value)}
                    />
                </Form.Field>
                
                
                <Button type='submit' onClick={sendDataToAPI}>Pakeisti</Button>
                <Link to='/meal'>
                    <Button
                        color="green">
                        Atgal
                    </Button>
                </Link>
            </Form>
        </section>
    )
}