import predictSchedule from '../schedules/predictSchedule';

class scheduleProvider {
    constructor() {
        this._predictSchedule = new predictSchedule();
    }

    bind = async () => {
        await this._predictSchedule.bind();
    };
}

export default scheduleProvider;
