import axios from "./axios-auth";

export const getSliders = async() =>{
    let products = await axios.get("/Sliders");
    return products;
}