import React, { useState } from 'react';
import { Form, Button } from 'semantic-ui-react';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';
import { useHistory } from 'react-router';

export default function AddChains() {
    const CHAINS_URL = '/chain';
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
  //let history = useHistory();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');

  const sendDataToAPI = () => {
    axiosPrivate.post(CHAINS_URL, {
        name,
        description
    }).then(() => {
        navigate("/chain");
    })
  }
  return (
    <section>
      <h1>Pridėti restoranų tinklą</h1>
      <br></br>
      <Form>
        <Form.Field>
          <label>Restorano tinklo pavadinimas</label>
          <br></br>
          <input name="fname" 
          type="text"
          autoComplete="off"
          required
          onChange={(e) => setName(e.target.value)} 
          placeholder='Tinklo pavadinimas' />
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