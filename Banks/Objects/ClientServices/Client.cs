using System;
using System.Collections.Generic;
using Banks.Objects.AccountServices;
using Banks.Tools;

namespace Banks.Objects.ClientServices
{
    public class Client
    {
        private readonly Dictionary<Bank, List<IAccount>> _clientCollectionAccounts;
        internal Client(string name, string surname, string address, string passport)
        {
            Id = Guid.NewGuid();
            Name = name ?? throw new CentralBankException("Incorrect name");
            Surname = surname ?? throw new CentralBankException("Incorrect surname");
            Address = address;
            Passport = passport;
            _clientCollectionAccounts = new Dictionary<Bank, List<IAccount>>();
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string Passport { get; }
        public bool IsAllInfo => !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Passport);

        public static void GetEvent(double other)
        {
            Console.WriteLine($"Change {other}");
        }

        public static ClientBuilder Builder(string name, string surname)
        {
            return new ClientBuilder().AddName(name).AddSurname(surname);
        }

        public void SubscribeToBankEvent(Bank bank)
        {
            bank.ChangeFieldInBank += GetEvent;
        }

        public void CreateAccount(Bank bank, IEnumerable<IAccount> accounts)
        {
            if (!_clientCollectionAccounts.ContainsKey(bank))
                _clientCollectionAccounts.Add(bank, (List<IAccount>)accounts);
            else
                _clientCollectionAccounts[bank] = (List<IAccount>)accounts;
        }
    }
}