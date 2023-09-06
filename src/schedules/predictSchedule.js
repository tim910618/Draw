import { exec } from 'child_process';
import schedule from 'node-schedule';

import scheduleConfig from '../configs/scheduleConfig';

class predictSchedule {
    bind = async () => {
        // scheduleRule second, minute, hour, dayOfMonth, month, dayOfWeek
        schedule.scheduleJob(`0 0 18 *  *  0`, async () => {
            exec(`start cmd.exe /k ${scheduleConfig.predictRout}`, (error) => {
                if (error) {
                    console.error('retrain error');
                    return;
                }
                console.log('retrain done!');
            });
        });
    };
}

export default predictSchedule;
