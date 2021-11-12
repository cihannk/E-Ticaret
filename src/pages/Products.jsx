import React, { useState } from "react";
import styled from "styled-components";
import Announcement from "../components/Announcement";
import Navbar from "../components/Navbar";
import { Checkbox } from "@material-ui/core";
import Search from "@material-ui/icons/Search";
import ProductList from "../components/ProductList";
import { FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";

const Container = styled.div``;
const ProductsContainer = styled.div`
  padding: 100px;
  display: flex;
`;
const SideFilterContainer = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
  //align-items: center;
`;
const SideFilterTitle = styled.span`
  font-weight: 600;
  font-size: 20px;
`;
const SideFilterBrandContainer = styled.div`
  display: flex;
  flex-direction: column;
  padding: 12px 0px;
`;
const SideFilterBrandTitle = styled.span`
  font-weight: 600;
`;
const SideFilterBrand = styled.div`
  display: flex;
  align-items: center;
  &:hover {
    background-color: #e6e3e3;
  }
`;
const SideFilterBrandName = styled.span``;

const SideFilterPriceContainer = styled.div`
  display: flex;
  flex-direction: column;
`;
const SideFilterPriceTitle = styled.span`
  font-weight: 600;
  padding: 12px 0px;
`;
const SideFilterPriceInputContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-around;
`;
const SideFilterPriceInput = styled.input`
  width: 30%;
  padding: 3px 0px;
  &::placeholder {
    text-align: center;
  }
`;
const SideFilterPriceButton = styled.button`
  width: 30%;
  padding: 2px 0px;
  background-color: #f3f3f3;
  border: none;
  cursor: pointer;
`;

const SideFilterColor = styled.div``;

const SortingFilterContainer = styled.div`
  width: 100%;
  display: flex;
  justify-content: flex-end;
  //margin-left: 100px;
`;
const SortingFilter = styled.div`
  width: 50px;
  height: 50px;
  background-color: red;
  margin-right: 22px;
`;
const MainContainer = styled.div`
  flex: 5;
`;

const label = { inputProps: { "aria-label": "Checkbox demo" } };

export default function Products() {
const [sortingFilter, setSortingFilter] = useState("");
const handleChange = (e) =>{
    setSortingFilter(e.target.value);
} 
  return (
    <Container>
      <Navbar />
      <Announcement />

      <ProductsContainer>
        <SideFilterContainer>
          <SideFilterTitle>Seçili filtreler</SideFilterTitle>

          <SideFilterBrandContainer>
            <SideFilterBrandTitle>Markalar</SideFilterBrandTitle>
            <SideFilterBrand>
              <Checkbox {...label} size="small" />
              <SideFilterBrandName>Samsung</SideFilterBrandName>
            </SideFilterBrand>
            <SideFilterBrand>
              <Checkbox {...label} size="small" />
              <SideFilterBrandName>Apple</SideFilterBrandName>
            </SideFilterBrand>
            <SideFilterBrand>
              <Checkbox {...label} size="small" />
              <SideFilterBrandName>Xiaomi</SideFilterBrandName>
            </SideFilterBrand>
          </SideFilterBrandContainer>

          <SideFilterPriceContainer>
            <SideFilterPriceTitle>Fiyat Aralığı</SideFilterPriceTitle>
            <SideFilterPriceInputContainer>
              <SideFilterPriceInput placeholder="En Az" />
              <SideFilterPriceInput placeholder="En Çok" />
              <SideFilterPriceButton>
                <Search style={{ width: 20, height: 20 }} />
              </SideFilterPriceButton>
            </SideFilterPriceInputContainer>
          </SideFilterPriceContainer>
        </SideFilterContainer>
        <MainContainer>
          <SortingFilterContainer>
            {/* <SortingFilter>a</SortingFilter> */}
            <FormControl style={{width:"20%"}}>
              <InputLabel id="demo-simple-select-label">Sıralama</InputLabel>
              <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={sortingFilter}
                label="Age"
                onChange={handleChange}
              >
                <MenuItem value={10}>Varsayılan</MenuItem>
                <MenuItem value={20}>Artan Fiyat</MenuItem>
                <MenuItem value={30}>Azalan Fiyat</MenuItem>
              </Select>
            </FormControl>
          </SortingFilterContainer>
          <ProductList />
        </MainContainer>
      </ProductsContainer>
    </Container>
  );
}
