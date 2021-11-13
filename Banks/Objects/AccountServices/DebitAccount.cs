using System;
using Banks.Objects.ClientServices;
using Banks.Tools;

namespace Banks.Objects.AccountServices
{
    public class DebitAccount : IAccount
    {
        private double _balance;
        private double _percentageOnBalance;
        private string _numberOfAccount;
        private bool _verification;
        public DebitAccount(Client user, Bank bank, double amount)
        {
            if (bank == null) throw new CentralBankException("null bank");
            if (user == null) throw new CentralBankException("null client");
            _verification = user.IsAllInfo;
            _percentageOnBalance = bank.SetPercentageOnBalanceForDebitAccounts;
            BelongBank = bank;
            _balance = amount;
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
            WithdrawalMoney(amount);
            account.ReplenishmentMoney(amount);
        }

        public void ActionWithAccount()
        {
            _balance += _balance * _percentageOnBalance / 100;
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