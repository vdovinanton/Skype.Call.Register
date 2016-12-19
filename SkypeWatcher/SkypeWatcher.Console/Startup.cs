using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SkypeWatcher.Core;
using SkypeWatcher.Core.Mock;
using SkypeWatcher.Entity;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher
{
    public class Startup
    {
        private static void Main(string[] args)
        {
            SkypeBase skype = new Core.SkypeWatcher();
            skype.CallFinished += CallFinished;

            /*skype.CallInProgress += (sender, model) =>
            {
                skype.Billing.OnMinuteLeft += OnMinuteLeft;
            };*/
            
            MockD();

            //SkypeBilling.Instance.Run();
            //SkypeBilling.Instance.OnMinuteLeft += OnMinuteLeft;

            //Thread.Sleep(3500);
            //SkypeBilling.Instance.CToken.Cancel();
            Console.WriteLine("I'm waiting for call... If your want to exit, press any key.");
            Console.ReadKey();
        }

        private static void OnMinuteLeft(object sender, WebUser webUser)
        {
            var info = webUser.DialogType == PaymentType.ByMinute
                ? $"Payment: {webUser.Payment}"
                : webUser.DialogType == PaymentType.ByTimeLimit ? $"Time value: {webUser.LimitTimeValue}" : "";
            Console.WriteLine(info);
        }

        private static void CallFinished(object sender, Dialog dialog)
        {
            Console.WriteLine($"\nCall info from '{dialog.Partner.LoginName}':" +
                              $"\n - Call begin: {dialog.CallHistory.First().Start.ToLocalTime()}" +
                              $"\n - End call: {dialog.CallHistory.First().End.ToLocalTime()}");
            Console.WriteLine("\nI'm waiting for call... If your want to exit, press any key.");
        }

        private static void MockD()
        {
            var dialog = new Dialog
            {
                Client = new Person
                {
                    LoginName = "linker",
                },
                Partner = new Person
                {
                    LoginName = "bravo04"
                },
                CallHistory = new List<CallHistory>
                {
                    new CallHistory
                    {
                        Start = DateTime.UtcNow,
                        End = DateTime.UtcNow.AddMinutes(2)
                    }
                }
            };
            using (var unit = new UnitOfWork(new SkypeCallContext()))
            {
                unit.Dialog.CreateOrUpdate(dialog);
                unit.Complete();
            }
        }
    }
}