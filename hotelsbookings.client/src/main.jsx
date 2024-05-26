import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import {createBrowserRouter,RouterProvider,} from "react-router-dom";
import ErrorPage from "./error-page";
import App from './Pages/App';
import MyBookings from './Pages/MyBookings';
import 'bootstrap/dist/css/bootstrap.min.css';
import Layout from './components/Layout';

const router = createBrowserRouter([
    {
        path: "/",
        element: <Layout />,
        errorElement: <ErrorPage />,
        children: [
            {
                path: "/",
                element: <App />,
            },
            {
                path: "MyBookings",
                element: <MyBookings />,
            }
        ]
    }
]);

ReactDOM.createRoot(document.getElementById('root')).render(

    <React.StrictMode>
        <RouterProvider router={router} />
  </React.StrictMode>,
)

