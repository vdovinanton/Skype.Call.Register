using System;
using SkypeWatcher.Entity;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher
{
    public class Startup
    {
        private static void Main(string[] args)
        {
            var skype = new SkypeWatcher();
            skype.CallHandler += CallHandler;

            Console.WriteLine("I'm waiting for call... If your want to exit, press any key.");
            Console.ReadKey();
        }

        private static void CallHandler(object sender, SkypeUser skypeUser)
        {
            using (var unit = new UnitOfWork(new SkypeCallContext()))
            {
                unit.UsersRepository.AddOrCreate(skypeUser);
                unit.Complete();
            }
        }
    }
}
