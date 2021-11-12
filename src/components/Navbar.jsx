import React from "react";
import styled from "styled-components";
import SearchIcon from "@material-ui/icons/Search";

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

export default function Navbar() {
  return (
    <Container>
      <Wrapper>
        <Left>
          <SearchContainer>
            <Search />
            <SearchIcon style={{color: "gray", width:"20px"}}/>
          </SearchContainer>
        </Left>
        <Mid>
          <Header>E TICARET</Header>
        </Mid>
        <Right>
          <UserButtons>
            <UserButton>Login</UserButton>
            <UserButton>Register</UserButton>
          </UserButtons>
        </Right>
      </Wrapper>
    </Container>
  );
}
