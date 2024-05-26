import { useEffect, useState } from 'react';
import './App.css';
import CardComp from '../components/CardComp'
import {Form} from 'react-bootstrap';

const App = () => {
    const [hotels, setHotels] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [searchQuery, setSearchQuery] = useState('');
    const columnsPerRow = 4;



    useEffect(() => {
        const fetchHotels = async () => {
            try {
                const response = await fetch('https://localhost:7267/api/Hotel');
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                setHotels(data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchHotels();
    }, []);




    const filteredHotels = hotels.filter(hotel =>
        hotel.location.toLowerCase().includes(searchQuery.toLowerCase())
    );

    const placeholdersNeeded = (columnsPerRow - (filteredHotels.length % columnsPerRow)) % columnsPerRow;

    const placeholders = Array(placeholdersNeeded).fill({ placeholder: true });


    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error}</p>;




    return (
        <div className="app-container" data-bs-theme="dark">
            <div className="container">
                <br /><br /><br /><br />
                <Form style={{ marginRight: "5px", marginLeft: "5px" }}>
                    <Form.Group controlId="formSearch">
                        <Form.Control
                            type="text"
                            placeholder="Search by location"
                            value={searchQuery}
                            onChange={e => setSearchQuery(e.target.value)}
                        />
                    </Form.Group>
                </Form>
                <br /><br /><br /><br />
                <div className="row hotel-row">
                    {[...filteredHotels, ...placeholders].map((hotel, index) => (
                        <div className="col hotel-col" key={hotel.hotelId || `placeholder-${index}`}>
                            {!hotel.placeholder ? (
                                <CardComp
                                    ImageSource={hotel.image}
                                    HotelName={hotel.hotelName}
                                    Location={hotel.location}
                                    hotelId={hotel.id}
                                />
                            ) : (
                                <div className="card-placeholder"></div>
                            )}
                            <br /><br />
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default App;
