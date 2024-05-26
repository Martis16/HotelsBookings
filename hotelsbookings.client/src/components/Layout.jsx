import { Outlet } from 'react-router-dom';
import NavBarComp from './NavBarComp';
import './Layout.css';

const Layout = () => {
    return (
        <div>
            <NavBarComp />
            <div className="background-image"> </div>  
                <div className="content" >
                    <Outlet />
                </div>
            
        </div>
    );
};

export default Layout;
