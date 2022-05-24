import axios from "./axios-auth";

export const register = async (userRegisterModel) => {
    return await axios.post("/Users/Register", userRegisterModel);
}

export const login = async (userLoginModel) => {
    return await axios.post("/Users/Login", userLoginModel);
}
