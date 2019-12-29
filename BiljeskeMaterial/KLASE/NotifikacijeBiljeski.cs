using BiljeskeMaterial.KONTROLE;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace BiljeskeMaterial.KLASE
{
    public class NotifikacijeBiljeski : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Baza db = new Baza();
                List<Biljeska> biljeske = db.DohvatiDanasnjeBiljeske();
                foreach (Biljeska biljeska in biljeske)
                {
                    if (DateTime.Parse(biljeska.VrijemePodsjecanja).ToString("HH:mm") == DateTime.Now.ToString("HH:mm"))
                    {
                        Notifikacija notifikacijaProzor = new Notifikacija(biljeska.Naslov, biljeska.Sadrzaj);
                        notifikacijaProzor.Show();
                        notifikacijaProzor.Topmost = true;
                    }
                }
            });

            return Task.CompletedTask;
        }
    }
}
