using System.Reflection;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using Coravel.Invocable;


namespace backend.Schedule
{
    public class analysisSchedule : IInvocable
    {
        public analysisSchedule()
        {

        }

        public Task Invoke()
        {
            // List<HolidayViewModel> holidayList = _suhuakaiservice.Getholiday();
            // string dt = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            // HolidayViewModel holiday = holidayList.Where(m=>Convert.ToDateTime(m.holiday_date_e).ToString("yyyy-MM-dd") == dt).FirstOrDefault();
            
            // if(holiday != null){
            //     _t61service.analysis(holiday.holiday);
            //     _suhuakaiservice.analysis(holiday.holiday);
            // }
            return Task.CompletedTask;  
        }

    }
}