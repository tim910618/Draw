import dotenv from 'dotenv';

const { PREDICT_ROUTE } = dotenv.config().parsed;

const scheduleConfig = {
    predictRout: PREDICT_ROUTE,
};

export default scheduleConfig;
