using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SKYPE4COMLib;

namespace SkypeWatcher
{
    public class Startup
    {
        private static readonly Skype Skype = new Skype();
        private static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    Skype.MessageStatus += OnMessageRecived;
                    Skype.CallDtmfReceived += OnCallRecived;
                    Skype.Attach();
                    Console.WriteLine("Skype attached");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error\n{e.Message}");
                    throw;
                }
                while (true)
                {
                    Thread.Sleep(1000);
                }
            }, TaskCreationOptions.LongRunning);

            while (true)
            {
                Thread.Sleep(1000);
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void OnCallRecived(Call pcall, string code)
        {
            Console.WriteLine("Call is recived");
        }

        private static void OnMessageRecived(ChatMessage pmessage, TChatMessageStatus status)
        {
            if (status == TChatMessageStatus.cmsReceived)
            {
                Console.WriteLine($"Message:\n{pmessage.Body}");
            }
        }
    }
}
