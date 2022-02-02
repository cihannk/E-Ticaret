import axios from "./axios-auth";

export const getBrands = async (brandIds) => {
    let mainPageCategories = await axios.get("/Brands");
    let mainPageCategoriesNewData = mainPageCategories.data.filter(category =>{
        if (brandIds.includes(category.id)){
            return category;
        }
    })
    console.log("mainPageCategoriesNewData: "+mainPageCategoriesNewData);
    mainPageCategories.data = mainPageCategoriesNewData;
    return mainPageCategories;
}