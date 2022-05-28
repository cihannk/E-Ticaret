import { lazy, Suspense, useState } from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect,
} from "react-router-dom";
import { UserProvider } from "./contexts/CartContext";
import Profile from "./pages/Profile";

const Home = lazy(() => import("./pages/Home"));
const Product = lazy(() => import("./pages/Product"));
const Products = lazy(() => import("./pages/Products"));
const Login = lazy(() => import("./pages/Login"));
const Register = lazy(() => import("./pages/Register"));
const Cart = lazy(() => import("./pages/Cart"));

function App() {
  const [loggedIn, setLoggedIn] = useState(false);
  return (
    <Router>
      <UserProvider>
        <Switch>
          <Route path="/login" exact>
            {loggedIn ? (
              <Redirect to="/" />
            ) : (
              <Suspense fallback={<span>loading...</span>}>
                <Login />
              </Suspense>
            )}
          </Route>
          <Route path="/register" exact>
          {loggedIn ? (
              <Redirect to="/" />
            ) : (
              <Suspense fallback={<span>loading...</span>}>
                <Register />
              </Suspense>
            )}
          </Route>
          <Route path="/" exact>
            <Suspense fallback={<span>loading...</span>}>
              <Home />
            </Suspense>
          </Route>
          <Route path="/product/:id" exact>
            <Suspense fallback={<span>loading...</span>}>
              <Product />
            </Suspense>
          </Route>
          <Route path="/products" >
            <Suspense fallback={<span>loading...</span>}>
              <Products />
            </Suspense>
          </Route>
          <Route path="/cart" exact>
            <Suspense fallback={<span>loading...</span>}>
              <Cart />
            </Suspense>
          </Route>
          <Route path="/profile" exact>
            <Suspense fallback={<span>loading...</span>}>
              <Profile />
            </Suspense>
          </Route>
        </Switch>
      </UserProvider>
      
    </Router>
  );
}

export default App;
