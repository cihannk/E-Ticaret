import React from 'react'
import styled from 'styled-components';
import Product from './Product';

const Container = styled.div`
    display: flex;
    flex-wrap: wrap;
    padding: 20px;
`

export default function ProductList({products}) {
    return (
        <Container>
            {/*productPageProducts*/products.map(product => <Product title={product.title} img={product.imageUrl} price={product.price} key={product.id} id={product.id}/>)}
        </Container>
    )
}
