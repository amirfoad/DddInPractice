using static DddInPractice.Logic.Money;

namespace DddInPractice.Logic
{
    public sealed class SnackMachine : Entity
    {
        /// <summary>
        /// The amount of money machine has
        /// </summary>
        public Money MoneyInside { get; private set; } = None;

        /// <summary>
        /// The amount of money user inserted
        /// </summary>
        public Money MoneyInTransaction { get; private set; } = None;

        public void InsertMoney(Money money)
        {
            Money[] coinAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            if (coinAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;

            MoneyInTransaction = None;
        }
    }
}