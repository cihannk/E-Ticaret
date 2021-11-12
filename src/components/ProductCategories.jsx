import React from 'react'
import styled from 'styled-components'
import { productCatData } from '../fakeData'
import ProductCategoryItem from './ProductCategoryItem'

const Container = styled.div`
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 24px;
    margin-top: 24px;
`

export default function ProductCategories() {
    return (
        <Container>
            {productCatData.map(item => <ProductCategoryItem key={item.id} title={item.title} img={item.img}/>)}
        </Container>
    )
}
