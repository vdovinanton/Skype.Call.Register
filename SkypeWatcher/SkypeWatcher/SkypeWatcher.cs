using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkypeWatcher.Entity.Models;
using SKYPE4COMLib;

namespace SkypeWatcher
{
    public class SkypeWatcher
    {
        private readonly Skype _skype = new Skype();

        /// <summary>
        /// Delegate for event <see cref="Call"/>
        /// </summary>
        public EventHandler<SkypeUser> CallHandler { get; set; } = delegate { };


        public SkypeWatcher()
        {
            Run().Wait();
        }

        private Task Run()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    _skype.CallStatus += OnCallRecived;
                    _skype.Attach();

                    Console.WriteLine("Skype attached");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error\n{e.Message}");
                    throw;
                }
            }, TaskCreationOptions.None);
        }

        private void OnCallRecived(Call pcall, TCallStatus status)
        {
            if (status != TCallStatus.clsFinished) return;

            var finishTime = pcall.Timestamp + TimeSpan.FromSeconds(pcall.Duration);
            var user = new SkypeUser
            {
                LoginName = pcall.PartnerHandle,
                CallHistory = new List<CallHistory>
                {
                    new CallHistory
                    {
                        Start = pcall.Timestamp,
                        End = finishTime
                    }
                }
            };
            Console.WriteLine($"\nCall info from '{user.LoginName}':" +
                              $"\n - Call begin: {user.CallHistory.First().Start}" +
                              $"\n - End call: {user.CallHistory.First().End}");
 
            CallHandler(this, user);
        }
    }
}