import axios from "./axios-auth";
import {getFromLocalStorage} from "../localStorageOpts";

let token = "";

const initToken = async () => {
    let result = await getFromLocalStorage("login");
    token = result.token;
}

export const register = async (userRegisterModel) => {
    return await axios.post("/Users/Register", userRegisterModel);
}

export const login = async (userLoginModel) => {
    return await axios.post("/Users/Login", userLoginModel);
}

export const profile = async (userId) => {
    await initToken();
    return await axios.get(`/Users/Profile/${userId}`, {headers: {"Authorization": `Bearer ${token}`}})
}
