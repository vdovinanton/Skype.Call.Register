using System;
using System.Threading;
using SkypeWatcher.Core.Mock;
using SkypeWatcher.Entity.Models;
using SKYPE4COMLib;

namespace SkypeWatcher.Core
{
    public abstract class SkypeBase: SkypeStartup
    {
        private WebUser _user;
        private ViewModel _model;
        public SkypeBilling Billing { get; set; }
        public EventHandler<Dialog> CallFinished { get; set; } = delegate { };
        public EventHandler<ViewModel> CallInProgress { get; set; } = delegate { };
        
        #region Abstracts
        protected abstract Dialog CreateDialog(Call pcall, TCallStatus status);
        protected abstract void AllowCall(ICall call, WebUser user);
        protected abstract void Save(Dialog dialog);
        protected abstract WebUser UserInfo(string skypeName, PaymentType type, bool isNull = false);
        #endregion

        protected override void OnCallRecived(Call pcall, TCallStatus status)
        {
            if (status == TCallStatus.clsRinging)
            {
                _user = UserInfo("Ekaterina_Romanova", PaymentType.ByTimeLimit, false) ?? new WebUser(PaymentType.Undefined, 0);

                _model = new ViewModel
                    {
                        Name = pcall.PartnerHandle,
                        Status = status,
                        UserStatus = _user == null
                                ? UserStatus.NotFound : _user.Payment < 1
                                ? UserStatus.EmptyAccountBalance : UserStatus.OK
                    };

                // Access denied if: 1) user.Payment < 1 && user.DialogType != PaymentType.ByTimeLimit
                //                   2) user payment < tariff plan && user.DialogType != PaymentType.ByTimeLimit
                AllowCall(pcall, _user);
            }

            // TODO: upgrade model status call before initail object ViewModel
            _model.Status = status;

            if (status == TCallStatus.clsInProgress)
            {
                Billing = new SkypeBilling(_user, pcall);
            }

            CallInProgress(this, _model);

            if (status == TCallStatus.clsFinished)
            {
                var dialog = CreateDialog(pcall, status);
                CallFinished(this, dialog);
                Save(dialog);
            }
        }
    }
}