import React, { useState } from "react";
import styled from "styled-components";
import { Remove, Add, DeleteOutline } from "@material-ui/icons";
import {changeOrDeleteCartWithUserId} from "../apiCalls/Cart";
import { changeCartItem } from "../models/cart/changeCartItem";

const ProductCard = styled.div`
  //padding: 6px 12px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  //background-color: yellow;
  height: 100px;
  border-bottom: 0.5px solid lightgray;
  box-shadow: rgba(0, 0, 0, 0.15) 1.95px 1.95px 2.6px;
`;
const ProductCardImg = styled.img`
  height: 80%;
  width: 10%;
`;
const ProductCardTitle = styled.span`
  font-size: 12px;
  font-weight: 600;
  padding: 6px;
  width: 40%;
`;
const AmountButtonContainer = styled.div`
  display: flex;
  align-items: center;
  //border-left: 1px solid black;
`;
const AmountButton = styled.button`
  width: 20px;
  height: 20px;
  border-radius: 50%;
  border: none;
  margin: 0px 6px;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: transparent;
  &:hover {
    background-color: lightgray;
    transition: all 0.5s ease;
  }
`;
const Amount = styled.span`
  font-size: 20px;
`;
const Price = styled.span`
  width: 10%;
  text-align: center;
  align-items: center;
`;
const DeleteButton = styled.div`
  padding: 16px;
  cursor: pointer;
`;

export default function CartProductCard({ calculate, productId, cartId, img, title, price, amount }) {
  const [counter, setCounter] = useState(amount);
  const handleCounter = (way) => {
    if (way === "+") {
      changeOrDeleteCartWithUserIdAsync(counter + 1);
      setCounter((prev) => prev + 1);
      
    } else {
      if (counter > 1) {
        changeOrDeleteCartWithUserIdAsync(counter - 1);
        setCounter((prev) => prev - 1);
      }
    }
  };
  const handleDelete = () =>{
    changeOrDeleteCartWithUserIdAsync(0);
  }

  const changeOrDeleteCartWithUserIdAsync = async (amount) => {
    calculate(prev => !prev);
    let item = changeCartItem;
    item.amount = amount;
    item.productId = productId;
    item.userCartId = cartId;

    let card = await changeOrDeleteCartWithUserId(item);
    console.log(card);
    return card;
  }
  return (
    <ProductCard>
      <ProductCardImg src={img} />
      <ProductCardTitle>
        {title < 65 ? title : (title = title.slice(0, 62) + "...")}
      </ProductCardTitle>
      <AmountButtonContainer>
        <AmountButton onClick={() => handleCounter("-")}>
          <Remove />
        </AmountButton>
        <Amount>{counter}</Amount>
        <AmountButton onClick={() => handleCounter("+")}>
          <Add />
        </AmountButton>
      </AmountButtonContainer>
      <Price>{price} TL</Price>
      <DeleteButton onClick={handleDelete}>
        <DeleteOutline/>
      </DeleteButton>
    </ProductCard>
  );
}
