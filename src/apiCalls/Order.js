import axios from "./axios-auth";
import { getFromLocalStorage } from "../localStorageOpts";

let token = "";

const initToken = async () => {
    let result = await getFromLocalStorage("login");
    token = result.token;
}

export const getClientSecret = async (price) => {
    
    await initToken();

    let secret = await axios.get(`/Orders/Secret/${price}`, {headers: {"Authorization": `Bearer ${token}`}});
    return secret;
}

export const pay = async (model) => {
    await initToken();
    
    let result = await axios.post(`/Orders/Pay/`, model, {headers: {"Authorization": `Bearer ${token}`}});
    return result;
}

export const getOrders = async () => {
    await initToken();
    
    let result = await axios.get("/Orders/User/", {headers: {"Authorization": `Bearer ${token}`}});
    return result;
}