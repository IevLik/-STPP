import React from 'react';
import { Link ,useNavigate} from 'react-router-dom';
import {
  Nav,
  NavLink,
  Bars,
  NavMenu,
  NavBtn,
  NavBtnLink,
} from './NavbarElements';
  
const Navbar = () => {
  return (
    <>
      <Nav>
        <Bars />
  
        <NavMenu>
          <NavLink to='/chain' activeStyle>
            Restoranų tinklai
          </NavLink>
          
          {/* Second Nav */}
          {/* <NavBtnLink to='/sign-in'>Sign In</NavBtnLink> */}
        </NavMenu>
        <NavBtn>
          <NavBtnLink to='/login'>Prisijungti/Atsijungti</NavBtnLink>
          <NavBtnLink to='/register'>Registruotis</NavBtnLink>
        </NavBtn>
      </Nav>
    </>
  );
};
  
export default Navbar;