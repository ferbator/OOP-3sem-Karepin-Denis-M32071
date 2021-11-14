using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Objects;
using Banks.Objects.AccountServices;
using Banks.Objects.ClientServices;

namespace Banks.Tools
{
    public class ConsoleInterface
    {
        public static void Input()
        {
            var centralBank = new CentralBank();
            Client client = null;
            IAccount account1 = null;
            IAccount account2 = null;
            Bank bank = null;
            Transaction transaction = null;
            string b = null;
            while (b != "Q")
            {
                Console.WriteLine("1 - Start Create Bank");
                Console.WriteLine("2 - Start Create Client");
                Console.WriteLine("3 - Start Create Account");
                Console.WriteLine("4 - Start Do Transaction");
                Console.WriteLine("5 - Start Canceled Transaction");
                Console.WriteLine("Q - Exit Program");
                b = Console.ReadLine();

                switch (b)
                {
                    case "1":
                    {
                        Console.WriteLine("Set Name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Set limitForNotVerification");
                        double limitForNotVerification = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Set creditLimitForCreditAccounts");
                        double creditLimitForCreditAccounts = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Set commissionUsingForCreditAccounts");
                        double commissionUsingForCreditAccounts = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Set percentageOnBalanceForDepositAccounts");
                        Console.WriteLine("Example: 50000 3");
                        Console.WriteLine("Example: 100000 5");
                        Console.WriteLine("Example: 1000000 6");
                        var percentageOnBalanceForDepositAccounts = new PercentageOnBalanceForDepositAccountsInBank();
                        for (int i = 0; i < 3; i++)
                        {
                            b = Console.ReadLine();
                            double first = Convert.ToDouble(b?.Split(' ').First());
                            double second = Convert.ToDouble(b?.Split(' ').Last());
                            percentageOnBalanceForDepositAccounts.AddParametersForDepositAccountBank(first, second);
                        }

                        Console.WriteLine("Set percentageOnBalanceForDebitAccounts");
                        double percentageOnBalanceForDebitAccounts = Convert.ToDouble(Console.ReadLine());
                        bank = centralBank.AddBankToBase(
                            name,
                            limitForNotVerification,
                            creditLimitForCreditAccounts,
                            commissionUsingForCreditAccounts,
                            percentageOnBalanceForDepositAccounts,
                            percentageOnBalanceForDebitAccounts);
                        Console.WriteLine("Done");
                        break;
                    }

                    case "2":
                        Console.WriteLine("1 - Start Create Client");
                        Console.WriteLine("2 - Start Create Verification Client");
                        b = Console.ReadLine();
                        switch (b)
                        {
                            case "1":
                            {
                                Console.WriteLine("Set name");
                                string name = Console.ReadLine();
                                Console.WriteLine("Set surname");
                                string surname = Console.ReadLine();
                                client = Client.Builder(name, surname).GetClient();
                                Console.WriteLine("Done");
                                break;
                            }

                            case "2":
                            {
                                Console.WriteLine("Set name");
                                string name = Console.ReadLine();
                                Console.WriteLine("Set surname");
                                string surname = Console.ReadLine();
                                Console.WriteLine("Set address");
                                string address = Console.ReadLine();
                                Console.WriteLine("Set passport");
                                string passport = Console.ReadLine();
                                client = Client.Builder(name, surname).AddAddress(address).AddPassport(passport)
                                    .GetClient();
                                Console.WriteLine("Done");
                                break;
                            }
                        }

                        break;
                    case "3":
                        Console.WriteLine("1 - Start Create Debit");
                        Console.WriteLine("2 - Start Create Credit");
                        Console.WriteLine("3 - Start Create Deposit");
                        b = Console.ReadLine();
                        switch (b)
                        {
                            case "1":
                                account1 = centralBank.RegAccountClientInBank(bank, client, AccountOption.Debit, 10000);
                                account2 = centralBank.RegAccountClientInBank(bank, client, AccountOption.Debit, 10000);
                                Console.WriteLine("Done");
                                break;
                            case "2":
                                account1 = centralBank.RegAccountClientInBank(bank, client, AccountOption.Credit, 10000);
                                account2 = centralBank.RegAccountClientInBank(bank, client, AccountOption.Credit, 10000);
                                Console.WriteLine("Done");
                                break;
                            case "3":
                                account1 = centralBank.RegAccountClientInBank(bank, client, AccountOption.Deposit, 10000);
                                account2 = centralBank.RegAccountClientInBank(bank, client, AccountOption.Deposit, 10000);
                                Console.WriteLine("Done");
                                break;
                        }

                        break;
                    case "4":
                        Console.WriteLine("Set Amount Transaction");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("1 - Do Replenishment Money Transaction");
                        Console.WriteLine("2 - Do Withdrawal Money Transaction");
                        Console.WriteLine("3 - Do Transfer Money Transaction");
                        b = Console.ReadLine();
                        switch (b)
                        {
                            case "1":
                                transaction = centralBank.ReplenishmentMoney(account1, amount);
                                Console.WriteLine("Done");
                                break;
                            case "2":
                                transaction = centralBank.WithdrawalMoney(account1, amount);
                                Console.WriteLine("Done");
                                break;
                            case "3":
                                transaction = centralBank.TransferMoney(account1, account2, amount);
                                Console.WriteLine("Done");
                                break;
                        }

                        break;
                    case "5":
                        centralBank.CancelTransaction(transaction);
                        Console.WriteLine("Done");
                        break;
                }
            }
        }
    }
}