import React, { useState, useEffect } from 'react';
import { Form, Button} from 'semantic-ui-react';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';

export default function EditChains() {
    const CHAINS_URL = '/chain';
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [ID, setID] = useState(null);
    const sendDataToAPI = () => {
        axiosPrivate.put(CHAINS_URL+`/${ID}`, {
            name,
            description
        }).then(() => {
            navigate("/chain");
        })
    }

    useEffect(() => {
        setName(localStorage.getItem('name'));
        setDescription(localStorage.getItem('description'));
        setID(localStorage.getItem('ID'))
    }, [])

    return (
        <section>
            <h1>Keisti {name}</h1>
            <br></br>
            <Form>
                <Form.Field>
                    <label>Restoranų tinklo pavadinimas</label>
                    <br></br>
                    <input name="fname"
                        type="text"
                        autoComplete="off"
                        required
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        placeholder='Chain Name' />
                </Form.Field>
                <Form.Field>
                    <label>Aprašymas</label>
                    <br></br>
                    <input
                        name="lname"
                        type="text"
                        autoComplete="off"
                        value={description}
                        placeholder='Description'
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </Form.Field>
                <Button type='submit' onClick={sendDataToAPI}>Keisti</Button>
                <Link to='/chain'>
                    <Button
                        color="green">
                        Atgal
                    </Button>
                </Link>
            </Form>
        </section>
    )
}