import { Component } from 'react';
import { Container, Nav, Navbar} from 'react-bootstrap';


export default class NavBarComp extends Component {
    render() {
        return (
            <Navbar expand="lg" className="bg-dark" variant="dark" fixed="top">
                <Container>
                    <Navbar.Brand href="/">Hotels</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto">
                            <Nav.Link href="/MyBookings">MyBookings</Nav.Link>
                        </Nav>
                    </Navbar.Collapse>

                </Container>
            </Navbar>
        );
    }
}
