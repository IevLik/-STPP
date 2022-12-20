import axios from 'axios';

export default axios.create({
    baseURL: 'https://leftoversapi.azurewebsites.net/api'
});

export const axiosPrivate = axios.create({
    baseURL: 'https://leftoversapi.azurewebsites.net/api',
    headers: { 'Content-Type': 'application/json' },
    withCredentials: true
});