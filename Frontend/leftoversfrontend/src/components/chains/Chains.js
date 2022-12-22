import React, { useEffect, useState } from 'react';
import ReactDOM from "react-dom";
import { Table, Button } from 'semantic-ui-react';
//import axios from '../../api/axios';
import useAxiosPrivate from "../../hooks/UseAxiosPrivate";
import { Link ,useNavigate} from 'react-router-dom';
import useAuth from "../../hooks/UseAuth";
import 'reactjs-popup/dist/index.css';
import EditChainModal from "./EditChainModal";
import { useContext } from "react";
import AuthContext from "../../context/AuthProvider";
import { FaBorderNone, FaQuestionCircle } from 'react-icons/fa';
import Modal from "./Modal";



export default function Chains() {
    

    const { auth } = useAuth();
    const CHAINS_URL = '/chain';
    const axiosPrivate = useAxiosPrivate();
    const navigate = useNavigate();
    const [chainsData, setChainsData] = useState([]);
    const allowedRoles = (["Admin", ""])
    useEffect(() => {
        axiosPrivate.get(CHAINS_URL)
            .then((getData) => {
                setChainsData(getData.data);
                
            })
    }, [])
    const [show, setShow] = useState(false);
    const { setAuth } = useContext(AuthContext);
    const logout = async () => {
        // if used in more components, this should be in context 
        // axios to /logout endpoint 
        setAuth({});
        navigate('/login');
    }

    const setData = (id, name, description) => {
        localStorage.setItem('ID', id)
        localStorage.setItem('name', name)
        localStorage.setItem('description', description)
    }

    const getData = () => {
        axiosPrivate.get(CHAINS_URL)
            .then((getData) => {
                setChainsData(getData.data);
            })
    }
    

    const onDelete = (id) => {
        axiosPrivate.delete(CHAINS_URL+`/${id}`)
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
      
        butt: {
            padding: 0,
            border: "0px solid #333",
            backgroundColor: "rgba(0,0,0,0.1)",
            backgroundColoropacity: 0,


          },
        
      };
    return (
        <section2>
            
            <h1>Restoranų tinklai 
                <Button onClick={() => setShow(true)} style={styles.butt}>
                    <FaQuestionCircle/>
                </Button>
                <Modal title="Trumpa informacija" onClose={() => setShow(false)} show={show}>
               
                </Modal>
                
                </h1>
            <br></br>
            <Table celled style={styles.table}>
            
                <Table.Header style={styles.th}>
                    <Table.Row style={styles.th}>
                        <Table.HeaderCell style={styles.th}>Pavadinimas</Table.HeaderCell>
                        <Table.HeaderCell style={styles.th}>Aprašymas</Table.HeaderCell>
                        <Table.HeaderCell style={styles.th}>Restoranai</Table.HeaderCell>
                        {auth?.roles?.find(role => allowedRoles?.includes(role)) && 
                        (<Table.HeaderCell style={styles.th}>Kesiti</Table.HeaderCell>)}
                        {auth?.roles?.find(role => allowedRoles?.includes(role)) && 
                        (<Table.HeaderCell style={styles.th}>Trinti</Table.HeaderCell>)}
            
                    </Table.Row>
                </Table.Header>

                <Table.Body style={styles.td}>
                    {chainsData.map((data) => {
                        return (
                            <Table.Row style={styles.td}>
                                <Table.Cell style={styles.td}>{data.name}</Table.Cell>
                                <Table.Cell style={styles.td}>{data.description}</Table.Cell>
                                <Table.Cell style={styles.td}>
                                    <Link to='/restaurant'>
                                        <Button
                                            color="green"
                                            onClick={() => setData(data.id, data.name, data.description)}>
                                            Peržiūrėti restoranus
                                        </Button>
                                    </Link>
                                </Table.Cell>
                                {auth?.roles?.find(role => allowedRoles?.includes(role)) && 
                                (<Table.Cell style={styles.td}>
                                   <Link to='/chain/edit'>
                                    <Button 
                                         color="green"
                                            onClick={() => 
                                            {
                                                setData(data.id, data.name, data.description);
                                                
                                              }}>
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
            (<Link to='/chain/add'>
                <Button
                    color="blue">
                    Pridėti restoranų tinklą
                </Button>
            </Link>)}
            
        </section2>
    )
}