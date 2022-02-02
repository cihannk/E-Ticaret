import React from 'react';
import { useHistory } from "react-router-dom";
import styled from 'styled-components';

const Container = styled.div`
    flex:1;
    min-width: 25%;
    max-width: 33%;
    display: flex;
    flex-direction: column;
    position: relative;
    align-items: center;
    justify-content: center;
    height: 350px;
    border: 0.5px solid lightgray;
    border-radius: 5px;
    margin:6px;
    /* &:hover{
        background-color: lightgray;
        transition: all 1s ease;
    } */
    cursor: pointer;
`
const ClickableScreen = styled.div`
    width: 100%;
    height: 100%;
    position: absolute;
    opacity: 0;
    &:hover{
        opacity: 0.2;
        transition: all 0.5s ease;
    }
    background-color: black;
`
const ProductImg = styled.img`
    width: 70%;
    height: 70%;
    object-fit: contain;
`
const ProductTitle = styled.span`
    padding: 6px 16px;
    text-align: center;
`
const ProductPriceContainer = styled.div`
    display: flex;
    align-items: center;
`
const ProductPrice = styled.span`
    font-weight: 600;
`

export default function Product({img, title, price, id}) {
    const history = useHistory();
    const handleClick = () =>{
        console.log("burada");
        history.push(`/product/${id}`);
        console.log(id);
    }
    return (
        <Container>
            <ClickableScreen onClick={handleClick}/>
            <ProductImg src={img}/>
            <ProductTitle>{title.length < 75 ? title : title = title.slice(0,72)+ "..."}</ProductTitle>
            <ProductPriceContainer>
                <ProductPrice>{price+" TL"}</ProductPrice>
            </ProductPriceContainer>
        </Container>
    )
}
