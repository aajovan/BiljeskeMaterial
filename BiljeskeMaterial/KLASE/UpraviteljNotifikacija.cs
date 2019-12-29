using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace BiljeskeMaterial.KLASE
{
    public class UpraviteljNotifikacija
    {
        public UpraviteljNotifikacija()
        {
            ZakaziBiljeske().GetAwaiter().GetResult();
        }
        public static async Task ZakaziBiljeske()
        {
            NameValueCollection props = new NameValueCollection
    {
        { "quartz.serializer.type", "binary" }
    };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<NotifikacijeBiljeski>()
                .WithIdentity("myJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(60)
                    .RepeatForever())
            .Build();

            await sched.ScheduleJob(job, trigger);
        }
    }
}
