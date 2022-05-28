import {useStripe, useElements, PaymentElement} from '@stripe/react-stripe-js';
import { useState } from 'react';
import { pay } from '../apiCalls/Order';
import styled from "styled-components";

const SubmitButton = styled.button`
  background-color: transparent;
    border: 1px solid lightgray;
    padding: 12px;
    margin-top: 12px;
    font-family: inherit;
    font-weight: 600;
    &:hover{
        background-color: lightgray;
    }

`

export default function CheckoutForm({intentId, cart, setPaymentComplete}) {
  const stripe = useStripe();
  const elements = useElements();

  const [errorMessage, setErrorMessage] = useState(null);

  const handlePayAsync = async () => {
    var cartItems = cart.map(cartItem => {
      return {
        productId: cartItem.product.id,
        amount: cartItem.amount,
        unitPrice: cartItem.product.price
      }
    })

    let model = {
      intentId: intentId,
      cartItems: cartItems
    };

    try{
      await pay(model);
      setPaymentComplete(true);
    }
    catch(err){
      setErrorMessage(err.message);
    }
    
  }

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!stripe || !elements) {
      return;
    }

    const {error} = await stripe.confirmPayment({
      elements,
      confirmParams: {
        return_url: 'http://localhost:3000/',
      },
      redirect: 'if_required'

    });


    if (error) {
      setErrorMessage(error.message);
    } else {
      handlePayAsync();
    }
  };
  return (
    <form onSubmit={handleSubmit}>
      <PaymentElement />
      <SubmitButton disabled={!stripe}>Gönder</SubmitButton>
      {/* <button disabled={!stripe}>Submit</button> */}
      {/* Show error message to your customers */}
      {errorMessage && <div>{errorMessage}</div>}
    </form>
  )
}
