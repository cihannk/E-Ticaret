import axios from "./axios-auth";

export const getMainPageCategories = async () => {
    let mainPageCategories = await axios.get("/Categories/mainpage");
    return mainPageCategories;
}