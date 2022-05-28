import styled from "styled-components";
import Announcement from "../components/Announcement";
import Navbar from "../components/Navbar";
import { useState, useEffect } from "react";
import Card from "../components/CartProductCard";
import { EmptyCart, getFromLocalStorage } from "../localStorageOpts";
import { Elements } from '@stripe/react-stripe-js';
import { loadStripe } from '@stripe/stripe-js';
import { PaymentElement } from '@stripe/react-stripe-js';
import { getClientSecret } from "../apiCalls/Order";
import CheckoutForm from "../components/CheckoutForm";
import { useUser } from "../contexts/CartContext";
import { useHistory } from "react-router-dom";

const Container = styled.div`
  padding: 50px 100px;
  display: flex;
`;
const PaymentContainer = styled.div`
  padding: 0px 100px;
  padding-bottom: 50px;
  display: flex;
`
const PayWithCardContainer = styled.div`
  border: 0.5px solid lightgray;
  border-radius: 2%;
  padding: 1em;
`
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
const ConfirmCartError = styled.span`
    padding: 12px 8px; 
    font-size: 1em;
    font-weight: 400;
    color: #df1b41;
`
const PaymentSummaryContainer = styled.div`
  padding: 50px 100px;
  display: flex;
  flex-direction: column;
`
const PaymentSummaryContainerTitle = styled.span`
    font-size: 2em;
    font-weight: 600;
    margin-bottom: 1em;
`

const BackToMainPageButtonContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: flex-end;
`
const BackToMainPageButton = styled.button`
  background-color: #1976d2;
    border: 1px solid lightgray;
    padding: 16px;
    font-family: inherit;
    font-weight: 600;
    color: white;
    &:hover{
        background-color: #589ce0;
        transition: all 1s ease;
    }
`

const stripePromise = loadStripe('pk_test_51L2jVZKySBPkhRBGJC3ggXBNCfOJ6qDBUUlGCmr4xeR0BuWNfWJEn5bvRlYe9nCyoAz38SgW5jdEoLeX0l7cRHZq00pRTjA1XA');

export default function Cart() {

  // const options = {
  //   // passing the client secret obtained from the server
  //   clientSecret: '{{CLIENT_SECRET}}',
  // };

  const context = useUser();
  const history = useHistory();

  const [cart, setCart] = useState(null);
  const [cartTotal, setCartTotal] = useState(0);
  const [intentId, setIntentId] = useState("");
  const [calculateAgain, setCalculateAgain] = useState(false);
  const [confirmCart, setConfirmCart] = useState(false);
  const [confirmCartError, setConfirmCartError] = useState("");
  const [stripeOptions, setStripeOptions] = useState();
  const [paymentComplete, setPaymentComplete] = useState(false);

  const getCartAsync = async () => {
    let cart = await getFromLocalStorage("cart");
    console.log("getCartAsync", cart);
    setCart(cart);
    let cartTotal = getCartTotal(cart);
    setCartTotal(cartTotal);
  }

  const getCartTotal = (cart) => {
    let sum = cart?.reduce((partialSum, rest) => partialSum + rest.product.price * rest.amount, 0);
    return sum;
  }

  const emptyCart = () =>{
    EmptyCart();
    context.setCartCount(0);
  }

  const handleConfirmCart = async (price) => {
    try {
      await setOptionsAsync(price);
      setConfirmCart(true);
      setConfirmCartError("");
    } catch (error) {
      console.log(error);
      setConfirmCartError(error.message);
    }

  }
  const handleReturnMainPageButton = (e) => {
    console.log("x");
    history.push("/");
  }

  const setOptionsAsync = async (price) => {
    let result = await getClientSecret(price);
    setIntentId(result.data.id);
    setStripeOptions({
      clientSecret: result.data.client_secret,
      appearance: {
        theme: "stripe",
        variables: {
          colorPrimary: '#0570de',
          colorBackground: '#ffffff',
          colorText: '#30313d',
          colorDanger: '#df1b41',
          fontFamily: 'Ideal Sans, system-ui, sans-serif',
          spacingUnit: '2px',
          borderRadius: '4px',
          // See all possible variables below
        }
      }

    })
  }

  useEffect(() => {
    getCartAsync();
  }, [])

  useEffect(() => {
    getCartAsync();
  }, [calculateAgain]);

  return (
    <div>
      <Navbar />
      <Announcement />
      <div>
        {cart?.length < 1 || cart === null ? <p>Sepet boş...</p> : (paymentComplete === false ? <div>
          <Container>
            <Left>
              <CartTitle>Sepetim</CartTitle>
              <ProductCartContainer>
                {cart?.map((cartItem) => (
                  <Card
                    calculate={setCalculateAgain}
                    product={cartItem.product}
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
                  <CartSummaryContainerPrice>{`${cartTotal} TL`}</CartSummaryContainerPrice>
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
                <Total>{`${cartTotal} TL`}</Total>
                <ConfirmCart onClick={() => handleConfirmCart(cartTotal)}>Sepeti Onayla</ConfirmCart>
                {confirmCartError && <ConfirmCartError>{confirmCartError}</ConfirmCartError>}
              </CartSummaryContainer>
            </Right>
          </Container>
          <PaymentContainer>
            {confirmCart && (
              <div>
                <CartSummaryContainerTitle>Kart ile öde</CartSummaryContainerTitle>
                <PayWithCardContainer>
                  <Elements stripe={stripePromise} options={stripeOptions} >
                    <CheckoutForm intentId={intentId} cart={cart} setPaymentComplete={setPaymentComplete} />
                  </Elements>
                </PayWithCardContainer>

              </div>

            )}
          </PaymentContainer>
        </div> : (
        <PaymentSummaryContainer>
          {emptyCart()}
          <CartSummaryContainer>
          <PaymentSummaryContainerTitle>Ödeme Başarıyla Tamamlandı</PaymentSummaryContainerTitle>
                <CartSummaryContainerTitle>Ödenen Tutar</CartSummaryContainerTitle>
                <CartSummaryContainerPriceCont>
                  <CartSummaryContainerPriceContTitle>
                    Tutar
                  </CartSummaryContainerPriceContTitle>
                  <CartSummaryContainerPrice>{`${cartTotal} TL`}</CartSummaryContainerPrice>
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
                  <Total>{`${cartTotal} TL`}</Total>
                  <BackToMainPageButtonContainer>
                    <BackToMainPageButton onClick={() => handleReturnMainPageButton()}>Ana Sayfaya Dön</BackToMainPageButton>
                  </BackToMainPageButtonContainer>
              </CartSummaryContainer>
        </PaymentSummaryContainer>
        ))}


      </div>



    </div>
  );
}
