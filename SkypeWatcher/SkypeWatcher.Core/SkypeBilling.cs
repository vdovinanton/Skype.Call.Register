using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SkypeWatcher.Core.Mock;
using SkypeWatcher.Entity.Models;
using SKYPE4COMLib;

namespace SkypeWatcher.Core
{
    public class SkypeBilling
    {
        public CancellationTokenSource CToken = new CancellationTokenSource();
        public static SkypeBilling Instance => _instance.Value;
        private static readonly Lazy<SkypeBilling> _instance = new Lazy<SkypeBilling>(() => new SkypeBilling());
        public EventHandler<WebUser> OnMinuteLeft { get; set; } = delegate { };
        private WebUser User { get; set; }
        private readonly TimeSpan _period = TimeSpan.FromSeconds(1);

        public SkypeBilling(WebUser user, Call pcall)
        {
            User = user;
            Run(pcall);
        }

        public SkypeBilling()
        {
        }

        public Task Run(Call pcall)
        {
            User = User ?? new WebUser( id: 1,
                                        name: "Ekaterina_Romanova",
                                        tariff: 8.15,
                                        type: PaymentType.ByMinute,
                                        paymentCount: 45);

            return Task.Factory.StartNew(() =>
            {
                var info = User.DialogType == PaymentType.ByMinute
                    ? $"User payment: {User.Payment}\nUser tariff: {User.TariffPlan}"
                    : User.DialogType == PaymentType.ByTimeLimit ? $"Limit value: {User.LimitTimeValue}" : "";

                Console.WriteLine(info);
                while (true)
                {
                    Thread.Sleep(_period);

                    switch (User.DialogType)
                    {
                            case PaymentType.ByMinute: CalculateByMinute(); break;
                            case PaymentType.ByTimeLimit: CalculateByLimitTime(pcall); break;
                    }
                    
                    if (CToken.IsCancellationRequested)
                    {
                        Console.WriteLine("task canceled");
                        SkypeWatcher.EndCall(pcall);
                        break;
                    }
                }
                // ReSharper disable once FunctionNeverReturns
            });
        }

        private void CalculateByMinute()
        {
            User.Payment = User.Payment - User.TariffPlan;
            OnMinuteLeft(this, User); 
            if (User.Payment < User.TariffPlan)
            {
                CToken.Cancel();
            }
        }

        private void CalculateByLimitTime(Call pcall)
        {
            var timeToEnd = pcall.Timestamp.TimeOfDay.Add(TimeSpan.FromSeconds(User.LimitTimeValue--));
            var startTime = pcall.Timestamp.TimeOfDay;
            OnMinuteLeft(this, User);
            if (startTime >= timeToEnd)
            {
                CToken.Cancel();
            }
        }
    }
}
