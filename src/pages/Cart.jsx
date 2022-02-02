import styled from "styled-components";
import Announcement from "../components/Announcement";
import Navbar from "../components/Navbar";
import { productPageProducts } from "../fakeData";
import { useState, useEffect } from "react";
import {getCartFromUserId} from "../apiCalls/Cart";
import Card from "../components/CartProductCard";

const Container = styled.div`
  padding: 50px 100px;
  display: flex;
`;
const Left = styled.div`
  flex: 4;
`;
const CartTitle = styled.h1`
  font-size: 24px;
  font-weight: 600;
`;
const ProductCartContainer = styled.div`
  //background-color: red;
`;

const Right = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
`;
const CartSummaryContainer = styled.div`
padding: 0px 24px;
  display: flex;
  flex-direction: column;
`;
const CartSummaryContainerTitle = styled.h2`
    font-size: 24px;
    font-weight: 600;
`;
const CartSummaryContainerPriceCont = styled.div`
  display: flex;
  justify-content: space-between;
  margin-bottom: 16px;
`;
const CartSummaryContainerPriceContTitle = styled.span`
    font-size: 16px;
    font-weight: 400;
`;
const CartSummaryContainerPrice = styled.span`
    font-weight: 600;
`;
const Total = styled.span`
    font-weight: 500;
    font-size: 20px;
    margin-bottom: 16px;
`;
const ConfirmCart = styled.button`
    background-color: transparent;
    border: 1px solid lightgray;
    padding: 16px;
    font-family: inherit;
    font-weight: 600;
    &:hover{
        background-color: lightgray;
        transition: all 1s ease;
    }
`;

export default function Cart() {

  const [cart, setCart] = useState(null);
  const [calculateAgain, setCalculateAgain] = useState(false);

  const getCartFromUserIdAsync = async (userId) =>{
    const result = await getCartFromUserId(userId)
    console.log(result.data);
    setCart(result.data);
  }

  // const calculateTotal = () => {
  //   getCartFromUserIdAsync(3);
  // }

  useEffect(()=> {
    console.log("useEffectte");
    getCartFromUserIdAsync(1002);
  },[calculateAgain]);


  return (
    <div>
      <Navbar />
      <Announcement />
      {cart && <Container>
        <Left>
          <CartTitle>Sepetim</CartTitle>
          <ProductCartContainer>
            {cart.cartItems.map((cartItem) => (
              <Card
                calculate = {setCalculateAgain}
                cartId={cart.id}
                productId = {cartItem.product.id}
                img={cartItem.product.imageUrl}
                title={cartItem.product.title}
                price={cartItem.product.price}
                amount={cartItem.amount}
                key={cartItem.product.id}
              />
            ))}
          </ProductCartContainer>
        </Left>
        <Right>
          <CartSummaryContainer>
            <CartSummaryContainerTitle>Sepet Özeti</CartSummaryContainerTitle>
            <CartSummaryContainerPriceCont>
              <CartSummaryContainerPriceContTitle>
                Ürünlerin Toplamı
              </CartSummaryContainerPriceContTitle>
              <CartSummaryContainerPrice>{`${cart.cartTotal} TL`}</CartSummaryContainerPrice>
            </CartSummaryContainerPriceCont>
            <CartSummaryContainerPriceCont>
              <CartSummaryContainerPriceContTitle>
                Kargo
              </CartSummaryContainerPriceContTitle>
              <CartSummaryContainerPrice>12TL</CartSummaryContainerPrice>
            </CartSummaryContainerPriceCont>
            <CartSummaryContainerPriceCont>
              <CartSummaryContainerPriceContTitle>
                Sepette %13 İndirim
              </CartSummaryContainerPriceContTitle>
              <CartSummaryContainerPrice>-12TL</CartSummaryContainerPrice>
            </CartSummaryContainerPriceCont>
            <Total>{`${cart.cartTotal} TL`}</Total>
            <ConfirmCart>Sepeti Onayla</ConfirmCart>
          </CartSummaryContainer>
          
        </Right>
      </Container>}
      
    </div>
  );
}
