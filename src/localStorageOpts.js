export const writeLocalStorage = (key, value) => {
    localStorage.setItem(key, JSON.stringify(value));
}
export const getFromLocalStorage = async (key) => {
    let result = localStorage.getItem(key);
    if (result !== null){
        return JSON.parse(localStorage.getItem(key));
    }
    return null;
}
export const removeFromLocalStorage = (key) => {
    localStorage.removeItem(key);
}

export const addToCart = async (cartItem) => {
    let cart = await getFromLocalStorage("cart");
    let cartNew = [];
    // initialize cart if nothing in localstorage1

    let changed = false;
    if (cart !== null){
        cartNew = cart?.map(cartItemList => {
            if(cartItemList.product.id === cartItem.product.id){
                cartItemList.amount += cartItem.amount;
                changed = true;
            }
            return cartItemList;
        });
    }
    
    if (!changed){
        cartNew.push(cartItem);
    }
    writeLocalStorage("cart", cartNew);
}

export const removeFromCart = async (productId, howMuch=1) => {
    let cart = await getFromLocalStorage("cart");
    if (cart !== null){
        cart = cart.filter(cartItem => {
            if (cartItem.product.id === productId){
                if (cartItem.amount - howMuch > 0){
                    cartItem.amount -= howMuch;
                    return cartItem;
                }
            }else{
                return cartItem;
            }
        });
        writeLocalStorage("cart", cart);
    }
}

export const getCartCount = async () => {
    let cart = await getFromLocalStorage("cart");
    return cart?.length;
}

export const EmptyCart = () => {
    removeFromLocalStorage("cart");
}