namespace Banks.Objects.AccountServices
{
    public interface IAccount
    {
        void WithdrawalMoney(double amount);
        void ReplenishmentMoney(double amount);
        void TransferMoney(IAccount account, double amount);
        void ActionWithAccount();
        string GetIdAccount();
        bool CheckVerification();
        Bank GetBelongBank();
    }
}