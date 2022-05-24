import React, { useEffect, useState } from "react";
import styled from "styled-components";
import SearchIcon from "@material-ui/icons/Search";
import { Link } from "react-router-dom";
import Person from "@material-ui/icons/Person";
import Cart from "@material-ui/icons/ShoppingCart";
import { getFromLocalStorage, removeFromLocalStorage } from "../localStorageOpts";

import { IconButton, Menu, MenuItem } from "@material-ui/core";
import { AccountCircle } from "@material-ui/icons";

const Container = styled.div`
  height: 60px;
`;
const Wrapper = styled.div`
  padding: 10px 20px;
  display: flex;
  align-items: center;
  justify-content: space-between;
`;
const Left = styled.div`
  display: flex;
  flex: 1;
`;
const SearchContainer = styled.div`
  display: flex;
  padding: 2px;
  align-items: center;
  border: 0.5px solid lightgray;
`;
const Search = styled.input`
  padding: 3px 6px;
  border: none;
  margin-right: 6px;
`;
const Mid = styled.div`
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
`;
const Header = styled.h1`
  font-weight: bold;
  margin: 0;
  color: black;
  text-decoration: none;
`;

const Right = styled.div`
  flex: 1;
`;
const UserButtons = styled.div`
  padding: 5px;
  display: flex;
  justify-content: flex-end;
`;
const UserButton = styled.span`
  margin-left: 25px;
  font-size: 14px;
  cursor: pointer;
`;
const UserLoggedButtons = styled.div`
  display: flex;
  //align-items: center;
  justify-content: flex-end;
`;
const UserLoggedButton = styled.div`
  width: 100px;
  display: flex;
  align-items: center;
  margin-left: 10px;
  cursor: pointer;
  &:hover {
    color: green;
  }
`;
const UserLoggedButtonTitle = styled.span`
  font-size: 14px;
  margin-left: 2px;
`;

export default function Navbar() {
  const [user, setUser] = useState(null);
  ///
  const [anchorEl, setAnchorEl] = useState(null);
  const handleMenu = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = (property) => {
    console.log(property);
    switch (property) {
      case ("logout"):
        removeFromLocalStorage("login");
        setUser(null);
        break;
      case ("profile"):
        break;
    }
    setAnchorEl(null);
  };
  const getFromLocalStorageAsync = async () => {
    var credentials = await getFromLocalStorage("login");
    console.log("cre ", credentials);
    setUser(credentials);
  }
  ///
  useEffect(() => {
    console.log("useEffect içinde");
    getFromLocalStorageAsync();
  }, []);

  return (
    <Container>
      <Wrapper>
        <Left>
          <SearchContainer>
            <Search />
            <SearchIcon style={{ color: "gray", width: "20px" }} />
          </SearchContainer>
        </Left>
        <Mid>
          <Link to="/" style={{ textDecoration: "none", color: "inherit" }}>
            <Header>E TICARET</Header>
          </Link>
        </Mid>
        <Right>
          {user ? (
            <UserLoggedButtons>
              <Link to="/cart" style={{ textDecoration: "none", color: "inherit" }}>
                <UserLoggedButton>
                  <Cart />
                  <UserLoggedButtonTitle>Sepetim</UserLoggedButtonTitle>
                </UserLoggedButton>
              </Link> 

              <UserLoggedButton onClick={handleMenu}>
                <Person />
                <UserLoggedButtonTitle>Hesabım</UserLoggedButtonTitle>
              </UserLoggedButton>
              <Menu
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                keepMounted
                transformOrigin={{
                  vertical: 'top',
                  horizontal: 'right',
                }}
                open={Boolean(anchorEl)}
                onClose={handleClose}
              >
                <MenuItem onClick={() => handleClose("profile")}>Profil</MenuItem>
                <MenuItem onClick={() => handleClose("logout")}>Çıkış Yap</MenuItem>
              </Menu>
            </UserLoggedButtons>
          ) : (
            <UserButtons>
              <Link
                to="/login"
                style={{ textDecoration: "none", color: "inherit" }}
              >
                <UserButton>Login</UserButton>
              </Link>
              <Link
                to="/register"
                style={{ textDecoration: "none", color: "inherit" }}
              >
                <UserButton>Register</UserButton>
              </Link>
            </UserButtons>
          )}
        </Right>
      </Wrapper>
    </Container>
  );
}
