import { useEffect, useState } from 'react';
import './MyBookings.css'; // Import your CSS file
import Table from 'react-bootstrap/Table';
import { format } from 'date-fns';
const MyBookings = () => {

    const [Bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);


    useEffect(() => {
        const fetchBookings = async () => {
            try {
                const response = await fetch('https://localhost:7267/api/Booking');
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                setBookings(data);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        fetchBookings();
    }, []);

    console.log(Bookings);
    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error}</p>;
    return (
        <div className="first">
            <h1>MyBookings</h1>
            
            <br /><br />
            <div className="table-responsive">
                <Table striped bordered hover variant="dark">
                    <thead>
                        <tr>
                            <th>Start Date</th>
                            <th>End Date</th>
                            <th>Number of people</th>
                            <th>Included Breakfast</th>
                            <th>Total Cost</th>
                            <th>Room Type</th>
                            <th>Hotel Name</th>
                            <th>Hotel Location</th>
                        </tr>
                    </thead>
                    <tbody>
                        {Bookings.map((booking) => (
                            <tr key={booking.Id}>
                                <td>{format(booking.startDate, "yyyy-MM-dd")}</td>
                                <td>{format(booking.endDate, "yyyy-MM-dd")}</td>
                                <td>{booking.personCount}</td>
                                <td>{booking.IncludeBreakfast ? 'No' : 'Yes'}</td>
                                <td>{booking.totalCost}</td>
                                <td>{booking.roomType}</td>
                                <td>{booking.hotel.hotelName}</td>
                                <td>{booking.hotel.location}</td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            </div>
        </div>

    );
};

export default MyBookings;
