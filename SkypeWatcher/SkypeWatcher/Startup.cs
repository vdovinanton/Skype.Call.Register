using System;
using System.Collections.Generic;
using SkypeWatcher.Entity;
using SkypeWatcher.Entity.Models;

namespace SkypeWatcher
{
    public class Startup
    {
        private static void Main(string[] args)
        {
            //TODO: implement check skype proccess
            //TODO: if db contain login name then write to istory new call info

            var skype = new SkypeWatcher();

            skype.CallHandler += (sender, user) =>
            {
                using (var unit = new UnitOfWork(new SkypeCallContext()))
                {
                    unit.UsersRepository.Add(user);
                    unit.Complete();
                }
            };
            Console.ReadKey();
        }

        private static void MockData()
        {
            using (var unit = new UnitOfWork(new SkypeCallContext()))
            {
                var user = new SkypeUser
                {
                    LoginName = "sancho",
                    CallHistory = new List<CallHistory>
                    {
                        new CallHistory
                        {
                            Start = DateTime.UtcNow,
                            End = DateTime.UtcNow.AddMinutes(2)
                        }
                    }
                };
                unit.UsersRepository.Add(user);
                unit.Complete();
            }
        }
    }
}
