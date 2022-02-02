import React from 'react'
import styled from 'styled-components';
import Product from './Product';
import { productPageProducts } from '../fakeData';

const Container = styled.div`
    display: flex;
    flex-wrap: wrap;
    padding: 20px;
    justify-content: space-between;
`

export default function ProductList({products}) {
    console.log("pl");
    console.log(products);
    return (
        <Container>
            {/*productPageProducts*/products.map(product => <Product title={product.title} img={product.imageUrl} price={product.price} key={product.id} id={product.id}/>)}
        </Container>
    )
}
