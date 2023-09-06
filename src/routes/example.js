import { exampleMiddleware } from '../middlewares/exampleMiddleware';
import exampleController from '../http/Controller/exampleController';

const api = {
    namespace: 'example',
    routes: [{
            RestFulMethod: 'get',
            path: '/',
            method: exampleController.Get,
            middlewareMethod: [exampleMiddleware],
        },
        {
            RestFulMethod: 'get',
            path: '/query',
            method: exampleController.GetQuery,
            middlewareMethod: [exampleMiddleware],
        },
        {
            RestFulMethod: 'get',
            path: '/alldata',
            method: exampleController.Alldata,
            middlewareMethod: [exampleMiddleware],
        },
        {
            RestFulMethod: 'post',
            path: '/create',
            method: exampleController.Create,
            middlewareMethod: [exampleMiddleware],
        },
        {
            RestFulMethod: 'patch',
            path: '/edit',
            method: exampleController.Edit,
            middlewareMethod: [exampleMiddleware],
        },
        {
            RestFulMethod: 'delete',
            path: '/delete',
            method: exampleController.Delete,
            middlewareMethod: [exampleMiddleware],
        },
    ],
};


//導出
export default api;