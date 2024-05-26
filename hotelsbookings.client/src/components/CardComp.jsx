
import Card from 'react-bootstrap/Card';
import PropTypes from 'prop-types';
import ModalComp from './ModalComp.jsx';

function CardComp({ ImageSource, HotelName, Location, hotelId }) {


    return (
        <>
            <Card style={{ width: '18rem', height: '23rem' }}>
                <Card.Img variant="top" src={ImageSource} style={{ height: '14rem', objectFit: 'cover' }} />
                <Card.Body>
                    <Card.Title>{HotelName}</Card.Title>
                    <Card.Text>
                        {Location}
                    </Card.Text>
                    <ModalComp Name={HotelName} HotelID={hotelId}/>
                </Card.Body>
            </Card>
            
        </>
    );
}

CardComp.propTypes = {
    ImageSource: PropTypes.string,
    HotelName: PropTypes.string,
    Location: PropTypes.string,
    hotelId: PropTypes.number,
};

export default CardComp;
