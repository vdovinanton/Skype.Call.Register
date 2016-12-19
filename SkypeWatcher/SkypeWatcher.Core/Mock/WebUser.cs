using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeWatcher.Core.Mock
{
    public class WebUser
    {
        public int Id { get; set; }
        public string SkypeName { get; set; }
        public double TariffPlan { get; set; }
        public PaymentType DialogType { get; set; }
        public double Payment { get; set; }
        public int LimitTimeValue { get; set; }

        public WebUser(int id, string name, double tariff, PaymentType type, double paymentCount)
        {
            Id = id;
            SkypeName = name;
            TariffPlan = tariff;
            DialogType = type;
            Payment = paymentCount;
            LimitTimeValue = DialogType == PaymentType.ByMinute ? 0 : DialogType == PaymentType.Undefined ? 0 : 10;
        }

        public WebUser(PaymentType type, double paymentCount)
        {
            DialogType = type;
            Payment = paymentCount;
        }
    }
}
