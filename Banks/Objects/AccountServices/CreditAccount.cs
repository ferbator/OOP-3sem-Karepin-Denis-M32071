using System;
using Banks.Objects.ClientServices;
using Banks.Tools;

namespace Banks.Objects.AccountServices
{
    public class CreditAccount : IAccount
    {
        private double _balance;
        private double _commissionUsing;
        private double _creditLimit;
        private string _numberOfAccount;
        private bool _verification;
        public CreditAccount(Client user, Bank bank, double amount)
        {
            if (bank == null) throw new CentralBankException("null bank");
            if (user == null) throw new CentralBankException("null client");
            _verification = user.IsAllInfo;
            _commissionUsing = bank.SetCommissionUsingForCreditAccounts;
            _creditLimit = bank.SetCreditLimitForCreditAccounts;
            _balance = amount;
            BelongBank = bank;
            _numberOfAccount = Guid.NewGuid().ToString("N");
        }

        public Bank BelongBank { get; }

        public void WithdrawalMoney(double amount)
        {
            if (_balance - amount > -_creditLimit)
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
            if (_balance < 0 && _balance - _commissionUsing >= -_creditLimit)
            {
                _balance -= _commissionUsing;
            }
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