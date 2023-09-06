/* eslint-disable import/extensions */

/**
 * Module dependencies.
 */
import debugLib from 'debug';
import http from 'http';
import app from '../App.js';
import dotenv from 'dotenv';

const main = async () => {
    const debug = debugLib('backend:server');
    /**
     * Normalize a port into a number, string, or false.
     */

    const normalizePort = (val) => {
        const port = parseInt(val, 10);

        if (Number.isNaN(port)) {
            // named pipe
            return val;
        }

        if (port >= 0) {
            // port number
            return port;
        }

        return false;
    };

    /**
     * Get port from environment and store in Express.
     */
    const { PORT } = dotenv.config().parsed;
    const port = normalizePort(PORT || '3000');

    await app.bind();
    const expressApp = app.getExpressApp();
    expressApp.set('port', port);

    /**
     * Create HTTP server.
     */

    const server = http.createServer(expressApp);

    /**
     * Listen on provided port, on all network interfaces.
     */

    server.listen(port);

    /**
     * Event listener for HTTP server "error" event.
     */
    /* eslint no-console: 0 */
    server.on('error', (error) => {
        if (error.syscall !== 'listen') {
            throw error;
        }

        const bind = typeof port === 'string' ? `Pipe ${port}` : `Port ${port}`;

        // handle specific listen errors with friendly messages
        switch (error.code) {
            case 'EACCES':
                console.error(`${bind} requires elevated privileges`);
                process.exit(1);
                break;
            case 'EADDRINUSE':
                console.error(`${bind} is already in use`);
                process.exit(1);
                break;
            default:
                throw error;
        }
    });

    /**
     * Event listener for HTTP server "listening" event.
     */

    server.on('listening', () => {
        const addr = server.address();
        const bind = typeof addr === 'string' ? `pipe ${addr}` : `port ${addr.port}`;
        debug(`Listening on ${bind}`);
        console.log(`✅  The server is listen on ${bind}`);
    });
};

export default main();
