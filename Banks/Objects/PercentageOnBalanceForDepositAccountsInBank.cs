using System.Collections.Generic;

namespace Banks.Objects
{
    public class PercentageOnBalanceForDepositAccountsInBank
    {
        public PercentageOnBalanceForDepositAccountsInBank()
        {
            PairsSumAndPercent = new Dictionary<double, double>();
        }

        public Dictionary<double, double> PairsSumAndPercent { get; }

        public void AddParametersForDepositAccountBank(double sum, double percentage)
        {
            PairsSumAndPercent.Add(sum, percentage);
        }
    }
}