import _ from 'lodash';

//next 將請求傳遞給下一個處理程序 模塊化
const exampleMiddleware = async (req, res, next) => {
    console.log('test => middleware');
    next();
};

export { exampleMiddleware };
