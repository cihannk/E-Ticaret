import React, {useState, useEffect} from 'react'
import styled from 'styled-components'
import ProductCategoryItem from './ProductCategoryItem'
import { getMainPageCategories } from '../apiCalls/Category'

const Container = styled.div`
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 24px;
    margin-top: 24px;
`

export default function ProductCategories() {
    const [mainPageCategories, setMainPageCategories] = useState(null);
    useEffect(() => {
        if (mainPageCategories === null){
            getMainPageCategoriesAsync();
        }
    }, [mainPageCategories])

    const getMainPageCategoriesAsync = async () => {
        const mainPageCategories = await getMainPageCategories();
        setMainPageCategories(mainPageCategories.data);
    }
    return (
        <Container>
            {mainPageCategories && mainPageCategories.map(item => <ProductCategoryItem key={item.id} title={item.displayName} img={item.imageUrl} pathName={item.pathName}/>)}
        </Container>
    )
}
