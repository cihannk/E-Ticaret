import React from "react";
import styled from "styled-components";
import { Link } from "react-router-dom";

const ProductCategory = styled.div`
  height: 60vh;
  display: flex;
  flex: 1;
  max-width: 33.3%;
  position: relative;
  justify-content: center;
  padding: 5px;
`;
const ProductCategoryImg = styled.img`
  height: 100%;
  width: 100%;
  object-fit: cover;
`;
const ProductCategoryInfo = styled.div`
  position: absolute;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 100%;
  height: 100%;
`;
const ProductCategoryTitle = styled.h2`
  color: white;
`;
const ProductCategoryButton = styled.button`
  background-color: white;
  padding: 10px 15px;
  border: 2px solid black;
  font-family: inherit;
  font-weight: bold;
  &:hover {
    background-color: lightgray;
    transition: all 0.5s ease;
  }
`;

export default function ProductCategoryItem({title, img, pathName}) {
  return (
    <ProductCategory>
      <ProductCategoryImg src={img} />
      <ProductCategoryInfo>
        <ProductCategoryTitle>{title}</ProductCategoryTitle>
        <Link to={`/products/category/${pathName}`}>
        <ProductCategoryButton>Satın Al</ProductCategoryButton>
        </Link>
      </ProductCategoryInfo>
    </ProductCategory>
  );
}
