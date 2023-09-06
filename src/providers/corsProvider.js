import cors from 'cors';

class corsProvider {
    constructor(expressApp) {
        this.app = expressApp;
    }

    bind = async () => {
        await this.bindCors();
    };

    bindCors = async () => {
        await this.app.use(cors());
    };
}

export default corsProvider;
