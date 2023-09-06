import fs from 'fs';
import path from 'path';
import _ from 'lodash';
import { Router } from 'express';
// import fileUploadController from './../http/controllers/fileUploadController';

class routeProvider {
    constructor(expressApp) {
        this._router = expressApp.Router;
    }

    bind = async() => {
        return await this.bindRouter();
    };

    bindRouter = async() => {
        const router = Router();
        const routerPath = path.resolve(__dirname, './../routes');

        fs.readdirSync(routerPath).forEach(async(file) => {
            const api = await
            import (`${routerPath}/${path.basename(file)}`);
            _.forEach(api.default.routes, (routesItem) => {
                switch (routesItem.RestFulMethod) {
                    case 'get':
                        router.get(
                            `/${api.default.namespace}${routesItem.path}`,
                            routesItem.middlewareMethod ?
                            _.map(routesItem.middlewareMethod, (value) =>
                                this.isAsync(value) ? this.parseAsyncFunctionToMiddleware(value) : value,
                            ) :
                            this.parseAsyncFunctionToMiddleware(),
                            routesItem.method,
                        );
                        break;
                    case 'post':
                        router.post(
                            `/${api.default.namespace}${routesItem.path}`,
                            routesItem.middlewareMethod ?
                            _.map(routesItem.middlewareMethod, (value) =>
                                this.isAsync(value) ? this.parseAsyncFunctionToMiddleware(value) : value,
                            ) :
                            this.parseAsyncFunctionToMiddleware(),
                            routesItem.method,
                        );
                        break;
                    case 'put':
                        router.put(
                            `/${api.default.namespace}${routesItem.path}`,
                            routesItem.middlewareMethod ?
                            _.map(routesItem.middlewareMethod, (value) =>
                                this.isAsync(value) ? this.parseAsyncFunctionToMiddleware(value) : value,
                            ) :
                            this.parseAsyncFunctionToMiddleware(),
                            routesItem.method,
                        );
                        break;
                    case 'patch':
                        router.patch(
                            `/${api.default.namespace}${routesItem.path}`,
                            routesItem.middlewareMethod ?
                            _.map(routesItem.middlewareMethod, (value) =>
                                this.isAsync(value) ? this.parseAsyncFunctionToMiddleware(value) : value,
                            ) :
                            this.parseAsyncFunctionToMiddleware(),
                            routesItem.method,
                        );
                        break;
                    case 'delete':
                        router.delete(
                            `/${api.default.namespace}${routesItem.path}`,
                            routesItem.middlewareMethod ?
                            _.map(routesItem.middlewareMethod, (value) =>
                                this.isAsync(value) ? this.parseAsyncFunctionToMiddleware(value) : value,
                            ) :
                            this.parseAsyncFunctionToMiddleware(null),
                            routesItem.method,
                        );
                        break;
                }
            });
        });

        return router;
    };

    parseAsyncFunctionToMiddleware = (fn) => (req, res, next) => {
        if (fn) {
            Promise.resolve(fn(req, res, next)).catch(next);
        } else {
            next();
        }
    };

    isAsync(obj) {
        return obj.constructor.name === 'AsyncFunction';
    }
}

export default routeProvider;