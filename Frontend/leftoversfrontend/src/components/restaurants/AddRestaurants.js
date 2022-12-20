import React, { useEffect, useState } from 'react';
import { Form, Button } from 'semantic-ui-react';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';

export default function AddRestaurant() {
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [chainID, setChainID] = useState(null);
    useEffect(() => {
    setChainID(localStorage.getItem('ID'))
    }, [])
    const sendDataToAPI = () => {
    axiosPrivate.post('/chain/' + `${chainID}` +'/restaurant', {
        name,
        description
    }).then(() => {
        navigate('/restaurant');
    })
  }
  return (
    <section>
      <h1>Pridėti restoraną</h1>
      <br></br>
      <Form>
        <Form.Field>
          <label>Restorano pavadinimas</label>
          <br></br>
          <input name="fname" 
          type="text"
          autoComplete="off"
          required
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
          placeholder='Aprašymas' 
          onChange={(e) => setDescription(e.target.value)} 
          />
        </Form.Field>
       
        <Button type='submit' onClick={sendDataToAPI}>Pridėti</Button>
        <Link to={'/restaurant'}>
            <Button
                color="green">
                Atgal
            </Button>
        </Link>
      </Form>
    </section>
  )
}