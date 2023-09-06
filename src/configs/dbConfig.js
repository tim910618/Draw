import dotenv from 'dotenv';
const { DB_HOST, DB_PORT, DB_DATABASE, DB_USERNAME, DB_PASSWORD } = dotenv.config().parsed;
const dbConfig = {
    host: DB_HOST,
    user: DB_USERNAME,
    port: DB_PORT,
    password: DB_PASSWORD,
    database: DB_DATABASE,
    ssl: false,
};

export default dbConfig;