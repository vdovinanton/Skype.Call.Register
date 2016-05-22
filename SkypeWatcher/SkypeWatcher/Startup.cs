using System;
using System.Collections.Generic;
using System.Linq;
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
            //skype.CallHandler += CallHandler;
            MockData();
            //Console.ReadKey();
        }

        private static void CallHandler(object sender, SkypeUser skypeUser)
        {
            using (var unit = new UnitOfWork(new SkypeCallContext()))
            {
                unit.UsersRepository.AddOrCreate(skypeUser);
                unit.Complete();
            }
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
                            Start = DateTime.UtcNow.AddMinutes(new Random().Next(0, 30)),
                            End = DateTime.UtcNow.AddMinutes(new Random().Next(30, 60))
                        }
                    }
                };

                unit.UsersRepository.AddOrCreate(user);
                unit.Complete();
            }
        }
    }
}
