using System;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher
{
    public class Startup
    {
        private static void Main(string[] args)
        {
            //TODO: implement check skype proccess

            var skype = new SkypeWatcher();

            skype.CallHandler += (sender, user) =>
            {
                
            };

            Console.ReadKey();
        }
    }
}
