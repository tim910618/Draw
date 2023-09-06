import _ from 'lodash';
import pool from './../services/dbService';

class dbWrapper {
    queryResult = async(sql, value) => {
        try {
            return await new Promise((resolve) => {
              console.log('SQL:', sql);
              //console.log('Values:', value);
                pool.query(sql, value, (err, result) => {
                    if (err) resolve(err);
                    //console.log('Query Result:', result);
                    resolve(result.rows);
                });
            });
        } catch (error) {
            return error;
        }
    };
    query = (sql, value) => {
        try {
            return new Promise((resolve) => {
              console.log('SQL:', sql);
                pool.query(sql, value, (err, result) => {
                    if (err) resolve(err);
                    resolve(true);
                });
            });
        } catch (error) {
            return error;
        }
    };
}

export default new dbWrapper();