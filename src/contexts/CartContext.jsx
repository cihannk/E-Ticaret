import { useContext, createContext, useState, useEffect } from "react";
import jwtDecode from "jwt-decode";

const UserContext = createContext();

export const UserProvider = ({children}) => {
    const [cart, setCart] = useState([
        // {
        //     productId: 0,
        //     amount: 0,
        //     unitPrice: 0
        // }
    ]);
    const [claims, setClaims] = useState(
        null
        // {
        // id: 0,
        // email: "",
        // role: ""
        // }
    );

    // Get cart data and claims from localstorage

    useEffect(()=> {
        setClaims()
    },[])

    const states = {
        claims: this.claims,
        cart: this.cart, 
        setCart: this.setCart        
    }

    return <UserContext.Provider value={states}>
        {children}
    </UserContext.Provider>
}

export const useUser = () => {
    return useContext(UserContext);
}

const decodeJwt = (token) => {
    
    jwtDecode()
}