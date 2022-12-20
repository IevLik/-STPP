import React, { useState, useEffect } from 'react';
import { Form, Button} from 'semantic-ui-react';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';

const EditChainsModal=({setIsOpen}) =>{
    const CHAINS_URL = '/chain';
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [ID, setID] = useState(null);
    const [chainsData, setChainsData] = useState([]);
    const sendDataToAPI = () => {
        axiosPrivate.put(CHAINS_URL+`/${ID}`, {
            name,
            description
        }).then(setIsOpen(false)).then(() => {
            getData();
        }).then(() => {
            navigate("/chain/editModal");
        }).then(() => {
            navigate("/chain");
        })
    }
    const getData = () => {
        axiosPrivate.get(CHAINS_URL)
            .then((getData) => {
                setChainsData(getData.data);
            })
    }
    useEffect(() => {
        setName(localStorage.getItem('name'));
        setDescription(localStorage.getItem('description'));
        setID(localStorage.getItem('ID'))
        axiosPrivate.get(CHAINS_URL)
            .then((getData) => {
                setChainsData(getData.data);
                
            })
    }, [])

    return (
        <section>
            <div onClick={() => setIsOpen(false)}/>
            
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
                <div onClick={() => setIsOpen(false)}/>
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
                
                <Button type='submit' onClick={sendDataToAPI}>Patvirtinti</Button>
                
                <Link to='/chain'>
                    <Button onClick={() => setIsOpen(false)}
                        color="green">
                        Atgal
                    </Button>
                </Link>
            </Form>

        </section>
    );
};
export default EditChainsModal;