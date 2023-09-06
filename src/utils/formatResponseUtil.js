import _ from 'lodash';

class formatResponseUtil {
    formatResponse = (res, data) => {
        if (data.status === 200) {
            return res.status(200).json({
                status: data.status,
                message: 'susses',
                success: true,
                data: data.data,
            });
        } else if (data.status === 404) {
            return res.status(404).json({
                status: data.status,
                message: 'Not Found',
                success: false,
                data: null,
            });
        } else if (data.status === 500) {
            return res.status(500).json({
                status: data.status,
                message: data.message,
                success: false,
                data: null,
            });
        }
    };

    keyErrorResponse = (res, message) => {
        return res.status(400).json({
            status: 400,
            message: message,
        });
    };
}

export default new formatResponseUtil();