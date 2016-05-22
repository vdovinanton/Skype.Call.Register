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
                // If client not running, them start minimized with no splash screen
                if (!_skype.Client.IsRunning)
                    _skype.Client.Start(true, true);

                try
                {
                    _skype.CallStatus += OnCallRecived;
                    _skype.Attach(5, false);

                    Console.WriteLine("Skype attached");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error:\n{e.Message}");
                    throw;
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void OnCallRecived(Call pcall, TCallStatus status)
        {
            if (status != TCallStatus.clsFinished || pcall.Type != TCallType.cltIncomingP2P) return;
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
            Console.WriteLine("\nI'm waiting for call... If your want to exit, press any key.");
            CallHandler(this, user);
        }
    }
}