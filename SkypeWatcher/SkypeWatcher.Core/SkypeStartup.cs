using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;

namespace SkypeWatcher.Core
{
    public abstract class SkypeStartup
    {
        protected abstract void OnCallRecived(Call pcall, TCallStatus status);

        private readonly Skype _skype = new Skype();
        protected string ClientNickName { get; set; }


        protected SkypeStartup()
        {
            Run().Wait();
        }

        private Task Run()
        {
            return Task.Factory.StartNew(() =>
            {
                if (!_skype.Client.IsRunning)
                    _skype.Client.Start(true, true);

                try
                {
                    _skype.CallStatus += OnCallRecived;
                    _skype.Attach(5, false);

                    ClientNickName = _skype.CurrentUser.Handle;

                    Console.WriteLine($"Skype attached to '{ClientNickName}'");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error:\n{e.Message}");
                    throw;
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
