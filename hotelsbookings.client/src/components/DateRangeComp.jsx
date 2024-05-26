import { useState } from 'react';
import PropTypes from 'prop-types';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-daterangepicker/daterangepicker.css';
import { DateRange } from 'react-date-range';

const DateRangeComp = ({ onChange }) => {
    const [state, setState] = useState([
        {
            startDate: new Date(),
            endDate: new Date(),
            key: 'selection'
        }
    ]);

    const handleDateChange = (ranges) => {
        const formattedRanges = {
            startDate: ranges.selection.startDate.toISOString(),
            endDate: ranges.selection.endDate.toISOString()
        };
        setState([ranges.selection]);
        if (onChange) {
            onChange(formattedRanges);
        }
    };

    return (
        <div>
            <DateRange
                editableDateInputs={true}
                onChange={handleDateChange}
                moveRangeOnFirstSelection={false}
                ranges={state}
            />
        </div>
    );
};

DateRangeComp.propTypes = {
    onChange: PropTypes.func
};

export default DateRangeComp;
