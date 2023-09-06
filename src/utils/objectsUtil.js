import _ from 'lodash';

class objectsUtil {
    /**
     * Generate A Singleton Instance
     */
    generateSingletonInstance = (symbol, constFunc) => {
        if (_.isUndefined(global[symbol])) {
            global[symbol] = constFunc();
        }
        return global[symbol];
    };
}

export default new objectsUtil();
