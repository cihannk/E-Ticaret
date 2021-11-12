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

export default function ProductList() {
    return (
        <Container>
            {productPageProducts.map(product => <Product title={product.title} img={product.img} price={product.price} key={product.id}/>)}
        </Container>
    )
}
