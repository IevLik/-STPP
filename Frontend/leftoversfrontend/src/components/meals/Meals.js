import React, { useEffect, useState } from 'react';
import { Table, Button } from 'semantic-ui-react';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';
import useAuth from "../../hooks/UseAuth";

export default function Meals() {
    const { auth } = useAuth();
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const [mealsData, setMealsData] = useState([]);
    const [chainID, setChainID] = useState(null);
    const [restaurantID, setRestaurantID] = useState(null);
    const [chainName, setChainName] = useState('');
    const allowedRoles = (["RestaurantUser", ""])
    useEffect(() => {
        setChainID(localStorage.getItem('ID'))
        setRestaurantID(localStorage.getItem('restaurantID'))
        setChainName(localStorage.getItem('name'))
        axiosPrivate.get('/chain/' + `${localStorage.getItem('ID')}` +'/restaurant/'+`${localStorage.getItem('restaurantID')}` +'/meal')
            .then((getData) => {
                setMealsData(getData.data);
            })
    }, [])
    const onDelete = (id) => {
        axiosPrivate.delete('/chain/' + `${localStorage.getItem('ID')}` +'/restaurant/'+`${localStorage.getItem('restaurantID')}` +'/meal'+`/${id}`)
        .then(() => {
            getData();
        })
    }
    const setData = (data) => {
        localStorage.setItem('mealID', data.id)
        localStorage.setItem('mealName', data.name)
        localStorage.setItem('mealPrice', data.price)
        //localStorage.setItem('price', price)
        //localStorage.setItem('phone', phone)
    }

    const getData = () => {
        axiosPrivate.get('/chain/' + `${localStorage.getItem('ID')}` +'/restaurant/'+`${localStorage.getItem('restaurantID')}` +'/meal')
            .then((getData) => {
                console.log(getData.data);
                setMealsData(getData.data);
            })
    }
    const styles = {
        table: {
          borderCollapse: "collapse",
          marginLeft: "10vh",
          marginRight: "10vh",
          alignSelf: "center",
        },
        th: {
          
          padding: 8,
          
          fontWeight: "bold",
          textAlign: "center",
          backgroundColor: "rgba(0,0,0,0.3)",
        },
        td: {
          padding: 8,
          width: "150px"
        },
        tr: {
            textAlign: "left",
          },
      };
    return (
        <section2>
            <h1> {chainName} Restorano siūlomi patiekalai</h1>
            <br></br>
            <Table celled style={styles.table}>
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell style={styles.th}>ID</Table.HeaderCell>
                        <Table.HeaderCell style={styles.th}>Pavadinimas</Table.HeaderCell>
                        <Table.HeaderCell style={styles.th}>Kaina</Table.HeaderCell>
                        <Table.HeaderCell style={styles.th}>Pirkti</Table.HeaderCell>
                        <Table.HeaderCell style={styles.th}>Trinti</Table.HeaderCell>
            
                    </Table.Row>
                </Table.Header>
                <Table.Body>
                    {mealsData.map((data) => {
                        return (
                            <Table.Row>
                                <Table.Cell style={styles.td}>{data.id}</Table.Cell>
                                <Table.Cell style={styles.td}>{data.name}</Table.Cell>
                                <Table.Cell style={styles.td}>{data.price}</Table.Cell>
                                
                                <Table.Cell style={styles.td}>
                                    <Link to='/buy'>
                                        <Button
                                            color="green"
                                            onClick={() => setData(data.id)}>
                                            Pirkti
                                        </Button>
                                    </Link>
                                </Table.Cell>
                                {auth?.roles?.find(role => allowedRoles?.includes(role)) && 
                                (<Table.Cell style={styles.td}>
                                    <Button color="red" onClick={() => onDelete(data.id)}>Trinti</Button>
                                </Table.Cell>)}
                                
                            </Table.Row>
                        )
                    })}

                </Table.Body>
            </Table>
            <br></br>
            {auth?.roles?.find(role => allowedRoles?.includes(role)) 
             && 
             (<Link to='add'>
                <Button
                    color="blue">
                    Sukurti patiekalą
                </Button>
            </Link>)}
            <Link to={'/restaurant'}>
                <Button
                    color="blue">
                    Atgal
                </Button>
            </Link>
        </section2>
    )
}