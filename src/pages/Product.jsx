import React from "react";
import { useState } from "react";
import styled from "styled-components";
import Announcement from "../components/Announcement";
import Navbar from "../components/Navbar";
import Add from "@material-ui/icons/Add";
import Remove from "@material-ui/icons/Remove";
import { Button, IconButton, Snackbar } from "@material-ui/core";
import CloseIcon from "@material-ui/icons/Close";
import { getProduct } from "../apiCalls/Product";
import { useEffect } from "react";
import { useLocation } from "react-router-dom";

import {userAddsProductToCart} from "../apiCalls/Cart"
import {cartItem} from "../models/cart/cartItem";

import { getFromLocalStorage } from "../localStorageOpts";

const ProductContainer = styled.div`
  padding: 100px;
`;
const ProductWrapper = styled.div`
  height: 100vh;
  display: flex;
`;
const ProductImgContainer = styled.div`
  width: 50%;
  padding: 24px;
`;
const ProductImg = styled.img`
  width: 100%;
  height: 100%;
  object-fit: cover;
`;
const ProductInfoContainer = styled.div`
  width: 50%;
  padding: 24px;
  display: flex;
  flex-direction: column;
`;
const ProductTitle = styled.span`
  font-size: 36px;
  font-weight: 600;
`;
const ProductPrice = styled.span`
  font-size: 24px;
  font-weight: 300;
  padding: 12px 0px;
`;
const ProductColorContainer = styled.div`
  display: flex;
  padding: 16px 0px;
`;
const ProductColor = styled.div`
  width: 24px;
  height: 24px;
  border-radius: 50%;
  background-color: ${(props) => props.color};
  margin-right: 6px;
`;
const ProductDesc = styled.span`
  font-size: 20px;
  font-weight: 400;
`;
const ButtonsContainer = styled.div`
  padding: 16px 0px;
  display: flex;
  align-items: center;
`;
const AmountButtonContainer = styled.div`
  display: flex;
`;
const AmountButton = styled.button`
  width: 32px;
  height: 32px;
  border-radius: 50%;
  border: 2px solid lightgray;
  margin: 0px 6px;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #585353;
  background-color: transparent;
  &:hover {
    background-color: #e2dcdc;
    transition: all 0.5s ease;
  }
`;
const Amount = styled.span`
  font-size: 24px;
`;
const AddCartButton = styled.button`
  padding: 14px 24px;
  background-color: transparent;
  border-radius: 6px;
  border: 2px solid lightgray;
  margin-left: 12px;
  &:hover {
    background-color: #e2dcdc;
    transition: all 0.5s ease;
  }
  font-family: inherit;
`;

export default function Product() {
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [amount, setAmount] = useState(1);
  const [product, setProduct] = useState(null);
  const [productId, setProductId] = useState(null);
  const location = useLocation();

  useEffect(()=>{
    let locationPath = location.pathname;
    let productId = locationPath.split("/")[2];
    setProductId(productId);
    getProductAsync(productId);
  },[])

  const getProductAsync = async (id) =>{
    let product = await getProduct(id);
    setProduct(product.data);
  }
  const handleAmount = (way) => {
    if (way === "+") {
      setAmount((prev) => prev + 1);
    } else {
      amount > 1 && setAmount((prev) => prev - 1);
    }
  };
  const handleClick = async() => {
    setOpenSnackbar(true);

    let newCartItem = cartItem;
    newCartItem.cartItems[0].amount = amount;
    newCartItem.cartItems[0].productId = productId;

    let login = await getFromLocalStorage("login");
    console.log("getFromLocalStorage: ",login);

    newCartItem.userCartId = login.id;
    const response = await userAddsProductToCart(newCartItem);
  };
  const handleClose = () => {
    setOpenSnackbar(false);
  };
  const action = (
    <React.Fragment>
      <Button color="secondary" size="small" onClick={handleClose}>
        UNDO
      </Button>
      <IconButton
        size="small"
        aria-label="close"
        color="inherit"
        onClick={handleClose}
      >
        <CloseIcon fontSize="small" />
      </IconButton>
    </React.Fragment>
  );
  return (
    <div>
      <Navbar />
      <Announcement />
      <ProductContainer>
        <ProductWrapper>
          <ProductImgContainer>
            <ProductImg src={product?.imageUrl} />
          </ProductImgContainer>
          <ProductInfoContainer>
            <ProductTitle>{product?.title}</ProductTitle>
            <ProductPrice>{`$ ${product?.price}`}</ProductPrice>
            <ProductDesc>
              {product?.description}
            </ProductDesc>
            <ProductColorContainer>
              <ProductColor color="black" />
              <ProductColor color="yellow" />
              <ProductColor color="gray" />
            </ProductColorContainer>
            <ButtonsContainer>
              <AmountButtonContainer>
                <AmountButton onClick={() => handleAmount("-")}>
                  <Remove />
                </AmountButton>
                <Amount>{amount}</Amount>
                <AmountButton onClick={() => handleAmount("+")}>
                  <Add />
                </AmountButton>
              </AmountButtonContainer>
              <AddCartButton onClick={handleClick}>Sepete Ekle</AddCartButton>
              <Snackbar
                open={openSnackbar}
                autoHideDuration={2000}
                onClose={handleClose}
                message="Sepete eklendi"
                action={action}
                anchorOrigin={{vertical: "bottom", horizontal: "left"}}
              />
            </ButtonsContainer>
          </ProductInfoContainer>
        </ProductWrapper>
      </ProductContainer>
    </div>
  );
}
