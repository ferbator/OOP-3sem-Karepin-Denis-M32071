namespace Banks.Objects
{
    public class Transaction
    {
        public Transaction(string withdrawalAccount, string transferAccount, double amount)
        {
            WithdrawalAccount = withdrawalAccount;
            TransferAccount = transferAccount;
            Amount = amount;
        }

        public string WithdrawalAccount { get; }
        public string TransferAccount { get; }
        public double Amount { get; }
        public override string ToString()
        {
            return $"{WithdrawalAccount} to {TransferAccount} of {Amount}";
        }
    }
}