import React, { useState, useEffect } from 'react';
import { Form, Button } from 'semantic-ui-react';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';

export default function EditRestaurant() {
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [restaurantID, setID] = useState(null);
    const sendDataToAPI = () => {
        axiosPrivate.put('/chain/' + `${localStorage.getItem('ID')}` +'/restaurant/'+`${localStorage.getItem('restaurantID')}`, {
            name,
            description
        }).then(() => {
            navigate("/restaurant");//gal /detailed
        })
    }

    useEffect(() => {
        setName(localStorage.getItem('restaurantName'));
        setDescription(localStorage.getItem('restaurantDescription'));
        setID(localStorage.getItem('restaurantID'))
    }, [])

    return (
        <section>
            <h1>Redaguoti restoraną</h1>
            <br></br>
            <Form>
                <Form.Field>
                    <label>Restorano pavadinimas</label>
                    <br></br>
                    <input name="fname"
                        type="text"
                        autoComplete="off"
                        required
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        placeholder='Pavadinimas' />
                </Form.Field>
                <Form.Field>
                    <label>Aprašymas</label>
                    <br></br>
                    <input
                        name="lname"
                        type="text"
                        autoComplete="off"
                        required
                        value={description}
                        placeholder='Aprašymas'
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </Form.Field>
                
                
                <Button type='submit' onClick={sendDataToAPI}>Pakeisti</Button>
                <Link to='/restaurant'>
                    <Button
                        color="green">
                        Atgal
                    </Button>
                </Link>
            </Form>
        </section>
    )
}