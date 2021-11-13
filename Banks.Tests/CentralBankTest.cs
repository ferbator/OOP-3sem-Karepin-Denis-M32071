using System;
using System.Collections.Generic;
using Banks.Objects;
using Banks.Objects.AccountServices;
using Banks.Objects.ClientServices;
using NUnit.Framework;

namespace Banks.Tests
{
    [TestFixture]
    public class CentralBankTest
    {
        private CentralBank _centralBank;
        private Bank _bank;
        private Client client;

        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
            _bank = _centralBank.AddBankToBase
            (
                "Тинькофф",
                5000,
                10000,
                5,
                new Dictionary<double, double> { { 50000, 3 }, { 100000, 4 }, { 30000, 6.5 } },
                5);
            client = Client.Builder("Виктор", "Пузо").AddAddress("пр Домов").AddPassport("3040 523322").GetClient();

        }

        [Test]
        public void AddBankToCentralBankBase()
        {
            Assert.IsTrue(_centralBank.FindBank(_bank));
        }

        [Test]
        public void CheckRegAccountClientInBank()
        {
            IAccount account = _centralBank.RegAccountClientInBank(_bank, client, AccountOption.Debit, 0);
            Assert.IsTrue(_bank.FindAccount(account.GetIdAccount()) == account);
        }

        [Test]
        public void CheckDoingTransaction()
        {
            IAccount account = _centralBank.RegAccountClientInBank(_bank, client, AccountOption.Debit, 0);
            Transaction transaction1 = _centralBank.ReplenishmentMoney(account, 100);
            Transaction transaction2 = _centralBank.WithdrawalMoney(account, 20);
            Transaction transaction3 = _centralBank.ReplenishmentMoney(account, 1000);
            Assert.IsTrue(Math.Abs(transaction1.Amount - 100) < 0.0001);
            Assert.IsTrue(Math.Abs(transaction2.Amount - 20) < 0.0001);
            Assert.IsTrue(Math.Abs(transaction3.Amount - 1000) < 0.0001);
            IAccount account2 = _centralBank.RegAccountClientInBank(_bank, client, AccountOption.Debit, 0);
            Transaction transaction4 = _centralBank.TransferMoney(account, account2, 100);
            Assert.IsTrue(Math.Abs(transaction4.Amount - 100) < 0.0001);
        }

    }
}