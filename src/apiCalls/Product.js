import axios from "./axios-auth";

export const getProduct = async (productId) => {
    let products = await axios.get("/Products/" + productId);
    return products;
}
export const getProductsByCategoryName = async (catName) => {
    let products = await axios.get("/Products/category/" + catName);
    return products;
}
export const getProductsByCategoryNameAndQueries = async (catName="",filters) => {
    let query = `/Products/category/${catName}?brands=`
    if (filters.checkedBrands){
        filters.checkedBrands.forEach(value => {
            query+= value+"," ;
        });
        query = query.slice(0, query.length-1);
    }
    if (filters.priceFilter1 && filters.priceFilter2){
        query = query + `&price=` + filters.priceFilter1 + "-" + filters.priceFilter2;
    }
    
    console.log("query: "+query);

    let products = await axios.get(query);
    return products;
}
export const getProducts = async () => {
    let products = await axios.get("/Products");
    return products;
}
