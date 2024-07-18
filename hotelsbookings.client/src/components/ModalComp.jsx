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
    const [errorMessage, setErrorMessage] = useState('');

    const handleDateChange = (selectedRange) => {
        setstartDate(selectedRange.startDate);
        setendDate(selectedRange.endDate);
    };

    const handleClose = () => {
        setShow(false);
        setErrorMessage(''); // Clear error messages when modal is closed
    };
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
                setErrorMessage("Must be at least one night");
                return;
            }

            if (PersonCount <= 0) {
                setErrorMessage("PersonCount must be a positive number.");
                return;
            }

            console.log(formData);
            const response = await fetch(`${import.meta.env.VITE_API_URL_BOOKING}/${HotelID}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(formData),
            });

            if (!response.ok) {
                const errorData = await response.json();
                throw new Error(errorData.message || 'Failed to create booking');
            }

            alert("Booking was successful");
            handleClose();
        } catch (error) {
            console.error('Error creating booking:', error);
            setErrorMessage(error.message);
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
                    {errorMessage && (
                        <div className="alert alert-danger" role="alert">
                            {errorMessage}
                        </div>
                    )}
                    <Form>
                        <h5>Choose the date</h5>
                        <DateRangeComp onChange={handleDateChange} />
                        <br />
                        <Form.Group controlId="includeBreakfast">
                            <Form.Check
                                type="checkbox"
                                label="Include Breakfast"
                                checked={IncludeBreakfast}
                                onChange={handleBreakfastChange}
                            />
                        </Form.Group>
                        <br />
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
                        <br />
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
                            <br />
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
