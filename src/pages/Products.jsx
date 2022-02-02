import React, { useState, useEffect } from "react";
import styled from "styled-components";
import Announcement from "../components/Announcement";
import Navbar from "../components/Navbar";
import { Checkbox } from "@material-ui/core";
import ProductList from "../components/ProductList";
import { FormControl, InputLabel, Select, MenuItem } from "@material-ui/core";
import { useLocation } from "react-router-dom";
import {
  getProducts,
  getProductsByCategoryName,
  getProductsByCategoryNameAndQueries,
} from "../apiCalls/Product";
import { getBrands } from "../apiCalls/Brand";
import BlankProductPage from "../components/BlankProductPage";

const Container = styled.div``;
const ProductsContainer = styled.div`
  padding: 60px 100px;
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
  justify-content: space-between;
`;
const SideFilterPriceInput = styled.input`
  width: 40%;
  padding: 3px 8px;
  /* &::-webkit-outer-spin-button {
    -webkit-appearance: none;
  } */
  &::placeholder {
    //text-align: center;

  }
`;
const SideFilterPriceButton = styled.button`
  width: 30%;
  padding: 2px 0px;
  background-color: #f3f3f3;
  border: none;
  cursor: pointer;
`;
const SideFilterApplyButtonContainer = styled.div`
  background-color: transparent;
  padding: 24px 0px;
  display: flex;
  justify-content: flex-end;
`;
const SideFilterApplyButton = styled.button`
  padding: 10px 12px;
  border: 1px solid lightgray;
  font-family: inherit;
  font-size: bold;
  background-color: transparent;
  cursor: pointer;
  font-weight: 500;
  &:hover {
    transition: all 1s ease;
    background-color: lightgray;
  }
`;

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
  const location = useLocation();

  const [sortingFilter, setSortingFilter] = useState("");
  const [category, setCategory] = useState("");
  const [products, setProducts] = useState([]);
  const [brands, setBrands] = useState(null);
  const [filters, setFilters] = useState({checkedBrands: [], priceFilter1: 0, priceFilter2: 0});

  useEffect(() => {
    console.log("useEffect icinde");
    let pathName = location.pathname;
    pathName = pathName.split("/");
    setCategory(pathName[3]);
    if (pathName[2] === "category") {
      getProductsByCategoryNameAsync(pathName[3]);
    } else {
      getProductsAsync();
    }
  }, [location]);

  const getProductsAsync = async () => {
    const productsQuery = await getProducts();
    setProducts(productsQuery.data);
  };
  const handleBrandChange = (brand) => {
    console.log(brand);
    if (filters.checkedBrands?.includes(brand.id)){
      console.log("içinde");
      let newBrands = filters.checkedBrands.filter(x => x !== brand.id);
      setFilters({...filters, checkedBrands: newBrands});
    }else{
      console.log(filters);
      let arr = Array.from(filters.checkedBrands);
      arr.push(brand.id);
      setFilters({...filters, checkedBrands: arr});
    }
  };
  const handlePriceChange = (x, e) => {
    console.log(filters);
    if (x === "az"){
      setFilters({...filters, priceFilter1: e.target.value})
    }
    else{
      setFilters({...filters, priceFilter2: e.target.value})
    }
  }
  const getProductsByCategoryNameAsync = async (catName) => {
    const productsQuery = await getProductsByCategoryName(catName);
    setProducts(productsQuery.data);
    getBrandsAsync(productsQuery.data);
    console.log(products);
  };

  const getBrandsAsync = async (products) => {
    let allBrandIds = [];
    products.forEach(product => {
      if (!allBrandIds.includes(product.brandId)){
        allBrandIds.push(product.brandId);
      }
    });

    console.log("all brand ids: "+allBrandIds);

    let brandsQuery = await getBrands(allBrandIds);
    setBrands(brandsQuery.data);
  };

  const handleSortingFilterChange = (e) => {
    console.log("val: "+e.target.value);
    let value = e.target.value;
    switch (value) {
      case "asc":
        setProducts(products.sort((a, b) => parseFloat(a.price) - parseFloat(b.price)));
        break;
      case "desc":
        setProducts(products.sort((a, b) => parseFloat(b.price) - parseFloat(a.price)));
        break
      default:
        break;
    }
    setSortingFilter(e.target.value);
  };

  const applyFilters = async () => {
    let products = await getProductsByCategoryNameAndQueries(category, filters);
    console.log(products);
    setProducts(products.data);
  };
  return (
    <Container>
      <Navbar />
      <Announcement />

      <ProductsContainer>
        <SideFilterContainer>
          <SideFilterTitle>Seçili filtreler</SideFilterTitle>

          <SideFilterBrandContainer>
            <SideFilterBrandTitle>Markalar</SideFilterBrandTitle>
            {brands &&
              brands.map((brand) => {
                return (
                  <SideFilterBrand>
                    <Checkbox
                      onChange={() => handleBrandChange(brand)}
                      value={brand.name}
                      {...label}
                      size="small"
                    />
                    <SideFilterBrandName>{brand.name}</SideFilterBrandName>
                  </SideFilterBrand>
                );
              })}
          </SideFilterBrandContainer>

          <SideFilterPriceContainer>
            <SideFilterPriceTitle>Fiyat Aralığı</SideFilterPriceTitle>
            <SideFilterPriceInputContainer>
              <SideFilterPriceInput
                onChange={(e) => handlePriceChange("az", e)}
                placeholder="En Az"
                required
                type="number"
              />
              <SideFilterPriceInput
                onChange={(e) => handlePriceChange("cok", e)}
                placeholder="En Çok"
                required
                type="number"
              />
              {/* <SideFilterPriceButton>
                <Search style={{ width: 20, height: 20 }} />
              </SideFilterPriceButton> */}

            </SideFilterPriceInputContainer>
          </SideFilterPriceContainer>

          <SideFilterApplyButtonContainer>
            <SideFilterApplyButton onClick={() => applyFilters()}>
              Filtreleri Uygula
            </SideFilterApplyButton>
          </SideFilterApplyButtonContainer>
        </SideFilterContainer>

        <MainContainer>
          <SortingFilterContainer>
            {/* <SortingFilter>a</SortingFilter> */}
            <FormControl style={{ width: "20%" }}>
              <InputLabel id="demo-simple-select-label">Sıralama</InputLabel>
              <Select
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={sortingFilter}
                label="Age"
                onChange={handleSortingFilterChange}
              >
                <MenuItem value={"normal"}>Varsayılan</MenuItem>
                <MenuItem value={"asc"}>Artan Fiyat</MenuItem>
                <MenuItem value={"desc"}>Azalan Fiyat</MenuItem>
              </Select>
            </FormControl>
          </SortingFilterContainer>
          {products[0] ? <ProductList products={products} /> : <BlankProductPage/>}
        </MainContainer>
      </ProductsContainer>
    </Container>
  );
}
