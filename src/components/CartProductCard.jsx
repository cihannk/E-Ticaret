import React, { useState } from "react";
import { useUser } from "../contexts/CartContext";
import styled from "styled-components";
import { Remove, Add, DeleteOutline } from "@material-ui/icons";
import {changeOrDeleteCartWithUserId} from "../apiCalls/Cart";
import { changeCartItem } from "../models/cart/changeCartItem";
import { addToCart, getCartCount, removeFromCart } from "../localStorageOpts";

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

export default function CartProductCard({ calculate, product, amount }) {
  const [counter, setCounter] = useState(amount);
  let context = useUser();

  const handleCounter = (way) => {
    if (way === "+") {
      addToCart({product, amount: 1});
      setCounter((prev) => prev + 1);
      calculate(prev => !prev);
    } 
    else {
      if (counter > 1) {
        removeFromCart(product.id, 1);
        setCounter((prev) => prev - 1);
        calculate(prev => !prev);
      }
    }
  };
  const handleDelete = async () =>{
    await removeFromCart(product.id, counter);
    let cartCount = await getCartCount();
    console.log(cartCount);
    context.setCartCount(cartCount);
    calculate(prev => !prev);
  }

  return (
    <ProductCard>
      <ProductCardImg src={product.imageUrl} />
      <ProductCardTitle>
        {product.title < 65 ? product.title : (product.title.slice(0, 62) + "...")}
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
      <Price>{product.price} TL</Price>
      <DeleteButton onClick={handleDelete}>
        <DeleteOutline/>
      </DeleteButton>
    </ProductCard>
  );
}
