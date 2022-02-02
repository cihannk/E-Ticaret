import React from 'react'
import { Link } from 'react-router-dom';
import styled from 'styled-components'

const Container = styled.div`
  display: flex;
  width: 100vw;
  height: 60vh;
`;
const ImgContainer = styled.div`
  flex: 4;
  //background-color: red;
  display: flex;
  justify-content: center;
  align-items: center;
`;
const SliderImg = styled.img`
  height: 100%;
`;
const InfoContainer = styled.div`
  flex: 6;
  //background-color: yellow;
  display: flex;
  flex-direction: column;
`;
const Info = styled.div`
  padding: 16px;
`;
const InfoHeader = styled.h2`
  font-size: 64px;
  text-align: center;
`;
const InfoDesc = styled.span`
  font-size: 24px;
  font-weight: 400;
  display: inline-block;
  padding: 0px 0px 24px;
`;
const InfoButton = styled.button`
  justify-content: flex-end;
  padding: 15px 20px;
  background-color: transparent;
  border: 1px solid black;
  font-size: 16px;
  font-family: inherit;
  font-weight: bold;
  cursor: pointer;
`;

export default function SliderItem({title, desc, img, id}) {
    return (
        <Container>
          <ImgContainer>
            <SliderImg src={img} />
          </ImgContainer>
          <InfoContainer>
            <Info>
              <InfoHeader>{title}</InfoHeader>
              <InfoDesc>
                {desc}
              </InfoDesc>
              <Link to={`product/${id}`}>
                <InfoButton>Satın Al</InfoButton>
              </Link>
            </Info>
          </InfoContainer>
        </Container>
    )
}
