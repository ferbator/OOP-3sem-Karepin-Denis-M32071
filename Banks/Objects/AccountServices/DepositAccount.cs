using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Objects.ClientServices;
using Banks.Tools;

namespace Banks.Objects.AccountServices
{
    public class DepositAccount : IAccount
    {
        private double _balance;
        private double _percentage;
        private string _numberOfAccount;
        private bool _verification;
        public DepositAccount(Client user, Bank bank, double amount)
        {
            if (bank == null) throw new CentralBankException("null bank");
            if (user == null) throw new CentralBankException("null client");
            _verification = user.IsAllInfo;
            _percentage = bank.PercentageOnBalanceForDepositAccounts.PairsSumAndPercent.First(i => i.Key > amount).Value;
            _balance = amount;
            BelongBank = bank;
            _numberOfAccount = Guid.NewGuid().ToString("N");
        }

        public Bank BelongBank { get; }

        public void WithdrawalMoney(double amount)
        {
            _balance -= amount;
        }

        public void ReplenishmentMoney(double amount)
        {
            _balance += amount;
        }

        public void TransferMoney(IAccount account, double amount)
        {
        }

        public void ActionWithAccount()
        {
            _balance += _balance * _percentage / 100;
        }

        public string GetIdAccount()
        {
            return _numberOfAccount;
        }

        public bool CheckVerification()
        {
            return _verification;
        }

        public Bank GetBelongBank()
        {
            return BelongBank;
        }
    }
}