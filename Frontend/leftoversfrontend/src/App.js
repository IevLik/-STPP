import logo from './logo.svg';

import { Routes, Router, Route } from 'react-router-dom'
import './App.css';
import Navbar  from './components/navBar/Navbar';

import Admin from './components/Admin';
import Home from './components/Home';
import Layout from './components/Layout';
import LinkPage from './components/LinkPage';
import Login from './components/Login';
import Buy from './components/Buy';
import Missing from './components/Missing';
import Register from './components/Register';
import RequireAuth from './components/RequireAuth';
import RestaurantUser from './components/RestaurantUser';
import Unauthorised from './components/Unauthorised';

import Restaurants from './components/restaurants/Restaurants';
import AddRestaurants from './components/restaurants/AddRestaurants';
import EditRestaurants from './components/restaurants/EditRestaurants';

import Meals from './components/meals/Meals';
import AddMeals from './components/meals/AddMeals';
import EditMeals from './components/meals/EditMeals';

import Chains from './components/chains/Chains';
import AddChains from './components/chains/AddChains';
import EditChains from './components/chains/EditChains';
import EditChainModal from './components/chains/EditChainModal';
const Roles = {
  'Admin': "Admin",
  'RestaurantUser': "RestaurantUser"
}

function App() {

  return (
    <div>
      <Navbar />

      <Routes>
      
        <Route path="/" element={<Layout />}>
          {/* public routes */}
          <Route path="linkpage" element={<LinkPage />} />
          <Route path="login" element={<Login />} />
          <Route path="home" element={<Home />} />
          <Route path="buy" element={<Buy />} />
          <Route path="register" element={<Register />} />
          <Route path="unauthorised" element={<Unauthorised />} />
          <Route path="/" element={<Login />} />
          <Route path="chain" element={<Chains />} />
          <Route path="restaurant" element={<Restaurants />} />
          <Route path="meal" element={<Meals />} />
          
          {/* we want to protect these routes */}

          <Route element={<RequireAuth allowedRoles={[Roles.RestaurantUser]} />}>
            <Route path="meal/add" element={<AddMeals />} />
            <Route path="meal/edit" element={<EditMeals />} />
          </Route>


          <Route element={<RequireAuth allowedRoles={[Roles.Admin]} />}>
          <Route path="restaurant/add" element={<AddRestaurants />} />
            <Route path="restaurant/edit" element={<EditRestaurants />} />
            <Route path="chain/add" element={<AddChains />} />
            <Route path="chain/edit" element={<EditChains />} />
            <Route path="chain/editModal" element={<EditChainModal />} />
            
          </Route>
          {/* catch all */}
          <Route path="*" element={<Missing />} />
        </Route>
      </Routes>
      </div>

  );
}

export default App;
