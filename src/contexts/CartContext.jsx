import { useContext, createContext, useState, useEffect } from "react";
import jwtDecode from "jwt-decode";
import {getFromLocalStorage, removeFromLocalStorage} from "../localStorageOpts";

const UserContext = createContext();

export const UserProvider = ({children}) => {
    const [claims, setClaims] = useState(null);
    const [cartCount, setCartCount] = useState(null);

    const decodeJwt = async (path) => {
        let tokenObj = await getFromLocalStorage(path);
        if (tokenObj !== null){
            let date = new Date();
            let decoded = jwtDecode(tokenObj.token);
            if(decoded.exp * 1000 < date.getTime()){
                // token expired
                removeFromLocalStorage("login");
                return null;
            }
            return decoded;
        }
        return null;
    }

    const setClaimsAsync = async () => {
        let claims = await decodeJwt("login");
        console.log("cartContext setClaimsAsync", claims);
        setClaims(claims);
    }

    useEffect(()=> {
        console.log("cartContext useeffect[]")
        setClaimsAsync();
    },[])

    const states = {
        claims: claims,
        setClaims: setClaimsAsync,
        setCartCount: setCartCount,
        cartCount: cartCount
    }

    return <UserContext.Provider value={states}>
        {children}
    </UserContext.Provider>
}

export const useUser = () => {
    return useContext(UserContext);
}
