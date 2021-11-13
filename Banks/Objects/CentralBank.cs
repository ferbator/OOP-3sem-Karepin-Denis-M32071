using System;
using System.Collections.Generic;
using System.IO;
using Banks.Objects.AccountServices;
using Banks.Objects.ClientServices;
using Banks.Tools;

namespace Banks.Objects
{
    public class CentralBank
    {
        private readonly List<Bank> _banks;
        private readonly List<Transaction> _transactions;
        public CentralBank()
        {
            _banks = new List<Bank>();
            _transactions = new List<Transaction>();
        }

        public Bank AddBankToBase(string name, double limitForNotVerification, double creditLimitForCreditAccounts, double commissionUsingForCreditAccounts, Dictionary<double, double> percentageOnBalanceForDepositAccounts, double percentageOnBalanceForDebitAccounts)
        {
            _banks.Add(new Bank(name, limitForNotVerification, creditLimitForCreditAccounts, commissionUsingForCreditAccounts, percentageOnBalanceForDepositAccounts, percentageOnBalanceForDebitAccounts));
            return _banks[^1];
        }

        public IAccount RegAccountClientInBank(Bank bank, Client client, AccountOption option, double amount)
        {
            if (bank == null) throw new CentralBankException("null bank");
            if (client == null) throw new CentralBankException("null client");
            if (!_banks.Contains(bank)) throw new CentralBankException("Bank dont registered");
            if (amount < 0) throw new CentralBankException("Negative balance");
            IAccount account;
            switch (option)
            {
                case AccountOption.Credit:
                    account = new CreditAccount(client, bank, amount);
                    bank.RegisterClient(client, account);
                    return account;
                case AccountOption.Deposit:
                    account = new DepositAccount(client, bank, amount);
                    bank.RegisterClient(client, account);
                    return account;
                case AccountOption.Debit:
                    account = new DebitAccount(client, bank, amount);
                    bank.RegisterClient(client, account);
                    return account;
                default:
                    throw new CentralBankException($"{option} - Incorrect options");
            }
        }

        public Transaction WithdrawalMoney(IAccount account, double amount)
        {
            if (!account.CheckVerification() && account.GetBelongBank().SetLimitForNotVerification < amount)
                throw new CentralBankException("Attempt to withdraw money from an unverified account");
            var tmpTransaction = new Transaction(account.GetIdAccount(), null, amount);
            _transactions.Add(tmpTransaction);
            account.GetBelongBank().AddTransaction(tmpTransaction);
            account.WithdrawalMoney(amount);
            return _transactions[^1];
        }

        public Transaction ReplenishmentMoney(IAccount account, double amount)
        {
            var tmpTransaction = new Transaction(null, account.GetIdAccount(), amount);
            _transactions.Add(tmpTransaction);
            account.GetBelongBank().AddTransaction(tmpTransaction);
            account.ReplenishmentMoney(amount);
            return _transactions[^1];
        }

        public Transaction TransferMoney(IAccount account1, IAccount account2, double amount)
        {
            if (!account1.CheckVerification() && account1.GetBelongBank().SetLimitForNotVerification < amount)
                throw new CentralBankException("Attempt to withdraw money from an unverified account");
            var tmpTransaction = new Transaction(account1.GetIdAccount(), account2.GetIdAccount(), amount);
            _transactions.Add(tmpTransaction);
            account1.GetBelongBank().AddTransaction(tmpTransaction);
            account2.GetBelongBank().AddTransaction(tmpTransaction);
            account1.TransferMoney(account2, amount);
            return _transactions[^1];
        }

        public void CancelTransaction(Transaction transaction)
        {
            if (transaction == null) throw new CentralBankException("Incorrect transaction");
            IAccount tmpTransferAccount = null;
            IAccount tmpWithdrawalAccount = null;
            if (transaction.TransferAccount != null && transaction.WithdrawalAccount != null)
            {
                foreach (Bank bank in _banks)
                {
                    tmpTransferAccount = bank.FindAccount(transaction.TransferAccount);
                    tmpWithdrawalAccount = bank.FindAccount(transaction.WithdrawalAccount);
                    if (tmpTransferAccount != null && tmpWithdrawalAccount != null) break;
                }

                tmpTransferAccount?.ReplenishmentMoney(transaction.Amount);
                tmpWithdrawalAccount?.WithdrawalMoney(transaction.Amount);
            }
            else if (transaction.WithdrawalAccount == null && transaction.TransferAccount != null)
            {
                foreach (Bank bank in _banks)
                {
                    tmpTransferAccount = bank.FindAccount(transaction.TransferAccount);
                    if (tmpTransferAccount != null) break;
                }

                tmpTransferAccount?.WithdrawalMoney(transaction.Amount);
            }
            else if (transaction.TransferAccount == null && transaction.WithdrawalAccount != null)
            {
                foreach (Bank bank in _banks)
                {
                    tmpWithdrawalAccount = bank.FindAccount(transaction.WithdrawalAccount);
                    if (tmpWithdrawalAccount != null) break;
                }

                tmpWithdrawalAccount?.ReplenishmentMoney(transaction.Amount);
            }

            _transactions.Remove(transaction);
        }

        public void ManageTime(int countOfDay)
        {
            for (int i = 0; i < countOfDay % 30; i++)
            {
                foreach (Bank bank in _banks)
                {
                    bank.AccruePercentage();
                }
            }
        }

        public bool FindBank(Bank bank)
        {
            return _banks.Contains(bank);
        }
    }
}