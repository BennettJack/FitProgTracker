import LoginSignup from "./Pages/Account/LoginSignup";
import {createBrowserRouter} from "react-router-dom"
import App from "./App";



export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children:[
            {path: "loginSignup", element: <LoginSignup/>},
            
        ]
    },
]);
