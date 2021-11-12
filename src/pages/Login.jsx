import React from 'react';
import { TextField } from '@material-ui/core';
import { useState } from 'react';
import styled from 'styled-components';

const Container = styled.div`
  height: 100vh;
  width: 100vw;
  display: flex;
  align-items: center;
  justify-content: center;
  background: url("https://img.redbull.com/images/c_limit,w_1500,h_1000,f_auto,q_auto/redbullcom/2018/11/05/b32a8a5c-450c-47a6-8b5a-f0131bb916fd/gamespot");
  background-size: cover;
`
const Wrapper = styled.div`
  padding: 20px;
  width: 40%;
  background-color: white;
`
const Title = styled.h1`
  font-weight: 300;
  text-align: center;
  margin:12px 0px;
`
const Form = styled.form`
display: flex;
flex-wrap: wrap;
flex-direction: column;
`
const Input = styled.input`
  flex: 1;
  min-width: 40%;
  padding: 10px;
  margin-bottom: 16px;
  
`
const SubmitButton = styled.button`
  padding: 10px 15px;
  font-family: inherit;
  font-weight: 600;
  background-color: teal;
  border: none;
  color: white;
  cursor: pointer;
  &:hover {
    background-color: #04d3d3;
    transition: all 0.5s ease;
  }
  
`

export default function Login() {
  const [credentials, setCredentials] = useState({});
  const handleChange = (e) => {
    setCredentials((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };
  const handleClick = (e) => {
    e.preventDefault();
    // to db
    
  };

  return (
    <Container>
      <Wrapper>
        <Title>Giriş Yap</Title>
        <Form>
          <Input name="username" required type="text" placeholder="username" onChange={handleChange}/>
          <Input name="password" required type="password" placeholder="password" onChange={handleChange}/>
          <SubmitButton onClick={handleClick}>Login</SubmitButton>
        </Form>
      </Wrapper>
    </Container>
  )
}
