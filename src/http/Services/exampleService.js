import exampleRepository from '../repositories/exampleRepository';
import moment from 'moment';
import _ from 'lodash';

class exampleService {
    Get = async() => {
        try {
            const result = await exampleRepository.Get();
            // (!result)= 1 0
            if (!result) {
                return {
                    status: 404,
                    data: null,
                };
            } else if (typeof result === 'string') {  //===會同時比較值和類型
                return {
                    status: 500,
                    message: result,
                };
            } else if (typeof result === 'object') {
                return {
                    status: 200,
                    data: result,
                };
            }
        } catch (err) {
            return {
                status: 500,
                message: err.message,
            };
        }
    };

    Alldata = async() => {
        try {
            const result = await exampleRepository.Alldata();
            if (!result) {
                return {
                    status: 404,
                    data: null,
                };
            } else if (typeof result === 'string') {
                return {
                    status: 500,
                    message: result,
                };
            } else if (typeof result === 'object') {
                return {
                    status: 200,
                    data: result,
                };
            }
        } catch (err) {
            return {
                status: 500,
                message: err.message,
            };
        }
    };

    GetQuery = async(num, str) => {
        try {
            const result = await exampleRepository.GetQuery(num, str);

            if (!result) {
                return {
                    status: 404,
                    data: null,
                };
            } else if (typeof result === 'string') {
                return {
                    status: 500,
                    message: result,
                };
            } else if (typeof result === 'object') {
                return {
                    status: 200,
                    data: result,
                };
            }
        } catch (err) {
            return {
                status: 500,
                message: err.message,
            };
        }
    };

    Create = async(name, content) => {
        try {
            const result = await exampleRepository.Create(name, content);
            if (!result) {
                return {
                    status: 404,
                    data: null,
                };
            } else if (typeof result === 'string') {
                return {
                    status: 500,
                    message: result,
                };
            } else if (typeof result === 'boolean') {
                return {
                    status: 200,
                    data: result,
                };
            }
        } catch (err) {
            return {
                status: 500,
                message: err.message,
            };
        }
    };

    Edit = async(id, name, content) => {
        try {
            const result = await exampleRepository.Edit(id, name, content);
            if (!result) {
                return {
                    status: 404,
                    data: null,
                };
            } else if (typeof result === 'string') {
                return {
                    status: 500,
                    message: result,
                };
            } else if (typeof result === 'boolean') {
                return {
                    status: 200,
                    data: result,
                };
            }
        } catch (err) {
            return {
                status: 500,
                message: err.message,
            };
        }
    };
    Delete = async(id) => {
        try {
            const result = await exampleRepository.Delete(id);

            if (!result) {
                return {
                    status: 404,
                    data: null,
                };
            } else if (typeof result === 'string') {
                return {
                    status: 500,
                    message: result,
                };
            } else if (typeof result === 'boolean') {
                return {
                    status: 200,
                    data: result,
                };
            }
        } catch (err) {
            return {
                status: 500,
                message: err.message,
            };
        }
    };
}

export default new exampleService();