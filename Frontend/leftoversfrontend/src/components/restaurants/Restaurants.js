import React, { useEffect, useState } from 'react';
import { Table, Button } from 'semantic-ui-react';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';
import useAuth from "../../hooks/UseAuth";

export default function Restaurants() {
    const { auth } = useAuth();
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const [restaurantsData, setRestaurantsData] = useState([]);
    const [chainID, setChainID] = useState(null);
    const [chainName, setChainName] = useState('');
    const allowedRoles = (["Admin", ""])
    useEffect(() => {
        setChainID(localStorage.getItem('ID'))
        setChainName(localStorage.getItem('name'))
        axiosPrivate.get('/chain/' + `${localStorage.getItem('ID')}` +'/restaurant')
            .then((getData) => {
                setRestaurantsData(getData.data);
            })
    }, [])
    
    const setData = (id) => {
        localStorage.setItem('restaurantID', id)
    }

    const getData = () => {
        axiosPrivate.get('/chain/' + `${localStorage.getItem('ID')}` +'/restaurant')
            .then((getData) => {
                console.log(getData.data);
                setRestaurantsData(getData.data);
            })
    }
    const [isOpen, setIsOpen] = useState(false);
    const onDelete = (id) => {
        axiosPrivate.delete('/chain/' + `${localStorage.getItem('ID')}` +'/restaurant'+`/${id}`)
        .then(() => {
            getData();
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
          border: "1px solid #333",
          padding: 8,
          fontWeight: "bold",
          textAlign: "left",
          backgroundColor: "rgba(0,0,0,0.3)",
        },
        td: {
          border: "1px solid #333",
          padding: 8,
          width: "150px"
        },
        tr: {
            textAlign: "left",
          },
      };
    return (
        <section2>
            <h1> {chainName} Restoranai</h1>
            <br></br>
            <Table celled style={styles.table}>
                <Table.Header >
                    <Table.Row>
                        <Table.HeaderCell style={styles.th}>Restorano pavadinimas</Table.HeaderCell >
                        <Table.HeaderCell style={styles.th}>Adresas</Table.HeaderCell >
                        <Table.HeaderCell style={styles.th}>Peržiūrėti patiekalus</Table.HeaderCell >
                        <Table.HeaderCell style={styles.th}>Keisti</Table.HeaderCell >
                        <Table.HeaderCell style={styles.th}>Trinti</Table.HeaderCell >

                    </Table.Row>
                </Table.Header>

                <Table.Body>
                    {restaurantsData.map((data) => {
                        return (
                            <Table.Row>
                                <Table.Cell style={styles.td}>{data.name}</Table.Cell>
                                <Table.Cell style={styles.td}>{data.description}</Table.Cell>
                                <Table.Cell style={styles.td}>
                                    <Link to='/meal'>
                                        <Button
                                            color="green"
                                            onClick={() => setData(data.id)}>
                                            Patiekalai
                                        </Button>
                                    </Link>
                                </Table.Cell>
                                {auth?.roles?.find(role => allowedRoles?.includes(role)) && 
                                (<Table.Cell style={styles.td}>
                                     <Link to='/restaurant/edit'>
                                    <Button 
                                         color="green"
                                            onClick={() => 
                                            {
                                                setData(data.id, data.name, data.description);
                                                setIsOpen(true)}}>
                                            Keisti
                                    </Button>
                                    </Link>
                                </Table.Cell>)}
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
                    Pridėti restoraną
                </Button>
            </Link>)}
            <Link to={'/chain'}>
                <Button
                    color="blue">
                    Atgal
                </Button>
            </Link>
        </section2>
    )
}