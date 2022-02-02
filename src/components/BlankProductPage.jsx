import React from 'react';
import styled from "styled-components";

const Container = styled.div`
    display: flex;
    align-items: center;
    height: 20vh;
    
`
const TitleContainer = styled.div`
    width: 80%;
    display: flex;
    align-items: center;
    justify-content: center;

`
const Title = styled.span`
    font-size: 3em;
    font-family: inherit;
    color: #7b7373;

`

export default function BlankProductPage() {


  return (
  <Container>
      <TitleContainer>
        <Title>Henüz ürün yok...</Title>
      </TitleContainer>
      
  </Container>
  )

}

