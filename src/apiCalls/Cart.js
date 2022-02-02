import axios from "./axios-auth";

export const userAddsProductToCart = async(cartItem) =>{
    let products = await axios.post("/Carts/user/addProduct", cartItem);
    return products;
}
export const getCartFromUserId = async (userId) =>{
    let cart = await axios.get(`/Carts/${userId}`);
    return cart;
}
export const changeOrDeleteCartWithUserId = async (changeCartItem) =>{
    let cart = await axios.post("/Carts/user/change", changeCartItem);
    return cart;
}