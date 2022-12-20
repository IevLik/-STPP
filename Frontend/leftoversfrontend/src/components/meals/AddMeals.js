import React, { useEffect, useState } from 'react';
import { Form, Button } from 'semantic-ui-react';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';
import useAuth from "../../hooks/UseAuth";

export default function AddMeal() {
  const { auth } = useAuth();
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const [name, setName] = useState('');
    const [price, setPrice] = useState('');
    const [chainID, setChainID] = useState(null);
    const [restaurantID, setRestaurantID] = useState(null);
    const allowedRoles = (["RestaurantUser", ""])
    useEffect(() => {
    setChainID(localStorage.getItem('ID'))
    }, [])

    const sendDataToAPI = () => {
    axiosPrivate.post('/chain/' + `${chainID}` +'/restaurant/'+ `${localStorage.getItem('restaurantID')}` +'/meal', {
        name,
        price
    }).then(() => {
        navigate('/meal');
    })
  }
  return (
    <section>
      <h1>Pridėti patiekalą</h1>
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
          <label>Kaina</label>
          <br></br>
          <input 
          name="lname" 
          type="text"
          autoComplete="off"
          placeholder='Kaina' 
          onChange={(e) => setPrice(e.target.value)} 
          />
        </Form.Field>
        
            <Button type='submit' onClick={sendDataToAPI}>Pridėti</Button>
        <Link to={'/meal'}>
            <Button
                color="green">
                Atgal
            </Button>
        </Link>
      </Form>
    </section>
  )
}