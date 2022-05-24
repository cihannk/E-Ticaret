import React from "react";
import { useState } from "react";
import styled from "styled-components";
import {register} from "../apiCalls/User"; 
import { useHistory } from 'react-router-dom';

const Container = styled.div`
  height: 100vh;
  width: 100vw;
  display: flex;
  align-items: center;
  justify-content: center;
  background: url("https://img.redbull.com/images/c_limit,w_1500,h_1000,f_auto,q_auto/redbullcom/2018/11/05/b32a8a5c-450c-47a6-8b5a-f0131bb916fd/gamespot");
  background-size: cover;
`;
const Wrapper = styled.div`
  padding: 20px;
  width: 40%;
  background-color: white;
`;
const Title = styled.h1`
  font-weight: 300;
  text-align: center;
  margin: 12px 0px;
`;
const Form = styled.form`
  display: flex;
  flex-wrap: wrap;
  flex-direction: column;
`;
const Input = styled.input`
  flex: 1;
  min-width: 40%;
  padding: 10px;
  margin-bottom: 16px;
`;
const FormError = styled.span`
  color: red;
  font-weight: 300;
  font-size: 0.8em;
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
`;

export default function Register() {
  const history = useHistory();
  const [errorMessage, setErrorMessage] = useState();
  const [credentials, setCredentials] = useState({});
  const handleChange = (e) => {
    setCredentials((prev) => ({ ...prev, [e.target.name]: e.target.value }));
  };
  const handleClick = async (e) => {
    e.preventDefault();
    // to db
    let result = await registerAsync(credentials);
    if (result === "success"){
      history.push("/");
    }
    else{
      //console.log("result",result.response.data.error);
      setErrorMessage(result.response.data.error);
    }
  };

  const registerAsync = async (loginModel) =>{
     try{
       await register(loginModel);
     }
     catch (err){
       return err;
     }
     return "success";
  }
  console.log(credentials);
  return (
    <Container>
      <Wrapper>
        <Title>Kayıt Ol</Title>
        <Form>
          <Input
            name="firstname"
            required
            type="text"
            placeholder="Ad"
            onChange={handleChange}
          />
          <Input
            name="lastname"
            required
            type="text"
            placeholder="Soyad"
            onChange={handleChange}
          />
          <Input
            name="email"
            required
            type="email"
            placeholder="Email"
            onChange={handleChange}
          />
          <Input
            name="password"
            required
            type="password"
            placeholder="Parola"
            onChange={handleChange}
          />
          {errorMessage && <FormError>{errorMessage}</FormError>}
          <SubmitButton onClick={handleClick}>Kayıt Ol</SubmitButton>
        </Form>
      </Wrapper>
    </Container>
  );
}
