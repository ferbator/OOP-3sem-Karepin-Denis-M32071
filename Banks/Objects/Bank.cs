using System.Collections.Generic;
using System.Linq;
using Banks.Objects.AccountServices;
using Banks.Objects.ClientServices;
using Banks.Tools;

namespace Banks.Objects
{
    public class Bank
    {
        private Dictionary<Client, List<IAccount>> _baseBank;
        private List<Transaction> _transactions;
        private double _limitForNotVerification;
        private double _creditLimitForCreditAccounts;
        private double _commissionUsingForCreditAccounts;
        private double _percentageOnBalanceForDebitAccounts;
        public Bank(string name, double limitForNotVerification, double creditLimitForCreditAccounts, double commissionUsingForCreditAccounts, PercentageOnBalanceForDepositAccountsInBank percentageOnBalanceForDepositAccounts, double percentageOnBalanceForDebitAccounts)
        {
            Name = name ?? throw new CentralBankException("Incorrect name");
            _limitForNotVerification = limitForNotVerification;
            _creditLimitForCreditAccounts = creditLimitForCreditAccounts;
            _commissionUsingForCreditAccounts = commissionUsingForCreditAccounts;
            PercentageOnBalanceForDepositAccounts = percentageOnBalanceForDepositAccounts;
            _percentageOnBalanceForDebitAccounts = percentageOnBalanceForDebitAccounts;
            _baseBank = new Dictionary<Client, List<IAccount>>();
            _transactions = new List<Transaction>();
        }

        public delegate void ChangeFieldInBanks(double other);

        public event ChangeFieldInBanks ChangeFieldInBank;
        public string Name { get; }

        public double SetCreditLimitForCreditAccounts
        {
            get => _creditLimitForCreditAccounts;
            set
            {
                _creditLimitForCreditAccounts = value;
                ChangeFieldInBank?.Invoke(value);
            }
        }

        public double SetCommissionUsingForCreditAccounts
        {
            get => _commissionUsingForCreditAccounts;
            set
            {
                _commissionUsingForCreditAccounts = value;
                ChangeFieldInBank?.Invoke(value);
            }
        }

        public double SetLimitForNotVerification
        {
            get => _limitForNotVerification;
            set
            {
                _limitForNotVerification = value;
                ChangeFieldInBank?.Invoke(value);
            }
        }

        public PercentageOnBalanceForDepositAccountsInBank PercentageOnBalanceForDepositAccounts { get; }

        public double SetPercentageOnBalanceForDebitAccounts
        {
            get => _percentageOnBalanceForDebitAccounts;
            set
            {
                _percentageOnBalanceForDebitAccounts = value;
                ChangeFieldInBank?.Invoke(value);
            }
        }

        public void RegisterClient(Client client, IAccount account)
        {
            if (client == null) throw new CentralBankException("Incorrect client");
            if (account == null) throw new CentralBankException("Incorrect account");
            if (_baseBank.ContainsKey(client))
                _baseBank[client].Add(account);
            else
                _baseBank.Add(client, new List<IAccount> { account });
            client.CreateAccount(this, GetInfoAccounts(client));
        }

        public IAccount FindAccount(string numberId)
        {
            if (numberId == null) throw new CentralBankException("Incorrect numberId");
            return _baseBank.Values.SelectMany(i => i).First(j => j.GetIdAccount() == numberId);
        }

        public void AccruePercentage()
        {
            foreach (IAccount j in _baseBank.Values.SelectMany(i => i))
            {
                j.ActionWithAccount();
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        private IEnumerable<IAccount> GetInfoAccounts(Client client)
        {
            return _baseBank.ContainsKey(client) ? _baseBank[client].ToList() : null;
        }
    }
}