using System;
using System.Collections.Generic;
using SkypeWatcher.Core.Mock;
using SkypeWatcher.Entity;
using SkypeWatcher.Entity.Models;
using SKYPE4COMLib;


namespace SkypeWatcher.Core
{
    public class SkypeWatcher: SkypeBase
    {
        protected override Dialog CreateDialog(Call pcall, TCallStatus status)
        {
            Dialog dialog = null;
            if (status == TCallStatus.clsFinished && pcall.Type == TCallType.cltIncomingP2P)
            {
                dialog = new Dialog
                {
                    Client = new Person
                    {
                        LoginName = ClientNickName
                    },
                    Partner = new Person
                    {
                        LoginName = pcall.PartnerHandle
                    },
                    CallHistory = new List<CallHistory>
                    {
                        new CallHistory
                        {
                            Start = pcall.Timestamp.ToUniversalTime(),
                            End = DateTime.UtcNow
                        }
                    }
                };
            }
            return dialog;
        }

        protected override void Save(Dialog user)
        {
            using (var unit = new UnitOfWork(new SkypeCallContext()))
            {
                unit.Dialog.CreateOrUpdate(user);
                unit.Complete();
            }
        }

        protected override WebUser UserInfo(string skypeName, PaymentType type, bool isNull = false)
        {
            return isNull 
                ? null 
                : new WebUser(id: 1,
                    name: skypeName,
                    tariff: 8.15,
                    type: type,
                    paymentCount: 45);
        }

        protected override void AllowCall(ICall call, WebUser user)
        {
            if (user.Payment < user.TariffPlan && user.DialogType != PaymentType.ByTimeLimit)
            {
                Billing.CToken.Cancel();
                EndCall(call);
                Console.WriteLine($" ---- Call is canceled: Payment: {user.Payment} Tariff: {user.TariffPlan} Type: {user.DialogType}");
            }
        }

        public static void EndCall(ICall call)
        {
            try
            {
                call.Finish();
                //TODO: To determine that the call was dropped and did not call the function saving
            }
            catch (Exception)
            {
                // igonre
            }
        }
    }
}