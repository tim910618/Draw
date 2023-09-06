import cookieParser from 'cookie-parser';
import express from 'express';
import httpErrors from 'http-errors';
import logger from 'morgan';
import path from 'path';
import RouterProvider from './providers/routerProvider';
import corsProvider from './providers/corsProvider';
import scheduleProvider from './providers/scheduleProvider';

class App {
    constructor() {
        this._app = express();
        this._routerProvider = new RouterProvider(express);
        this._corsProvider = new corsProvider(this._app);
        this._scheduleProvider = new scheduleProvider();
    }

    bind = async () => {
        await this.setCORS();
        await this.globalMiddlewares();
        await this.setResponseHeader();
        await this.setRouter();
        await this.notFoundHandler();
        await this.errorHandler();
        await this.schedule();
    };

    setCORS = async () => {
        await this._corsProvider.bind();
    };

    schedule = async () => {
        await this._scheduleProvider.bind();
    };

    globalMiddlewares = async () => {
        this._app.use(logger('dev'));
        this._app.use(express.json());
        this._app.use(express.urlencoded({ extended: true }));
        this._app.use(cookieParser());
        this._app.use(express.static(path.join(__dirname, './../public')));
    };

    setResponseHeader = async () => {
        // catch 404 and forward to error handler
        this._app.use((req, res, next) => {
            res.setHeader('Content-Type', 'application/json');
            next();
        });
    };

    setRouter = async () => {
        this._app.use('/api', await this._routerProvider.bind());
    };

    notFoundHandler = async () => {
        this._app.use((req, res, next) => {
            next(httpErrors(404));
        });
    };

    errorHandler = async () => {
        // error handler
        this._app.use((err, req, res, next) => {
            // set locals, only providing error in development
            res.locals.message = err.message;
            res.locals.error = req.app.get('env') === 'development' ? err : {};

            // render the error page
            res.status(err.status || 500);
            res.json(err);
        });
    };

    getExpressApp = () => {
        return this._app;
    };
}

export default new App();
