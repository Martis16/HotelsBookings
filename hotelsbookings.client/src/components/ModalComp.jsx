import { useState } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import PropTypes from 'prop-types';
import 'react-date-range/dist/styles.css'; 
import 'react-date-range/dist/theme/default.css'; 
import DateRangeComp from './DateRangeComp';
import './ModalComp.css'; 


const ModalComp = ({ Name, HotelID }) => {
    const [show, setShow] = useState(false);
    const [IncludeBreakfast, setIncludeBreakfast] = useState(false);
    const [PersonCount, setNumPeople] = useState(1);
    const [RoomType, setRoomType] = useState('Deluxe');
    const [startDate, setstartDate] = useState(new Date());
    const [endDate, setendDate] = useState(new Date());

    const handleDateChange = (selectedRange) => {
        setstartDate(selectedRange.startDate);
        setendDate(selectedRange.endDate);
    };

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const handleBreakfastChange = () => {
        setIncludeBreakfast(!IncludeBreakfast);
    };

    const handleNumPeopleChange = (e) => {
        setNumPeople(parseInt(e.target.value));
    };

    const handleRoomTypeChange = (e) => {
        setRoomType(e.target.value);
    };

    const handleBooking = async () => {
        try {
            const formData = {
                StartDate: startDate,
                EndDate: endDate, 
                IncludeBreakfast,
                PersonCount,
                RoomType,

            };
            const start = new Date(startDate);
            const end = new Date(endDate);

            if (start.toDateString() === end.toDateString()) {
                alert("Must be at least one night");
                return;
            }
            console.log(formData);
            const response = await fetch(`https://localhost:7267/api/Booking/${HotelID}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData),
            });

            if (!response.ok) {
                throw new Error('Failed to create booking');
            }
            alert("Booking was successfull");
            handleClose();

        } catch (error) {
            console.error('Error creating booking:', error);
        }
    };


    return (
        <>
            <Button variant="outline-info" onClick={handleShow}>
                Book this hotel
            </Button>

            <Modal show={show} onHide={handleClose} centered className="my-modal">
                <Modal.Header closeButton>
                    <Modal.Title>{Name}</Modal.Title>
                </Modal.Header>
                <Modal.Body className="modal-content">
                    <Form>
                        <h5>Choose the date</h5>
                        <DateRangeComp onChange={handleDateChange} />
                        <br></br>
                        <Form.Group controlId="includeBreakfast">
                            <Form.Check
                                type="checkbox"
                                label="Include Breakfast"
                                checked={IncludeBreakfast}
                                onChange={handleBreakfastChange}
                            />
                        </Form.Group>
                        <br></br>
                        <Form.Group controlId="numPeople">
                            <Form.Label>Number of People</Form.Label>
                            <Form.Control
                                as="select"
                                value={PersonCount}
                                onChange={handleNumPeopleChange}
                            >
                                {[1, 2, 3, 4, 5, 6].map((num) => (
                                    <option key={num} value={num}>
                                        {num}
                                    </option>
                                ))}
                            </Form.Control>
                        </Form.Group>
                        <br></br>
                        <Form.Group controlId="roomType">
                            <Form.Label>Room Type</Form.Label>
                            <Form.Control
                                as="select"
                                value={RoomType}
                                onChange={handleRoomTypeChange}
                            >
                                {['Standard', 'Deluxe', 'Suite'].map((type) => (
                                    <option key={type} value={type}>
                                        {type}
                                    </option>
                                ))}
                            </Form.Control>
                            <br></br>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={handleBooking}>
                        Book now
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};

ModalComp.propTypes = {
    Name: PropTypes.string,
    HotelID: PropTypes.number,
};

export default ModalComp;
